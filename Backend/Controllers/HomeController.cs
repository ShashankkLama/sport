using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Your logic for displaying the home page (e.g., list of recent blog posts)
            return View();
        }

        public IActionResult About()
        {
            // Your logic for displaying the About page
            return View();
        }
    }
}
