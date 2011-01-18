using System.Collections.Generic;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

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
            return _pages.ContainsKey(name) ? _pages[name] : Page.EmptyPage;
        }

        public void SavePage(string name, Page page)
        {
            _pages[name] = page;
        }
    }
}