using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Binders;
using SportsStore.WebUI.Infrastructure;

namespace SportsStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null, // we don't need to specify a name
                "Page{page}",
                new {Controller = "Product", action = "List"}
                );

            routes.MapRoute(null,
                            "{category}", // Matches /Football or /AnythingWithNoSlash
                            new {controller = "Product", action = "List", page = 1}
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Product", action = "List", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}