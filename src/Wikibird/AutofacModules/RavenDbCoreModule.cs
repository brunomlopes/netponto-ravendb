using System;
using System.Reflection;
using Autofac;
using Raven.Client;
using Raven.Client.Document;
using Autofac.Integration.Mvc;
using Wikibird.Core.Abstractions;
using Wikibird.Core.Implementations;
using Wikibird.Models;
using Module = Autofac.Module;

namespace Wikibird.AutofacModules
{
    public class RavenDbCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
                                 {
                                     IDocumentStore documentStore = new DocumentStore {ConnectionStringName = "RavenDbConnectionString"};
                                     SetupKeyGeneratorForPage(documentStore);
                                     return documentStore.Initialize();
                                 })
                .As<IDocumentStore>();

            builder.RegisterAdapter<IDocumentStore, IDocumentSession>(store => store.OpenSession())
                .InstancePerHttpRequest()
                .OnRelease(session => session.SaveChanges());

            builder.RegisterType<RavenPageService>()
                .As<IPageService>()
                .InstancePerHttpRequest();
        }

        private void SetupKeyGeneratorForPage(IDocumentStore documentStore)
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