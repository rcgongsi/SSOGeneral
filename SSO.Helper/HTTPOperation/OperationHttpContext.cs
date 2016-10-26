﻿using System;
using System.Web;

namespace SSO.Helper.HTTPOperation
{
    /// <summary>
    /// MVC操作方法
    /// </summary>
    public class OperationHttpContext : Operation
    {
        public HttpContextBase Context { get; set; }

        public OperationHttpContext(HttpContextBase context)
        {
            Context = context;
        }

        public override string GetRequest(string request)
        {
            return Context.Request[request];
        }

        public override void SetCookie(HttpCookie cookie)
        {
            Context.Response.Cookies.Add(cookie);
        }

        public override HttpCookie GetCookie(string cookieName)
        {
            return Context.Request.Cookies[cookieName];
        }

        public override void Redirect(string url)
        {
            Context.Response.Redirect(url);
        }

        public override void PerformJs(string text)
        {
            text = text.Replace("<script>", "");
            text = text.Replace("</script>", "");
            PerformJavascript = text;
        }

        public override Uri Uri()
        {
            return new Uri(Context.Request.Url.ToString());
        }
    }
}