using ASM2.Areas.Admin.DAL;
using ASM2.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASM2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserDAL userDAL;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            userDAL = new UserDAL(configuration);
        }
        public IActionResult Index()
        {
            List<User> users = userDAL.GetAllUsers().ToList();
            return View(users);
        }
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userDAL.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var user = userDAL.GetUserById(Id);
            if (user == null)
            {
                return NotFound(); 
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userDAL.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user); 
        }

        [HttpGet] 
        public IActionResult Delete(int Id)
        {
            var user = userDAL.GetUserById(Id);
            if (user == null)
            {
                return NotFound(); 
            }
            return View(user);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int Id)
        {
            var user = userDAL.GetUserById(Id);
            if (user != null)
            {
                userDAL.DeleteUser(user); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(user);  
        }




        [HttpGet]
        public IActionResult Detail(int id)
        {
            var user = userDAL.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
