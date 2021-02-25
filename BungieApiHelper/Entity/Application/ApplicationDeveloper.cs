using BungieApiHelper.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BungieApiHelper.Entity.Application
{
    public class ApplicationDeveloper
    {
        public int role { get; set; }
        public int apiEulaVersion { get; set; }
        public UserInfoCard user { get; set; }
    }
}
