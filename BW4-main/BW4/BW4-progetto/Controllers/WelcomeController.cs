using Microsoft.AspNetCore.Mvc;

namespace BW4_progetto.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
