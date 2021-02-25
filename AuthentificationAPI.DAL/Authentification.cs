using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthentificationAPI.DAL
{
    public class Authentification
    {
        public string AppId { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
    }
}
