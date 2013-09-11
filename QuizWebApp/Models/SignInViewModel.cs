using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Web.WebPages.OAuth;

namespace QuizWebApp.Models
{
    public class SignInViewModel
    {
        public bool ExternalAuth { get; private set; }

        public ICollection<AuthenticationClientData> RegisteredClientData { get; set; }

        [Display(Name="ハンドル名"), Required]
        public string HandleName { get; set; }

        public SignInViewModel()
        {
            var extAuthStr = ConfigurationManager.AppSettings["ExternalAuthentication"] ?? "false";
            this.ExternalAuth = bool.Parse(extAuthStr);

            this.RegisteredClientData = OAuthWebSecurity.RegisteredClientData;
        }
    }
}