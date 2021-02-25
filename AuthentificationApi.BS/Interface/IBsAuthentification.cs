using AuthentificationApi.BS.DTO;
using AuthentificationAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthentificationApi.BS.Interface
{
    public interface IBsAuthentification
    {
        DtoAuthentification BuildAuthentification(string code, string state);
    }
}
