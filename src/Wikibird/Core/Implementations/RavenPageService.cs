using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Wikibird.Core.Abstractions;
using Wikibird.Models;

namespace Wikibird.Core.Implementations
{
    public class RavenPageService : IPageService
    {
        private readonly IDocumentSession _session;

        public RavenPageService(IDocumentSession session)
        {
            _session = session;
        }

        public Page GetPage(string name)
        {
            return _session.Load<Page>(name.AsPageId()) ?? Page.EmptyPage(name);
        }

        public void SavePage(string name, string title, string content)
        {
            var page = GetPage(name);

            page.Name = name;
            page.Title = title;
            page.Content = content;
            _session.Store(page);
        }

        public IEnumerable<PageTitle> GetPageNames()
        {
            return _session.Query<Page>()
                .Select(p => new PageTitle() { Name = p.Name, Title = p.Title });
        }
    }
}
