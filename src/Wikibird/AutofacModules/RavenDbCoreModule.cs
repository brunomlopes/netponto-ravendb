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
            builder.Register(
                ctx => new DocumentStoreFactory {ConnectionStringName = "RavenDbConnectionString"}.CreateDocumentStore())
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