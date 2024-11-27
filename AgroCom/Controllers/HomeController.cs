using Microsoft.AspNetCore.Mvc;

namespace AgroCom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get() =>
            Ok("Hello,Mario.");
    }
}
