using AutoMapper;
using System.Web.Http;

namespace MSC.ConferenceMate.API
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			System.Web.Http.GlobalConfiguration.Configuration.Formatters.Add(new System.Net.Http.Formatting.BsonMediaTypeFormatter());

			//Initialize Log4Net
			log4net.Config.XmlConfigurator.Configure();
			log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			log.Debug("Setup in Application_Start.");

			// Initialize Automapper
			//AutoMapperInitializer.Initialize();
			MSC.ConferenceMate.Repository.Mappers.AutoMapperInitializer.Initialize();
			Mapper.AssertConfigurationIsValid();
		}
	}
}