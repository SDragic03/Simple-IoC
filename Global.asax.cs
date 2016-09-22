using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IoCWebApp.Classes;
using IoCWebApp.Factories;
using IoCWebApp.Intefaces;

namespace IoCWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));

            ControllerFactory.Container.Register<IMessage, Message>(Lifecycle.Transient);
            ControllerFactory.Container.Register<IContactInfo, ContactInfo>(Lifecycle.Singleton);
        }
    }
}
