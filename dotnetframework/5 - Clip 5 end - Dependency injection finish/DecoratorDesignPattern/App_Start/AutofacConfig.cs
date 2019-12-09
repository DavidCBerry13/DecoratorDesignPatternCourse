using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using DecoratorDesignPattern.WeatherInterface;
using DecoratorDesignPattern.OpenWeatherMap;
using System.Web.Configuration;

namespace DecoratorDesignPattern.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<Cache>()
                .AsSelf()
                .SingleInstance();

            String apiKey = WebConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            builder.Register<WeatherService>(c => new WeatherService(apiKey))
                .As<IWeatherService>()
                .InstancePerRequest();

            builder.RegisterDecorator<WeatherServiceLoggingDecorator, IWeatherService>();
            builder.RegisterDecorator<WeatherServiceCachingDecorator, IWeatherService>();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}