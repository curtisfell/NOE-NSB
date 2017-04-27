using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web.Http;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Logging;

namespace Zsus.Noe.Approve
{
    using TraceFactoryDelegate = Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>>;

    class Startup
    {
        //  Hack from http://stackoverflow.com/a/17227764/19020 to load controllers in 
        //  another assembly.  Another way to do this is to create a custom assembly resolver
        //Type valuesControllerType = typeof(OWINTest.API.ValuesController);

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            //  Enable attribute based routing
            //  http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "WithActionApi",
                "api/{controller}/{action}/{id}",
                new { action = "DefaultAction", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiFlag",
                routeTemplate: "api/{controller}/{id}/{sagaid}/{flag}",
                defaults: new { id = RouteParameter.Optional, flag = RouteParameter.Optional }
            );
            
            appBuilder.UseWebApi(config);
        }
    }

}
