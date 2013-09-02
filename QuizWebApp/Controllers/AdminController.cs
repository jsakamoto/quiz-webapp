using System;
using System.Linq;
using System.Web.Mvc;
using PlayCode2013Quiz.Models;

namespace PlayCode2013Quiz.Controllers
{
    public class AdminController : Controller
    {
        public PlayCode2013QuizDB DB { get; set; }

        public AdminController()
        {
            this.DB = new PlayCode2013QuizDB();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(this.DB);
        }

        [HttpGet]
        public ActionResult QuestionBody()
        {
            var context = this.DB.Contexts.First();
            var curQuestion = this.DB.Questions.Find(context.CurrentQuestionID);
            return PartialView(curQuestion);
        }

        [HttpPost]
        public ActionResult CurrentQuestion(int questionID)
        {
            this.DB.Contexts.First().CurrentQuestionID = questionID;
            this.DB.SaveChanges();

            return Json(new { });
        }
    }
}
