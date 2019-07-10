using DryIoc;
using DryIoc.WebApi;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using MSC.ConferenceMate.API.Infrastructure;
using System.Net.Http.Headers;
using CodeGenHero.WebApi;

namespace MSC.ConferenceMate.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//if (System.Diagnostics.Debugger.IsAttached)
			//	config.EnableSystemDiagnosticsTracing();

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

			// DryIoC registration.
			config.RegisterDependencyResolver();
		}

		private static void RegisterDependencyResolver(this HttpConfiguration config)
		{
			IContainer container = new Container(rules => rules
			.With(FactoryMethod.ConstructorWithResolvableArguments)
			.WithoutThrowOnRegisteringDisposableTransient())
			.WithWebApi(config);
			container.RegisterDependencies();

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