using System;
using System.Linq;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    public class DashboardController : Controller
    {
        public PlayCode2013QuizDB DB { get; set; }

        public DashboardController()
        {
            this.DB = new PlayCode2013QuizDB();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(this.DB);
        }

        [HttpGet]
        public ActionResult LatestDashboard()
        {
            return PartialView("DashboardMainContent", this.DB);
        } 
    }
}
