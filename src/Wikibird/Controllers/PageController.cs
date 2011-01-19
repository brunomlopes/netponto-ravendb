using System.Linq;
using System.Web.Mvc;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

namespace Wikibird.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public ActionResult Index(string pageName)
        {
            var page = _pageService.GetPage(pageName);
            var pageNames = _pageService.GetPageNames();
            return View(new PageViewModel {Page = page, PageNames = pageNames.ToArray() });
        }

        [HttpGet]
        public ActionResult Edit(string pageName)
        {
            var page = _pageService.GetPage(pageName);
            return View(page);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string pageName, string title, string content)
        {
            _pageService.SavePage(pageName, title, content);
            return RedirectToAction("Index");
        }
    }
}
