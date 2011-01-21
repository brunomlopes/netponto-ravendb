using System.Linq;
using System.Web.Mvc;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

namespace Wikibird.Controllers
{
    public class TagController : Controller
    {
        private readonly IPageService _pageService;

        public TagController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public ActionResult Index(string tag)
        {
            var result = _pageService.ListTag(tag);

            return View(new TagViewModel { Tag = tag, Pages = result.Pages.ToArray(), TotalCount = result.TotalCount });
        }
    }
}