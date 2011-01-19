using Autofac;
using Raven.Client;
using Raven.Client.Document;
using Autofac.Integration.Mvc;
using Wikibird.Core.Abstractions;
using Wikibird.Core.Implementations;

namespace Wikibird.AutofacModules
{
    public class RavenDbCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new DocumentStore {ConnectionStringName = "RavenDbConnectionString"}.Initialize())
                .As<IDocumentStore>();

            builder.RegisterAdapter<IDocumentStore, IDocumentSession>(store => store.OpenSession())
                .InstancePerHttpRequest()
                .OnRelease(session => session.SaveChanges());

            builder.RegisterType<RavenPageService>()
                .As<IPageService>()
                .InstancePerHttpRequest();
        }
    }
}