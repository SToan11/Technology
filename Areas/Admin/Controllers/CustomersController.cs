using ASM2.Areas.Admin.DAL;
using ASM2.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASM2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly CustomersDAL _customersDAL;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _customersDAL = new CustomersDAL(configuration);
        }

        public IActionResult Index()
        {
            List<Customers> customers = _customersDAL.GetAllCustomers().ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _customersDAL.AddCustomer(customer); 
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int customerId)
        {
            var customer = _customersDAL.GetCustomerById(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _customersDAL.UpdateCustomer(customer);
                return RedirectToAction("Index"); 
            }
            return View(customer); 
        }

        [HttpGet]
        public IActionResult Delete(int customerId)
        {
            var customer = _customersDAL.GetCustomerById(customerId);
            if (customer == null)
            {
                return NotFound(); 
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int customerId)
        {
            var customer = _customersDAL.GetCustomerById(customerId);
            if (customer != null)
            {
                _customersDAL.DeleteCustomer(customer); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(customer); 
        }

        [HttpGet]
        public IActionResult Detail(int customerId)
        {
            var customer = _customersDAL.GetCustomerById(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer); 
        }

    }
}
