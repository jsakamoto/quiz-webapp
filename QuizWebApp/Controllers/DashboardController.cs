using System;
using System.Linq;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    public class DashboardController : Controller
    {
        public QuizWebAppDb DB { get; set; }

        public DashboardController()
        {
            this.DB = new QuizWebAppDb();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new DashboardViewModel(this.DB);
            return View(model);
        }

        [HttpGet]
        public ActionResult LatestDashboard()
        {
            var model = new DashboardViewModel(this.DB);
            return PartialView("DashboardMainContent", model);
        }

        protected override void Dispose(bool disposing)
        {
            this.DB.Dispose();
            base.Dispose(disposing);
        }
    }
}
