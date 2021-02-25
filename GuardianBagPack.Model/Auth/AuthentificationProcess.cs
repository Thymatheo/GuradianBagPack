using BungieApiHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuardianBagPack.Model.Auth
{
    public static class AuthentificationProcess
    {
        public static Authentification Auth { get; set; }
        public static string BasicToken { get; set; }
        public static Token Token { get; set; } = new Token();
        public static Timer RefreshAccessTokenRimer { get; set; }

        public static readonly int CLIENT_ID = 32440;
        private static readonly string AUTH_API_URL = "https://localhost:44317/";


        public static async Task Init()
        {
            await _InitAppId();
            await _InitState();
            await _InitCode();
            _VerifyAuth();
            BasicToken = _InitBasicToken();
            await _InitOauth2Token();

        }

        private static void _VerifyAuth()
        {
            Console.WriteLine(string.Format("AppID : {0}\nState : {1}\nCode : {2}", Auth.AppId, Auth.State, Auth.Code));
        }

        private async static Task _InitState()
        {
            Auth.State = await _ApiCallerGet<string>(new Uri(AUTH_API_URL + "api/Auth/state/new"));
            DtoAuthUpdateState result = await _ApiCallerPut<DtoAuthUpdateState>(new Uri(AUTH_API_URL + "api/Auth/update/" + Auth.AppId + "/state?state=" + Auth.State));
            if (result.AppId != Auth.AppId)
                throw new Exception("App id are different!!");
            if (result.State != Auth.State)
                throw new Exception("State are different!!");
        }

        private async static Task _InitAppId()
        {
            if (Auth.AppId == null)
            {
                Auth.AppId = await _ApiCallerGet<string>(new Uri(AUTH_API_URL + "api/Auth/appid/new"));
                DtoAuthCreated result = await _ApiCallerPost<DtoAuthCreated>(new Uri(AUTH_API_URL + "api/Auth/add"));
                if (result.AppId != Auth.AppId) throw new Exception("App id are different!!");
            }
        }

        private async static Task _InitCode()
        {
            try
            {
                using (HttpClient client = new HttpClient() { BaseAddress = new Uri("https://www.bungie.net/en/oauth/authorize") })
                {
                    var context = new QueryContext()
                    {
                        queryParams = new List<Param>(),
                        responseMessage = new HttpResponseMessage()
                    };
                    string url = client.BaseAddress + "?client_id=" + CLIENT_ID + "&response_type=code&state=" + Auth.State;
                    Process browser = new Process();
                    browser.EnableRaisingEvents = true;
                    browser.StartInfo.Arguments = "--new-window " + url;
                    browser.StartInfo.FileName = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe";
                    browser.Start();
                    browser.WaitForExit();
                    Auth.Code = (await _ApiCallerGet<Authentification>(new Uri(AUTH_API_URL + "api/Auth/" + Auth.AppId))).Code;
                    if (Auth.Code == null)
                        throw new Exception("Code can't be null please retry to loggin!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private async static Task<T> _ApiCallerGet<T>(Uri apiEndPoint)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = apiEndPoint })
            {
                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
        }
        private async static Task<T> _ApiCallerPost<T>(Uri apiEndPoint)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = apiEndPoint })
            {
                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null))
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
        }
        private async static Task<T> _ApiCallerPut<T>(Uri apiEndPoint)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = apiEndPoint })
            {
                using (HttpResponseMessage response = await client.PutAsync(client.BaseAddress, null))
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        private static async Task _InitOauth2Token()
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri("https://www.bungie.net/Platform/App/OAuth/Token/") })
            {
                var headerConfig = new Dictionary<string, string>()
                {
                    {"client_id", CLIENT_ID.ToString()},
                    { "grant_type","authorization_code"},
                    {"code", Auth.Code}
                };
                var content = new FormUrlEncodedContent(headerConfig);
                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    try
                    {
                        var responseContent = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
                        Token.Value = responseContent.access_token;
                        Token.Type = responseContent.token_type;
                        Token.LifeSpan = responseContent.expires_in;
                        Token.LastModified = DateTime.Now;
                        Token.MemberId = responseContent.membership_id;
                        Console.WriteLine(JsonConvert.SerializeObject(responseContent, Formatting.Indented));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private static string _InitBasicToken()
        {
            var token = CLIENT_ID + ":" + Auth.Code;
            byte[] array = new byte[256];
            for (int x = 0; x < token.Length; ++x)
                array[x] = (byte)token[x];
            return Convert.ToBase64String(array);
        }


        private static string _RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];
                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }
            return res.ToString();
        }
    }

    internal class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public int membership_id { get; set; }
    }
    internal class DtoAuthCreated
    {
        public int IdAuth { get; set; }
        public string AppId { get; set; }
    }
    internal class DtoAuthUpdateState
    {
        public string AppId { get; set; }
        public string State { get; set; }
    }
}
