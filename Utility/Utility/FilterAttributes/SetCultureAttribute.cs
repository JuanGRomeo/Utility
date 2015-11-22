using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Utility.FilterAttributes
{
    /// <summary>
    /// Provides functionality for culture change through an attribute.
    /// </summary>
    public class SetCultureAtributte : FilterAttribute, IActionFilter
    {
        #region Fields
        public static string LANG = "Lang";
        public static string ISO_LANG = "es-ES";
        #endregion

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Is it View ?
            ViewResultBase view = filterContext.Result as ViewResultBase;
            if (view == null) // if not exit
                return;

            HttpContextBase context = filterContext.RequestContext.HttpContext;
            HttpRequestBase request = context.Request;
            HttpResponseBase response = context.Response;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(request.UserLanguages[0]);
            string isolang = ci.Name;

            //assign by querystring
            if (request.QueryString[SetCultureAtributte.LANG] != null)
                isolang = request.QueryString[SetCultureAtributte.LANG];

            //assign by Session
            else if (context.Session[SetCultureAtributte.LANG] != null && (!context.Session[SetCultureAtributte.LANG].Equals(String.Empty)))
                isolang = context.Session[SetCultureAtributte.LANG].ToString();

            //assign by cookie
            else if (request.Cookies[SetCultureAtributte.LANG] != null)
                isolang = request.Cookies[SetCultureAtributte.LANG].Value;

            ChangeLang(isolang, response, request);
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        /// <summary>
        /// Change application culture
        /// </summary>
        /// <param name="lang">ISO code of the new lang</param>
        public static void ChangeLang(string lang, HttpResponseBase response, HttpRequestBase request)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            bool newCookie = false;
            HttpCookie requestCookie = request.Cookies[SetCultureAtributte.LANG];

            if (requestCookie == null)
            {
                requestCookie = new HttpCookie(SetCultureAtributte.LANG);
                newCookie = true;
            }

            requestCookie.Expires = DateTime.Now.AddYears(25);
            requestCookie.Value = lang;

            if (newCookie)
                response.Cookies.Add(requestCookie);
            else
                response.Cookies.Set(requestCookie);
        }
    }
}
