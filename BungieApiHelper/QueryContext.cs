using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BungieApiHelper
{
    public class QueryContext
    {
        public string queryName { get; set; }
        public List<Param> queryParams { get; set; }
        public List<Param> headerParam { get; set; }
        public HttpResponseMessage responseMessage { get; set; }

    }
}