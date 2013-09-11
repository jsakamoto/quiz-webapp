using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizWebApp.Models;

namespace QuizWebApp.Code
{
    public class AuthorizeQuizMaster : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeQuizMaster.IsAllow(httpContext);
        }

        public static bool IsAllow(HttpContextBase httpContext)
        {
            var userIdentity = httpContext.User.Identity;
            if (userIdentity.IsAuthenticated == false) return false;

            // If no configuration about Quiz Master,
            // then all users can be Quiz Master.
            var setting = JsonAppSettings.AsDictionary("QuizMaster");
            if (setting == null) return true;

            using (var db = new QuizWebAppDb())
            {
                var userInfo = db.Users.Find(userIdentity.UserId());
                if (userInfo == null) return false;

                return
                    setting["idProviderName"] == userInfo.IdProviderName &&
                    setting["name"] == userInfo.Name;
            }
        }
    }
}