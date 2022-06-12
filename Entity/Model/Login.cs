using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Login
    {
        public string Username { get; set; }
        public string Citizenshipnumber { get; set; }
        public string Email { get; set; }
        public string Designationname { get; set; }
        public string Token { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class Register
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string citizenshipnumber { get; set; }
        public string phonenumber { get; set; }
    }
}
