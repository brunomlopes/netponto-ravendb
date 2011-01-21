using System.Linq;
using Raven.Client.Indexes;
using Raven.Database.Indexing;
using Wikibird.Models;

namespace Wikibird.Core.Implementations.Indexes
{
    public class Pages_ByCategory : AbstractIndexCreationTask<Page>
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<Page>
                       {
                           Map = pages => from page in pages
                                          select new
                                                     {
                                                         page.Category
                                                     }
                       }.ToIndexDefinition(DocumentStore.Conventions);
        }
    }
}