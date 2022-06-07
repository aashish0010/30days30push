using Entity.Model;
using Infrastructure.Dapper;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class LoginService : ILogininterface
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        public async Task<IEnumerable<Login>> Login(LoginRequest request)
        {
            string sql = "sp_user";
            dict.Add("@nameorcitizen", request.UserName);
            dict.Add("@password", request.Password);
            dict.Add("@flag", "Login");
            return await DbHelper.RunQueryWithModelAync<Login>(sql,dict);
        }
    }
}
