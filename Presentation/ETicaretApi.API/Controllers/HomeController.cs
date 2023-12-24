using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
