using System.Linq;
using System.Web.Mvc;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

namespace Wikibird.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IPageService _pageService;

        public CategoryController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public ActionResult Index(string category)
        {
            var result = _pageService.ListCategory(category);

            return View(new CategoryViewModel { Category = category, Pages = result.Pages.ToArray(), TotalCount = result.TotalCount });
        }
    }
}