using System;
using System.Configuration;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using QuizWebApp.Code;
using QuizWebApp.Models;

namespace QuizWebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            if (model.ExternalAuth) return Redirect("~");

            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            var result = new AuthenticationResult(isSuccessful: true,
                provider: "local",
                providerUserId: model.HandleName,
                userName: model.HandleName,
                extraData: null);
            RegistUserAndIssueAuthCookie(result);

            return Redirect("~/");
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                using (var db = new QuizWebAppDb())
                {
                    var userInfo = db.Users.Find(this.User.Identity.UserId());
                    if (userInfo != null)
                    {
                        userInfo.AttendAsPlayerAt = null;
                        db.SaveChanges();
                    }
                }
                FormsAuthentication.SignOut();
            }
            return Json(new { url = this.Url.Content("~/") });
        }

        [HttpPost]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new LamdaResult(_ =>
            {
                OAuthWebSecurity.RequestAuthentication(
                    provider,
                    Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            });
        }

        [HttpGet]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result = OAuthWebSecurity.VerifyAuthentication(
                Url.Action("ExternalLoginCallback",
                new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return Redirect("~/");
            }

            RegistUserAndIssueAuthCookie(result);

            return Redirect("~/");
        }

        private void RegistUserAndIssueAuthCookie(AuthenticationResult result)
        {
            var salt = ConfigurationManager.AppSettings["SaltOfUserID"];
            var user = new QuizWebApp.Models.User
            {
                UserId = GetHashedText(string.Join("@", salt, result.ProviderUserId, result.Provider)),
                IdProviderName = result.Provider,

                // terrible hack...
                Name = result.Provider != "github" ? result.UserName : result.ExtraData["login"]
            };

            using (var db = new QuizWebApp.Models.QuizWebAppDb())
            {
                if (db.Users.Find(user.UserId) == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }

            var cookie = FormsAuthentication.GetAuthCookie(user.Name, false);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ticket.GetType().InvokeMember("_UserData",
                BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance,
                null, ticket, new object[] { user.UserId });
            cookie.Value = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(cookie);
        }

        private string GetHashedText(string text)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
    }
}
