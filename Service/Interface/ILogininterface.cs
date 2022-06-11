using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ILogininterface
    {
        Task<dynamic> Login(LoginRequest request);
        Task<IEnumerable<GetByIdLogin>> GetUserById(string username);
    }
}
