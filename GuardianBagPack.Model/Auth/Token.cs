using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianBagPack.Model.Auth
{
    public class Token
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public int LifeSpan { get; set; }
        public int MemberId { get; set; }
        public DateTime LastModified { get; set; }
    }
}
