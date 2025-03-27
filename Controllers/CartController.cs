
using ASM2.Areas.Admin.DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
public class CartController : Controller
{
    private readonly CartService _cartService;
    private readonly ProductDAL _productDAL;

    public CartController(CartService cartService, ProductDAL productDAL)
    {
        _cartService = cartService;
        _productDAL = productDAL;
    }

    public IActionResult Index()
    {
        var cartItems = _cartService.GetCartItems();
        return View(cartItems);
    }

    [HttpPost]

    public IActionResult AddToCart(int productId, int quantity)
    {
        var product = _productDAL.GetProductById(productId);

        if (product == null)
        {

            return NotFound();
        }

        var cartItem = new CartItem
        {
            DetailImage = product.Image,
            ProductId = product.ProductId,
            ProductName = product.Name,
            Price = (decimal)product.UnitPrice,
            Quantity = quantity
        };


        _cartService.AddToCart(cartItem);
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        _cartService.RemoveFromCart(productId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateToCart(int productId, int quantity)
    {
        var cartItems = _cartService.GetCartItems();
        var existingItem = cartItems.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity = quantity;
            _cartService.UpdateCart(cartItems);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PlaceOrder()
    {
        _cartService.ClearCart();

        // Thông báo đặt hàng thành công
        TempData["SuccessMessage"] = "Đặt hàng thành công!";

        // Chuyển hướng về trang giỏ hàng
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SubmitOrderDetails()
    {
        if (ModelState.IsValid)
        {
            _cartService.ClearCart();
            // Xử lý lưu thông tin đơn hàng vào cơ sở dữ liệu
            TempData["SuccessMessage"] = "Đặt hàng thành công!";
            return RedirectToAction("Index","Product_");
        }

        // Nếu có lỗi, hiển thị lại form với lỗi.
        return View("Index");
    }



}
