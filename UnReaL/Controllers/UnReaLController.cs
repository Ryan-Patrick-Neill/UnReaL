using Microsoft.AspNetCore.Mvc;

namespace UnReaL.Controllers
{
    public class UnReaLController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
