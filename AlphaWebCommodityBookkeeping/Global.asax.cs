using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
using System.Web.SessionState;
using AlphaWebCommodityBookkeeping.Controllers;
using DevExpress.Web.Mvc;
//using StackExchange.Profiling;

namespace AlphaWebCommodityBookkeeping
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogonAuthorize());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogIn", id = UrlParameter.Optional } // Parameter defaults
            );
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}.mvc/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);


            //routes.MapRoute(
            //        "Root",
            //        "",
            //new { controller = "Home", action = "Index", id = "" }

            //);

        }

        public class LogonAuthorize : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
           || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
                if (!skipAuthorization)
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }

        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new AlphaScoreModelBinder.AlphaScoreModelBinder();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

        }
        
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Handler is IRequiresSessionState)
            {
                if (Csla.ApplicationContext.AuthenticationType == "Windows")
                    return;

                IPrincipal principal;
                try
                {
                    principal = (IPrincipal)HttpContext.Current.Session["CslaPrincipal"];
                }
                catch
                {
                    principal = null;
                }

                if (principal == null)
                {
                    if (User.Identity.IsAuthenticated && User.Identity is FormsIdentity)
                    {
                        // no principal in session, but ASP.NET token
                        // still valid - so sign out ASP.NET
                        FormsAuthentication.SignOut();
                        Response.Redirect(Request.Url.PathAndQuery);
                    }
                    // didn't get a principal from Session, so
                    // set it to an unauthenticted PTPrincipal
                    BusinessObjects.Security.PTPrincipal.Logout();
                }
                else
                {
                    // use the principal from Session
                    Csla.ApplicationContext.User = principal;
                }
            }
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            DevExpressHelper.Theme = "DevEx";
        }

        //protected void Application_BeginRequest()
        //{
        //    if (Request.IsLocal)
        //    {
        //        MiniProfiler.Start();
        //    }
        //}

        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}

    }
}