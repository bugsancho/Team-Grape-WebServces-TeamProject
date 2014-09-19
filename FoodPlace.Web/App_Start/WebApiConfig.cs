namespace FoodPlace.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.Owin.Security.OAuth;
    using Newtonsoft.Json.Serialization;
    using System.Web.Http.Cors;
    using System.Web.Http.Cors.Tracing;
    using System.Web.OData.Extensions;
    using System.Net.Http.Headers;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

           // var cors = new EnableCorsAttribute("*", "*", "*");
         //   config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.AddODataQueryFilter();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}