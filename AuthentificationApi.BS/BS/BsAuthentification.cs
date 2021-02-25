using AuthentificationApi.BS.DTO;
using AuthentificationApi.BS.Interface;
using AuthentificationAPI.DAL;

namespace AuthentificationApi.BS.BS
{
    public class BsAuthentification : IBsAuthentification
    {
        public static Authentification Auth { get; set; }

        public BsAuthentification() { }
        public DtoAuthentification BuildAuthentification(string code, string state)
        {
            Auth = new Authentification()
            {
                Code = code,
                State = state
            };
            return ToDto(Auth);
        }
        private DtoAuthentification ToDto(Authentification auth)
        {
            return new DtoAuthentification()
            {
                code = auth.Code,
                state = auth.State
            };
        }
    }
}
