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

        private readonly CreateViewModel _createVM = new();

        public UnReaLController(IUnReaLService appService, IBijectionService bijectionService)
        {
            _appService = appService;
            _bijectionService = bijectionService;
        }

        public IActionResult Index() => RedirectToAction(actionName: nameof(Create));

        public IActionResult Create() => View(_createVM);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShortURL shortUrl)
        {
            _createVM.ShortURL = shortUrl;
            _createVM.ErrorString = _createVM.ValidateInput(shortUrl.Url);

            TryValidateModel(_createVM);
            if (ModelState.IsValid && string.IsNullOrEmpty(_createVM.ErrorString))
            {
                var savedItemId = _appService.Save(_createVM.ShortURL);

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = savedItemId });
            }

            return View(_createVM);
        }

        public IActionResult Show(int? id)
        {
            if (!id.HasValue) { return NotFound(); }

            var shortUrl = _appService.GetById(id.Value);
            if (shortUrl == null) { return NotFound(); }

            ViewData["Path"] = _bijectionService.Encode(shortUrl.Id);

            return View(shortUrl);
        }

        [HttpGet("/UnReaL/Go/{path:required}", Name = "UnReaL_Go")]
        public IActionResult Go(string path)
        {
            if (path == null) { return NotFound(); }

            var shortUrl = _appService.GetByPath(path);
            if (shortUrl == null) { return NotFound(); }

            return Redirect(shortUrl.Url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
