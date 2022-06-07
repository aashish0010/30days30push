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
        public LoginService login => new LoginService();
    }
}
