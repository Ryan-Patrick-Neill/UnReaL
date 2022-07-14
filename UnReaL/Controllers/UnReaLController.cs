using Microsoft.AspNetCore.Mvc;
using UnReaL.Repository;

namespace UnReaL.Controllers
{
    public class UnReaLController : Controller
    {
        private readonly IUnReaLService _appService;
        private readonly IBijectionService _bijectionService;

        public UnReaLController(IUnReaLService appService, IBijectionService bijectionService)
        {
            _appService = appService;
            _bijectionService = bijectionService;
        }

        public IActionResult Index() => RedirectToAction(actionName: nameof(Create));

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string originalURL)
        {
            throw new NotImplementedException();
        }
    }
}
