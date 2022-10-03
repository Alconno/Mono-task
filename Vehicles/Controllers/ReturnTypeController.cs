using Microsoft.AspNetCore.Mvc;

namespace Vehicles.Controllers
{
    public class ReturnTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
