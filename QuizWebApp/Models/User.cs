using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.Models
{
    public class User
    {
        public string UserId { get; set; }

        public string IdProviderName { get; set; }

        public string Name { get; set; }

        public DateTime? AttendAsPlayerAt { get; set; }
    }
}