using Microsoft.AspNetCore.Mvc;

namespace RestWallAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}