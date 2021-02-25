using BungieApiHelper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianBagPack.Model.Interface
{
    public interface IModel
    {
        Api bungieApi { get; set; }
        IHost AuthApi { get; set; }
        void Main(string[] args);
        Task InitAuthProcess();
        Task GetUserData();
    }
}
