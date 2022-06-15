using Entity.Model;
using Entity.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Linq;
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
            LoginValidator valid = new LoginValidator();
            var res = valid.Validate(login);
            if(!res.IsValid)
            {
                TempData["Message"]= res.Errors[0].ErrorMessage;
                TempData["Flag"] = "error";
                return View();
            };

            if(data.StatusCode == 400)
            {
                TempData["Message"] = data.Message;
                TempData["Flag"] = "error";
                return View();
            }
            else
            {
                TempData["Message"] = "Login Successfully..";
                TempData["Flag"] = "success";
                HttpContext.Session.SetString("username",login.UserName);
                TempData["CurrentUser"]= HttpContext.Session.GetString("username");
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            var data = await _unitOfWork.login.Register(register);
            RegisterValidator valid = new RegisterValidator();
            var res = valid.Validate(register);
            if (!res.IsValid)
            {
                TempData["Message"] = res.Errors[0].ErrorMessage;
                TempData["Flag"] = "error";
                return View();
            }
            else if(data.Count() !=0)
            {
                if (data.FirstOrDefault().StatusCode == 400)
                {
                    TempData["Message"] = data.FirstOrDefault().Message;
                    TempData["Flag"] = "error";
                    return View();
                }
                else
                {
                    TempData["Message"] = data.FirstOrDefault().Message;
                    TempData["Flag"] = "success";
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                TempData["Message"] = "Technical Error..!!";
                TempData["Flag"] = "error";
                return View();
            }
            
            
        }
        public async Task<IActionResult> GetByIdUser(string username)
        {
            var data= await _unitOfWork.login.GetUserById(username);
            return Json(data.FirstOrDefault());
        }
    }
}
