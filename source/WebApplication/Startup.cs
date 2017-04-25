using System;
using System.Collections.Generic;

namespace WebApplication
{
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Filters;
    using ApplicationInsights.OwinExtensions;
    using Microsoft.ApplicationInsights;
    using Owin;
    using WebApplication.Controllers;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseApplicationInsights();

            var configuration = new HttpConfiguration
            {
                DependencyResolver = new DependencyResolver()
            };
            
            configuration.MapHttpAttributeRoutes();
            configuration.Filters.Add(new ExceptionLoggingFilter());

            app.UseWebApi(configuration);
        }
    }

    public class DependencyResolver : IDependencyResolver
    {
        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(MyController))
            {
                return new MyController();
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            yield break;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }

    public class ExceptionLoggingFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var telemetry = new TelemetryClient();

            telemetry.TrackException(actionExecutedContext.Exception);

            base.OnException(actionExecutedContext);
        }
    }
}