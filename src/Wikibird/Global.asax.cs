using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Wikibird.AutofacModules;

namespace Wikibird
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Edit", // Route name
                "edit/{*pageName}", // URL with parameters
                new { controller = "Page", action = "Edit", pageName = "Homepage" }
                );
            
            routes.MapRoute(
                "Category", // Route name
                "category/{*category}", // URL with parameters
                new { controller = "Category", action = "Index", category = "Default" }
                );
            
            routes.MapRoute(
                "Tag", // Route name
                "tag/{*tag}", // URL with parameters
                new { controller = "Tag", action = "Index", tag = "Default" }
                );
            
            routes.MapRoute(
                "New", // Route name
                "new/{*pageName}", // URL with parameters
                new { controller = "Page", action = "New", pageName = "Homepage" }
                );

            routes.MapRoute(
                "Default", // Route name
                "{*pageName}", // URL with parameters
                new { controller = "Page", action = "Index", pageName = "Homepage" }
            );
        }

        private static void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterModule(new RavenDbCoreModule());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ConfigureAutofac();
        }
    }
}