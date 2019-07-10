using AutoMapper;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MSC.ConferenceMate.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static IContainer Container { get; set; }

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			System.Web.Http.GlobalConfiguration.Configuration.Formatters.Add(new System.Net.Http.Formatting.BsonMediaTypeFormatter());

			//Initialize Log4Net
			log4net.Config.XmlConfigurator.Configure();
			log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			log.Debug("Setup in Application_Start.");

			// Initialize Automapper
			MSC.ConferenceMate.Repository.Mappers.AutoMapperInitializer.Initialize();
			Mapper.AssertConfigurationIsValid();
		}
	}
}