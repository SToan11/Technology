using ASM2.Areas.Admin.DAL;
using ASM2.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ASM2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductDAL _productDAL;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _productDAL = new ProductDAL(configuration);
        }

        public IActionResult Index()
        {
            List<Product> products = _productDAL.GetAllProducts().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile? FileImage)
        {
            if (ModelState.IsValid)
            {
                _productDAL.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int productId)
        {
            var product = _productDAL.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product, IFormFile? FileImage)
        {
            if (ModelState.IsValid)
            {
                if (FileImage != null && FileImage.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    string extension = Path.GetExtension(FileImage.FileName).ToLower();

                    if (allowedExtensions.Contains(extension))
                    {
                        string ImageName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss_") + Path.GetFileName(FileImage.FileName);
                        string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImageName);

                        using (var stream = new FileStream(SavePath, FileMode.Create))
                        {
                            FileImage.CopyTo(stream);
                        }
                        product.Image = ImageName;
                    }
                    else
                    {
                        ModelState.AddModelError("FileImage", "Invalid file type.");
                    }
                }
                _productDAL.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int productId)
        {
            var product = _productDAL.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int productId)
        {
            var product = _productDAL.GetProductById(productId);
            if (product != null)
            {
                _productDAL.DeleteProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Detail(int productId)
        {
            var product = _productDAL.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
