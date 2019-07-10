using CodeGenHero.WebApi;
using DryIoc;
using DryIoc.WebApi;
using DryIoc.Mvc;
using MSC.ConferenceMate.Web.Infrastructure;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web.Http;

namespace MSC.ConferenceMate.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			#region Custom Controller Selector

			// Web API routes
			config.MapHttpAttributeRoutes(new InheritableDirectRouteProvider()); // Allow for inheritance of the RoutePrefix attribute (see: stackoverflow.com/questions/19989023/net-webapi-attribute-routing-and-inheritance)

			#endregion Custom Controller Selector

			// clear the supported mediatypes of the xml formatter
			config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
				new MediaTypeHeaderValue("application/json-patch+json"));

			config.Formatters.Add(new BsonMediaTypeFormatter());

			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
			json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			LogActionAttribute laa = new LogActionAttribute();
			config.Filters.Add(laa);

			config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

			//config.Routes.MapHttpRoute(
			//	name: "DefaultApi",
			//	routeTemplate: "api/{controller}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);

			// DryIoC registration.
			config.RegisterDependencyResolver();
		}

		private static void RegisterDependencyResolver(this HttpConfiguration config)
		{
			IContainer container = new Container(rules => rules
			.With(FactoryMethod.ConstructorWithResolvableArguments)
			.WithoutThrowOnRegisteringDisposableTransient())
			.WithWebApi(config).WithMvc();//); // .WithWebApi(config);
			container.RegisterDependencies();

			//IEnumerable<Assembly> controllerAssemblies = new List<Assembly>() { typeof(MvcApplication).Assembly };
			// Important to use container returned from call for any further operation.
			// If you won't do anything with it, then it is fine not to save returned container
			// (the WebApi dependency resolver will still have a reference to it)
			//MvcApplication.Container = container
			//	.WithWebApi(config: config, controllerAssemblies: controllerAssemblies, throwIfUnresolved: type => true) //t => t.IsController())
			//	.WithMvc(controllerAssemblies: controllerAssemblies, throwIfUnresolved: type => true);

			MvcApplication.Container = container;

			// auto–diagnostic to find out if all registered dependencies are able to be fulfilled/resolved; avoid unpleasant surprises at runtime.
			KeyValuePair<ServiceInfo, ContainerException>[] registrationValidations = container.Validate();
			if (registrationValidations.Length != 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (var registrationValidation in registrationValidations)
				{
					if (!registrationValidation.Value.Message.Contains("HttpRequestMessage"))
					{   // Ignore error: "Unable to resolve HttpRequestMessage as parameter "request" in ISession".
						sb.AppendLine(registrationValidation.Value.Message);
					}
				}

				if (sb.Length > 0)
				{
					throw new System.Configuration.ConfigurationErrorsException($"IoC registration validation resulted in these errors: {sb.ToString()}");
				}
			}
		}
	}
}