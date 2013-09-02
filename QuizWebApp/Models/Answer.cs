using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayCode2013Quiz.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }

        public int PlayerID { get; set; }

        public int QuestionID { get; set; }

        public int ChoosedOptionIndex { get; set; }

        /// <summary>
        /// 0: no entry.
        /// 1: pending.
        /// 2: correct.
        /// 3: incorrect.
        /// </summary>
        public AnswerStateType Status { get; set; }
    }
}