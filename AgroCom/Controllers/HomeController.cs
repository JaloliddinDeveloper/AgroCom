using Microsoft.AspNetCore.Mvc;

namespace AgroCom.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
