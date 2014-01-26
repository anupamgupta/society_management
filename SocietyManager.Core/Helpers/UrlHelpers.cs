using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public static class UrlHelpers
    {
        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName)
        {
            return new Uri(HttpContext.Current.Request.Url, urlHelper.Action(actionName));
        }

        internal static Uri ActionFull(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return new Uri(HttpContext.Current.Request.Url, urlHelper.Action(actionName, controllerName));
        }

        public static Uri GetPermalink(string relativePath)
        {
            if (System.Web.HttpContext.Current != null)
            {
                var uri = System.Web.HttpContext.Current.Request.Url;
                return new UriBuilder(uri.Scheme, uri.Host, uri.Port, relativePath).Uri;
            }

            var defaultUri = new Uri("http://localhost");
            return new UriBuilder(defaultUri.Scheme, defaultUri.Host, defaultUri.Port, relativePath).Uri;
        }

        public static Uri GetBaseUri(this HtmlHelper helper)
        {
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Request.Url;
            }
            var defaultUri = new Uri("http://localhost");
            return new UriBuilder(defaultUri.Scheme, defaultUri.Host, defaultUri.Port).Uri;
        }

        public static string AbsoluteAction(this UrlHelper url, string action, object routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

            string absoluteAction = String.Format("{0}://{1}{2}",
                                                    requestUrl.Scheme,
                                                    requestUrl.Authority,
                                                    url.Action(action, routeValues));

            return absoluteAction;
        }

        public static string AbsoluteAction(this UrlHelper url, string scheme, string action, object routeValues)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

            string absoluteAction = String.Format("{0}://{1}{2}",
                                                    scheme,
                                                    requestUrl.Authority,
                                                    url.Action(action, routeValues));

            return absoluteAction;
        }
    }
}
