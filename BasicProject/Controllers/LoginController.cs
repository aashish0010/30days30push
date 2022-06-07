using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;

namespace BasicProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }
        public async Task<IActionResult> Index(LoginRequest login)
        {
            var data= await _unitOfWork.login.Login(login);
            return View();
        }
    }
}
