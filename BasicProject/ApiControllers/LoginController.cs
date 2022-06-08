using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Service;
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
        [Route("~/api/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var data = await _unitOfWork.login.Login(model);
            return Ok(data);
        }
    }
}
