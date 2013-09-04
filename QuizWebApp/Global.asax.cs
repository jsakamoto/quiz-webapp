using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using QuizWebApp.Models;

namespace QuizWebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteTable.Routes.MapHubs();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            Database.SetInitializer(new CreateDatabaseIfNotExists<QuizWebAppDb>());

            using (var db = new QuizWebAppDb())
            {
                if (db.Contexts.Any() == false)
                {
                    db.Contexts.Add(new Context { CurrentQuestionID = 1, CurrentState = ContextStateType.PleaseWait });
                    db.SaveChanges();
                }
            }
        }
    }
}