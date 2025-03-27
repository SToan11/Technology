using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CartService
{
    private const string CartSessionKey = "CartItems";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Lấy danh sách sản phẩm trong giỏ hàng từ session
    public List<CartItem> GetCartItems()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        string cartJson = session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(cartJson))
        {
            return new List<CartItem>();
        }
        return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
    }

    // Thêm sản phẩm vào giỏ hàng
    public void AddToCart(CartItem item)
    {
        var cartItems = GetCartItems();
        var existingItem = cartItems.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            cartItems.Add(item);
        }
        SaveCartItems(cartItems);
    }

    // Xóa sản phẩm khỏi giỏ hàng
    public void RemoveFromCart(int productId)
    {
        var cartItems = GetCartItems();
        var itemToRemove = cartItems.FirstOrDefault(i => i.ProductId == productId);
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            SaveCartItems(cartItems);
        }
    }

    // Cập nhật số lượng sản phẩm trong giỏ hàng
    public void UpdateCart(List<CartItem> cartItems)
    {
        SaveCartItems(cartItems);
    }

    // Lưu giỏ hàng vào session
    private void SaveCartItems(List<CartItem> cartItems)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        string cartJson = JsonConvert.SerializeObject(cartItems);
        session.SetString(CartSessionKey, cartJson);
    }

    // Xóa toàn bộ giỏ hàng
    public void ClearCart()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.Remove(CartSessionKey);
    }
}
