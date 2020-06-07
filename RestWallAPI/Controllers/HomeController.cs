using Microsoft.AspNetCore.Mvc;

namespace RestWallAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return RedirectToAction("GetRoot", "Root");
            return View();
        }
    }
}