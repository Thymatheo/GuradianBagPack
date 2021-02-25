using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BungieApiHelper
{
    public class Api
    {
        public static string apiUrl { get; } = "https://www.bungie.net/Platform";
        public static string apiKeyLabel { get; } = "X-API-Key";
        public static string apiKeyValue { get; } = "1ad4c51973af4fad84ebecdc1535fbf3";

        public Api()
        {
            _BuildHttpClient();
        }

        public async Task<DefaultResponse<T>> ExecuteApiQuery<T>(QueryContext context)
        {
            using (HttpClient client = _BuildHttpClient())
            {
                var uri = client.BaseAddress.ToString();
                var param = "";
                if (context.queryParams.Count() >= 1)
                {
                    param = "?";
                    context.queryParams.ForEach(x =>
                    {
                        if (param.Length > 1)
                            param = param + "&";
                        param = param + x.name + "=" + x.value;
                    });
                }
                if (context.headerParam.Count >= 1)
                {
                    context.headerParam.ForEach(x =>
                    {
                        client.DefaultRequestHeaders.Add(x.name, x.value);
                    });
                }
                var url = uri + context.queryName + param;
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    try
                    {
                        Console.OutputEncoding = Encoding.Unicode;
                        var content = await response.Content.ReadAsStringAsync();
                        var defaultResponse = JsonConvert.DeserializeObject<DefaultResponse<T>>(@content);
                        Console.WriteLine(JsonConvert.SerializeObject(defaultResponse, Formatting.Indented));
                        return null;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.ReadLine();
                        return null;
                    }
                }
            }
        }

        private HttpClient _BuildHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(apiUrl)
            };
            httpClient.DefaultRequestHeaders.Add(apiKeyLabel, apiKeyValue);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json") { CharSet = "utf-8" });
            return httpClient;
        }
    }
}
