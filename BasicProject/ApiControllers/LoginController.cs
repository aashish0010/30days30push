using Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicProject.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        [HttpPost]
        public async Task<IEnumerable<Login>> Login(LoginRequest model)
        {
            return await _unitOfWork.login.Login(model);
        }
    }
}
