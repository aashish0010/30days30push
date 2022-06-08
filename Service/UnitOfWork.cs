using Infrastructure.Jwt;
using Microsoft.Extensions.Configuration;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public LoginService login => new LoginService(_configuration);
        public TokenService token => new TokenService(_configuration);
    }
}
