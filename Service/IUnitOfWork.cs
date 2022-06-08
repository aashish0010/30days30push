using Infrastructure.Jwt;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUnitOfWork
    {
        public LoginService login { get; }
        public TokenService token { get; }
    }
}
