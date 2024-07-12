using Microsoft.AspNetCore.Mvc;

namespace BW4_progetto.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
