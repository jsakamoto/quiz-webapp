using System;
using System.Linq;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    public class QuestionController : Controller
    {
        public PlayCode2013QuizDB DB { get; set; }

        public QuestionController()
        {
            this.DB = new PlayCode2013QuizDB();
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