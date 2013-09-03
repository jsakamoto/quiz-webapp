using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.Models
{
    public class Context
    {
        public int ContextID { get; set; }

        public int CurrentQuestionID { get; set; }

        /// <summary>
        /// 0: "please wait."
        /// 1: "choice the answer."
        /// 2: "closed."
        /// 3: "show correct answer."
        /// </summary>
        public ContextStateType CurrentState { get; set; }
    }
}