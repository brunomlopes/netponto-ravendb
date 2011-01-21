using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Wikibird.Models
{
    public class Page
    {
        public static Func<string,Page> EmptyPage = name => new Page(){Name = name, Category = "Default"};

        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        [UIHint("WymEditor")]
        public string Content { get; set; }

        public IList<string> Tags { get; set; }
        public string Category { get; set; }

        public Page()
        {
            Tags = new List<string>();
        }
    }
}