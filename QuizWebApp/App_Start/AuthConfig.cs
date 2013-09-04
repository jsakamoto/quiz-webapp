using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;

namespace QuizWebApp
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            var oauthMicrosoftSetting = JsonAppSettings.AsDictionary("OAuth.Microsoft");
            if (oauthMicrosoftSetting != null)
            {
                OAuthWebSecurity.RegisterMicrosoftClient(
                    oauthMicrosoftSetting["clientId"],
                    oauthMicrosoftSetting["clientSecret"]);
            }

            var oauthTwitterSetting = JsonAppSettings.AsDictionary("OAuth.Twitter");
            if (oauthTwitterSetting != null)
            {
                OAuthWebSecurity.RegisterTwitterClient(
                    oauthTwitterSetting["consumerKey"],
                    oauthTwitterSetting["consumerSecret"]);
            }

            var oauthFacebookSetting = JsonAppSettings.AsDictionary("OAuth.facebook");
            if (oauthFacebookSetting != null)
            {
                OAuthWebSecurity.RegisterFacebookClient(
                    oauthFacebookSetting["appId"],
                    oauthFacebookSetting["appSecret"]);
            }

            OAuthWebSecurity.RegisterGoogleClient();

            // TODO: GitHub Account Support.
            //var oauthGitHubSetting = JsonAppSettings.AsDictionary("OAuth.GitHub");
            //OAuthWebSecurity.RegisterClient(new GitHubOAuth2Client(
            //    oauthGitHubSetting["clientId"],
            //    oauthGitHubSetting["clientSecret"]), "GitHub", null);
        }
    }
}