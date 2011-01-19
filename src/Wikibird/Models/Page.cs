using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Wikibird.Models
{
    public class Page
    {
        public static Func<string,Page> EmptyPage = name => new Page(){Name = name};

        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        [UIHint("WymEditor")]
        public string Content { get; set; }
    }
}