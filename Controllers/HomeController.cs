using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASM2.Models;

namespace ASM2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string? userName = Request.Cookies.ContainsKey("UserName") ? Request.Cookies["UserName"] : null;

        if (!string.IsNullOrEmpty(userName))
        {
            // Nếu có cookie lưu email, hiển thị lời chào kèm email
            ViewBag.Message = $"Xin chào  {userName}";
        }
        else
        {
            // Nếu không có cookie, hiển thị lời chào chung chung
            ViewBag.Message = "Xin chào bạn";
        }

        return View();
        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
