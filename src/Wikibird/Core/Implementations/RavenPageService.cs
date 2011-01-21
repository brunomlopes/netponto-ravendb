using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Wikibird.Core.Abstractions;
using Wikibird.Core.Implementations.Indexes;
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

        public void SavePage(string name, string title, string content, string[] tags)
        {
            var page = GetPage(name);

            page.Name = name;
            page.Title = title;
            page.Content = content;
            page.Tags = tags;
            _session.Store(page);
        }

        public IEnumerable<PageTitle> GetPageNames()
        {
            return _session.Query<Page>()
                .Select(p => new PageTitle() { Name = p.Name, Title = p.Title });
        }

        public ListResult ListCategory(string category)
        {
            var result = _session.Advanced.LuceneQuery<Page, Pages_ByCategory>()
                .Where("Category:" + category);

            return new ListResult(result, new RavenQueryStatistics(){TotalResults = result.QueryResult.TotalResults});
        }

        public ListResult ListTag(string tag)
        {
            var result = _session.Advanced.LuceneQuery<Page, Pages_ByTag>()
                .Where("Tag:"+tag);

            return new ListResult(result, new RavenQueryStatistics() { TotalResults = result.QueryResult.TotalResults });
        }
    }
}
