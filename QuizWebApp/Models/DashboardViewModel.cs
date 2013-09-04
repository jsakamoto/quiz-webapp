using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<User> Players { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        public DashboardViewModel(QuizWebAppDb db)
        {
            this.Answers = db.Answers.ToArray();
            this.Questions = db.Questions.ToArray();

            var users = db.Users.ToArray();
            this.Players = users
                .Where(user =>
                    this.Answers.Any(a => a.PlayerID == user.UserId) ||
                    DateTime.UtcNow.AddMinutes(-30) <= user.AttendAsPlayerAt
                )
                .OrderBy(user => user.Name)
                .ToArray();
        }
    }
}