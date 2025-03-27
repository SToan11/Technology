using ASM2.Areas.Admin.DAL;
using ASM2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASM2.Controllers
{
    public class Product_Controller : Controller
    {
        private readonly ProductDAL _productDAL;

        public Product_Controller(ProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public IActionResult Index(string searchString)
        {
            var productsS = _productDAL.GetAllProducts();

            if (!string.IsNullOrEmpty(searchString))
            {
                productsS = productsS.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var viewModel = new Pro_View
            {
                Products_C = productsS,
            };

            return View(viewModel);
        }

        public IActionResult Detail(int id)
        {
            var product = _productDAL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
