using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.Models
{
    public class PublicQuestion
    {
        public int QuestionNumber { get; set; }

        public Question Question { get; set; }
    }
}