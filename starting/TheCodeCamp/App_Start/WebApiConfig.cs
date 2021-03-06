﻿using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace TheCodeCamp
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {  

      // Web API configuration and services
      AutofacConfig.Register();

            config.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1,1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new UrlSegmentApiVersionReader();
                //ApiVersionReader.Combine(
                //new HeaderApiVersionReader("X-Version"),
                //new QueryStringApiVersionReader("ver"));
            });

     //Change Code to Json
       config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
           new CamelCasePropertyNamesContractResolver();


            var constraintResolve = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof(ApiVersionRouteConstraint)
                }
            };
      // Web API routes
      config.MapHttpAttributeRoutes(constraintResolve);

      //config.Routes.MapHttpRoute(
      //    name: "DefaultApi",
      //    routeTemplate: "api/{controller}/{id}",
      //    defaults: new { id = RouteParameter.Optional }
      //);
    }
  }
}
