using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianBagPack.Model.Auth
{
        public class Authentification
        {
            public string AppId { get; set; }
            public string Code { get; set; }
            public string State { get; set; }
        }
}
