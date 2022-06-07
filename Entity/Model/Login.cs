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
        public string citizenshipnumber { get; set; }
        public string Email { get; set; }
        public string Designationname { get; set; }
    }
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
