using ASM2.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ASM2.Areas.Admin.DAL;

namespace ASM2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            AccountDAL _accountDAL = new AccountDAL(_configuration);

            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            Account account = _accountDAL.Login(userName, password);
            if (account !=null)
            {
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, account.Role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("Name", userName);
                HttpContext.Session.SetString("Role", account.Role);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Info = "Sai tên đăng nhập hoặc mật khẩu";
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
