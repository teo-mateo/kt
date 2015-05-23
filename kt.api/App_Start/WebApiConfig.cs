using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Cors;

namespace kt.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //enable cors
            //http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api#enable-cors
            config.EnableCors();

            //will spit out json when text/html is requested.
            //otherwise, the response will be xml.
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
