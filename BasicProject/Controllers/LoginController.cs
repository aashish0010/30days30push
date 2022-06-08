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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest login)
        {
            var data= await _unitOfWork.login.Login(login); 
            if(data.StatusCode == 400)
            {
                TempData["Message"] = "Error";
                return View();
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
