using ASM2.Class;
using ASM2.DAL;
using ASM2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASM2.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                RegandLog userDAL = new RegandLog(_configuration);
                bool isRegistered = userDAL.Register(user.Email, user.Pass);

                if (isRegistered)
                {
                    ViewBag.Message = "Registration successful. You can now login.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Registration failed. Email may already be in use.";
                }
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                RegandLog userDAL = new RegandLog(_configuration);
                User loggedInUser = userDAL.Login(user.Email, user.Pass);

                if (loggedInUser != null)
                {
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    };
                    Response.Cookies.Append("username", user.Email, options);
                    ViewBag.Info = "Đăng nhập thành công";
                    HttpContext.Session.SetComplexData("UserData", loggedInUser);
                    return RedirectToAction("Index", "Product_");
                }
                else
                {
                    ViewBag.Info = "Sai email hoặc mật khẩu";
                }
            }
            return View(user);
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetComplexData<User>("UserData");
            if (user != null)
            {
                ViewBag.UserName = user.Email;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Xóa cookie
            Response.Cookies.Delete("username");

            // Xóa session
            HttpContext.Session.Clear();

            // Chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }
    }
    
}