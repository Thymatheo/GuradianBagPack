using AuthentificationApi.BS.DTO;
using AuthentificationApi.BS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthentificationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private IBsAuthentification _bsAuthentification;

        public AuthentificationController(IBsAuthentification bsAuthentification)
        {
            _bsAuthentification = bsAuthentification;
        }
        [HttpGet]
        public ActionResult<string> GetAuthentification(string code, string state)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                    return BadRequest("code manquant");
                if (string.IsNullOrWhiteSpace(state))
                    return BadRequest("state manquant");
                var auth = _bsAuthentification.BuildAuthentification(code, state);
                if (auth.state == state && auth.code == code)
                    return Ok("Connection succesfull! You can close the window");
                else
                    return BadRequest("Connection Fail!!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
