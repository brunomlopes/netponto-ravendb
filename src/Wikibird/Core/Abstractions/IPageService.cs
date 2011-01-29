using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Linq;
using Wikibird.Models;

namespace Wikibird.Core.Abstractions
{
    public interface IPageService
    {
        Page GetPage(string name);
        void SavePage(string name, string title, string content, string[] tags);
        IEnumerable<PageTitle> GetPageNames();
        ListResult ListCategory(string category);
        ListResult ListTag(string tag);
    }

    public class ListResult
    {
        private readonly RavenQueryStatistics _stats;

        public ListResult(IEnumerable<Page> pages, RavenQueryStatistics stats)
        {
            _stats = stats;
            Pages = pages;
        }

        public IEnumerable<Page> Pages { get; set; }
        public int TotalCount { get { return _stats.TotalResults; } }
    }
    
}