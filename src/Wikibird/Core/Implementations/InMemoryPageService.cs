using System;
using System.Collections.Generic;
using Wikibird.Core.Abstractions;
using Wikibird.Models;
using System.Linq;

namespace Wikibird.Core.Implementations
{
    class InMemoryPageService : IPageService
    {
        private Dictionary<string, Page> _pages;

        public InMemoryPageService()
        {
            _pages = new Dictionary<string, Page>();
        }

        public Page GetPage(string name)
        {
            return _pages.ContainsKey(name) ? _pages[name] : Page.EmptyPage(name);
        }

        public void SavePage(string name, string title, string content, string[] tags)
        {
            Page page;
            if(!_pages.TryGetValue(name, out page))
            {
                page = Page.EmptyPage(name);
            }
            page.Name = name;
            page.Title = title;
            page.Content = content;
            _pages[name] = page;
        }

        public IEnumerable<PageTitle> GetPageNames()
        {
            return _pages.Values.Select(page => new PageTitle {Name = page.Name, Title = page.Title});
        }

        public ListResult ListCategory(string category)
        {
            return new ListResult();
        }

        public ListResult ListTag(string tag)
        {
            return new ListResult();
        }
    }
}