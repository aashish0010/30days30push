using Entity.Model;
using Infrastructure.Dapper;
using Infrastructure.Jwt;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Service
{
    public class LoginService : ILogininterface
    {
        private readonly IConfiguration _configuration;
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        Dictionary<string, string> dict = new Dictionary<string, string>();
        public async Task<dynamic> Login(LoginRequest request)
        {
            var token = new TokenService(_configuration);
            var login = new Login();
            string sql = "sp_user";
            dict.Add("@nameorcitizen", request.UserName);
            dict.Add("@password", request.Password);
            dict.Add("@flag", "Login");
            var res = await DbHelper.RunQueryWithoutModelAsync(sql, dict);

            if(res.FirstOrDefault().StatusCode==400)
            {
                login.StatusCode = res.FirstOrDefault().StatusCode;
                login.Message = res.FirstOrDefault().Message;
            }
            else
            {
                login.Username = res.FirstOrDefault().username;
                login.Email = res.FirstOrDefault().email;
                login.Citizenshipnumber = res.FirstOrDefault().citizenshipnumber;
                login.Designationname = res.FirstOrDefault().designationname;
                login.Token = token.TokenGenerateString(login.Username);
            }
            return login;
        }
    }
}
