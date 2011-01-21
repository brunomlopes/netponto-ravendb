using System.Linq;
using Raven.Client.Indexes;
using Raven.Database.Indexing;
using Wikibird.Models;

namespace Wikibird.Core.Implementations.Indexes
{
    public class Pages_ByTag : AbstractIndexCreationTask<Page>
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<Page>
                       {
                           Map = pages => from page in pages
                                          from tag in page.Tags
                                          select new
                                                     {
                                                         Tag = tag
                                                     }
                       }.ToIndexDefinition(DocumentStore.Conventions);
        }
    }
}