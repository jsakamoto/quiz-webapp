using System;
using System.Linq;
using System.Web.Http;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    public class PublicQuestionsController : ApiController
    {
        private PlayCode2013QuizDB db = new PlayCode2013QuizDB();

        // GET api/Questions
        [Queryable]
        public IQueryable<PublicQuestion> GetQuestions()
        {
            return db.GetPublicQuestions().AsQueryable();
        }

        [HttpGet]
        public int Count()
        {
            return db.GetPublicQuestions().Count();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}