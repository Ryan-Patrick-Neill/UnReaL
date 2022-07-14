using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UnReaL.Models;
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
        public IActionResult Create(string url)
        {
            var shortUrl = new ShortURL { Url = url };

            TryValidateModel(shortUrl);
            if (ModelState.IsValid)
            {
                var savedItemId = _appService.Save(shortUrl);

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = savedItemId });
            }

            return View(shortUrl);
        }

        public IActionResult Show(int? id)
        {
            if (!id.HasValue) { return NotFound(); }

            var shortUrl = _appService.GetById(id.Value);
            if (shortUrl == null) { return NotFound(); }

            ViewData["Path"] = _bijectionService.Encode(shortUrl.Id);

            return View(shortUrl);
        }

        [HttpGet("/UnReaL/RedirectTo/{path:required}", Name = "UnReaL_RedirectTo")]
        public IActionResult RedirectTo(string path)
        {
            if (path == null) { return NotFound(); }

            var shortUrl = _appService.GetByPath(path);
            if (shortUrl == null) { return NotFound(); }

            return Redirect(shortUrl.Url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
