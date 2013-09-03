using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizWebApp.Models;
using MarkdownDeep;

namespace QuizWebApp.Helpers
{
    public static class Helper
    {
        private static Markdown _Formatter = new Markdown
        {
            MarkdownInHtml = true,
            NewWindowForExternalLinks = true,
            SafeMode = true,
            FormatCodeBlock = (a, b) => "<pre><code>" + HttpUtility.HtmlDecode(b) + "</code></pre>"
        };

        public static IHtmlString FormatToHtml(this HtmlHelper helper, string text, TextFormatType format)
        {
            switch (format)
            {
                case TextFormatType.PlainText:
                    return helper.Raw(HttpUtility.HtmlEncode(text ?? "").Replace("\n", "<br />"));
                case TextFormatType.MarkDown:
                    lock (_Formatter)
                    {
                        return helper.Raw(_Formatter.Transform(HttpUtility.HtmlEncode(text ?? "")));
                    }
                default: throw new NotImplementedException();
            }
        }

        public static IHtmlString ToLocalDateTimeString(this DateTime utc)
        {
            var timeZoneId = ConfigurationManager.AppSettings["TimeZone"];
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return new MvcHtmlString(TimeZoneInfo.ConvertTimeFromUtc(utc, timeZoneInfo).ToString("g"));
        }

        public static string AppUrl(this UrlHelper urlHelper)
        {
            var request = urlHelper.RequestContext.HttpContext.Request;
            return request.Url.GetLeftPart(UriPartial.Scheme | UriPartial.Authority);
        }
    }
}