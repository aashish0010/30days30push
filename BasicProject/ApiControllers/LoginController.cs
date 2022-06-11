using Entity.Model;
using Entity.Validator;
using FluentValidation.Results;
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
        [Route("~/api/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var data = await _unitOfWork.login.Login(model);
            var common = new CommonResponse();
            LoginValidator valid = new LoginValidator();
            var res=valid.Validate(model);
            if(!res.IsValid)
            {
                common.StatusCode = 400;
                common.Message = res.Errors[0].ErrorMessage;
                return Ok(common);
            };

            if (data.StatusCode == 400)
            {
                common.StatusCode = data.StatusCode;
                common.Message = data.Message;
                return Ok(common);
            }
            return Ok(data);
        }

        [Route("~/api/getbyid")]
        [HttpGet]
        public async Task<IEnumerable<GetByIdLogin>> GetUserById(string username)
        {
            return await _unitOfWork.login.GetUserById(username);
        }
    }
}
