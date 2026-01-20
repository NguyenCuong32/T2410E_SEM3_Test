using Microsoft.AspNetCore.Mvc;

namespace comicsys.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
