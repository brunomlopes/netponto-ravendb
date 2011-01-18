using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Wikibird.Models
{
    public class Page
    {
        public static Page EmptyPage = new Page();

        public string Title { get; set; }

        [UIHint("WymEditor")]
        public string Content { get; set; }
    }
}