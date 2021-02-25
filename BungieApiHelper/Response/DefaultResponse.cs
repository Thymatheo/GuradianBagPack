using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BungieApiHelper
{
    public class DefaultResponse<T>
    {
        public T Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public Dictionary<string,string> MessageData { get; set; }
        public string DetailedErrotTrace { get; set; }
    }
}
