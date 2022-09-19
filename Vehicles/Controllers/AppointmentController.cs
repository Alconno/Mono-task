using Microsoft.AspNetCore.Mvc;
using System;

namespace Vehicles.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
