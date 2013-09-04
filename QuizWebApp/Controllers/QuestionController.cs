using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        public QuizWebAppDb DB { get; set; }

        public QuestionController()
        {
            this.DB = new QuizWebAppDb();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userId = User.Identity.UserId();
            var questins = this.DB.Questions
                .Where(q => q.OwnerUserId == userId)
                .OrderBy(q => q.CreateAt)
                .ToArray();
            return View(questins);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Question());
        }

        [HttpPost]
        public ActionResult Create(Question model)
        {
            if (IsValidDataURL(model) == false) throw new ApplicationException("Invalid Data URL.");

            if(ModelState.IsValid == false)
            {
                return View(model);
            }

            model.OwnerUserId = User.Identity.UserId();
            model.CreateAt = DateTime.UtcNow;
            this.DB.Questions.Add(model);
            this.DB.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var question = this.DB.Questions.Find(id);
            if (question.OwnerUserId != User.Identity.UserId())
                throw new Exception("Access Violation.");

            return View(question);
        }

        [HttpPost]
        public ActionResult Edit(int id, Question model)
        {
            var question = this.DB.Questions.Find(id);
            if (question.OwnerUserId != User.Identity.UserId())
                throw new Exception("Access Violation.");

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            UpdateModel(question, 
                prefix: null, 
                includeProperties: null,
                excludeProperties: new[] { "QuestionId", "OwnerUserId", "CreateAt" });

            if (IsValidDataURL(question) == false) throw new ApplicationException("Invalid Data URL.");

            this.DB.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool IsValidDataURL(Question model)
        {
            return model.GetOptions(trim: false)
                .Select(opt => opt.OptionImage ?? "")
                .All(url => Regex.IsMatch(url, @"(^data:image/\w+;\w+,[0-9a-zA-Z/+=]+$)|(^$)"));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var question = this.DB.Questions.Find(id);
            if (question.OwnerUserId != User.Identity.UserId())
                throw new Exception("Access Violation.");

            return View(question);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection _)
        {
            var question = this.DB.Questions.Find(id);
            if (question.OwnerUserId != User.Identity.UserId())
                throw new Exception("Access Violation.");

            this.DB.Questions.Remove(question);
            this.DB.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}