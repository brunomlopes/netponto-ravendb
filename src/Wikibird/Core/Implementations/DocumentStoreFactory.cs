using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Wikibird.Models;

namespace Wikibird.Core.Implementations
{
    public class DocumentStoreFactory
    {
        public string ConnectionStringName { get; set; }
        public string Url { get; set; }

        public IDocumentStore CreateDocumentStore()
        {
            DocumentStore documentStore = new DocumentStore();
            if (!string.IsNullOrWhiteSpace(ConnectionStringName))
                documentStore.ConnectionStringName = ConnectionStringName;
            if (!string.IsNullOrWhiteSpace(Url))
                documentStore.Url = Url;
            SetupKeyGeneratorForPage(documentStore);
            documentStore.Initialize();
            IndexCreation.CreateIndexes(this.GetType().Assembly, documentStore);
            return documentStore;
        }

        private static void SetupKeyGeneratorForPage(IDocumentStore documentStore)
        {
            var defaultKeyGenerator = documentStore.Conventions.DocumentKeyGenerator;
            documentStore.Conventions.DocumentKeyGenerator = obj =>
            {
                var page = obj as Page;
                if (page != null)
                {
                    return page.Name.AsPageId();
                }
                return defaultKeyGenerator(obj);
            };
        }
    }
}