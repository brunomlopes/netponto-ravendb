using System.Web.Mvc;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

namespace Wikibird.Controllers
{
    public class HomePageController : Controller
    {
        private const string _homePageName = "Homepage";

        private readonly IPageService _pageService;

        public HomePageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public ActionResult Index()
        {
            var page = _pageService.GetPage(_homePageName);
            return View(page);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var page = _pageService.GetPage(_homePageName);
            return View(page);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string title,  string content)
        {
            _pageService.SavePage(_homePageName, title, content);
            return RedirectToAction("Index");
        }
    }
}
