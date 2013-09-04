using System;
using System.Linq;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    public class PublicQuestionController : Controller
    {
        public QuizWebAppDb DB { get; set; }

        public PublicQuestionController()
        {
            this.DB = new QuizWebAppDb();
        }

        public ActionResult Index()
        {
            var publicQuestions = this.DB.GetPublicQuestions();
            return View(publicQuestions);
        }

        public ActionResult Detail(int id)
        {
            var publicQuestion = this.DB.GetPublicQuestions()
                .First(pq => pq.Question.QuestionId == id);
            return View(publicQuestion);
        }

        protected override void Dispose(bool disposing)
        {
            DB.Dispose();
            base.Dispose(disposing);
        }
    }
}