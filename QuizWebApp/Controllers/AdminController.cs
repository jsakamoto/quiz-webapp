using System;
using System.Linq;
using System.Web.Mvc;
using QuizWebApp.Code;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    [AuthorizeQuizMaster]
    public class AdminController : Controller
    {
        public QuizWebAppDb DB { get; set; }

        public AdminController()
        {
            this.DB = new QuizWebAppDb();
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
