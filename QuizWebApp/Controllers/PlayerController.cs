using System;
using System.Linq;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        public PlayCode2013QuizDB DB { get; set; }

        public PlayerController()
        {
            this.DB = new PlayCode2013QuizDB();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(this.DB);
        }

        [HttpGet]
        public ActionResult PlayerMainContent()
        {
            var context = this.DB.Contexts.First();
            var playerID = this.User.Identity.UserId();
            var questionID = this.DB.Contexts.First().CurrentQuestionID;
            var ansewer = this.DB.Answers.FirstOrDefault(a => a.PlayerID == playerID && a.QuestionID == questionID);
            if (ansewer == null)
            {
                ansewer = new Answer { PlayerID = playerID, QuestionID = questionID, ChoosedOptionIndex = -1 };
                this.DB.Answers.Add(ansewer);
                this.DB.SaveChanges();
            }

            return PartialView("PlayerMainContent_" + context.CurrentState.ToString(), this.DB);
        }
    }
}
