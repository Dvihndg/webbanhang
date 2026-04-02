using Microsoft.AspNetCore.Mvc;

namespace WebBanHangMvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
