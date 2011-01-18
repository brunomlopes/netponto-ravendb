using Autofac;
using Wikibird.Core.Abstractions;
using Wikibird.Core.Implementations;

namespace Wikibird.AutofacModules
{
    public class InMemoryCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryPageService>()
                .As<IPageService>()
                .SingleInstance();
        }
    }
}