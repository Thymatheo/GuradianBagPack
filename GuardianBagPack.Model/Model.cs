using BungieApiHelper;
using BungieApiHelper.Entity.User;
using GuardianBagPack.Model.Auth;
using GuardianBagPack.Model.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Text;

namespace GuardianBagPack.Model
{
    public class Model : IModel
    {
        public Api bungieApi { get; set; }
        public IHost AuthApi { get; set; }
        public Thread AuthApiThread { get; set; }
        private static readonly Lazy<Model> _instance = new Lazy<Model>(() => new Model(), true);
        public static IModel Instance { get => _instance.Value; }

        public Model()
        {

        }
        public void Main(string[] args)
        {
            bungieApi = new Api();
            InitAuthProcess().GetAwaiter().GetResult();
            GetUserData().GetAwaiter().GetResult();
        }

        public async Task InitAuthProcess()
        {
            await AuthentificationProcess.Init();
            Console.WriteLine("Succes!! " + AuthentificationProcess.Token.MemberId);
        }
        public async Task GetUserData()
        {
            var context = new QueryContext()
            {
                queryName = "/User/GetCurrentBungieNetUser/",
                queryParams = new List<Param>(),
                headerParam = new List<Param>()
                {
                    { new Param(){
                        name = "Authorization",
                        value=string.Concat(AuthentificationProcess.Token.Type,
                                            " ",
                                            AuthentificationProcess.Token.Value)
                        }
                    }
                },
                responseMessage = new HttpResponseMessage()
            };
            var response = await bungieApi.ExecuteApiQuery<GeneralUser>(context);
        }
    }
}
