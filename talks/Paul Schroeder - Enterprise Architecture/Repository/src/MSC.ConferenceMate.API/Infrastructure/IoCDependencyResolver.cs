using CodeGenHero.Logging;
using CodeGenHero.Logging.Log4net;
using DryIoc;
using MSC.ConferenceMate.API.Models.Interface;
using MSC.ConferenceMate.Repository;
using entCM = MSC.ConferenceMate.Repository.Entities.CM;
using MSC.ConferenceMate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using MSC.ConferenceMate.API.Infrastructure.Interface;
using iDom = MSC.ConferenceMate.Domain.Interface;
using System.Configuration;
using dom = MSC.ConferenceMate.Domain;

namespace MSC.ConferenceMate.API.Infrastructure
{
	public static class IoCDependencyResolver
	{
		private static readonly Type _ApiControllerType = typeof(ApiController);
		private static readonly Assembly _CurrentAssembly = typeof(WebApiApplication).Assembly;

		public static ISession GetSession(HttpRequestMessage request)
		{
			// TODO: This is just a sample. Insert whatever session management logic you need.
			var session = new Models.Session(request);
			return session;
		}

		public static void RegisterDependencies(this IContainer container)
		{
			container.RegisterRepositories();

			container.RegisterServices();

			container.RegisterDbContexts();

			//container.RegisterControllers();
		}

		private static void RegisterControllers(this IContainer container)
		{   // This is now handled by the "WithWebApi" and WithMvc extension methods on the container object.
			// Register Web API Controllers
			foreach (Type controller in _CurrentAssembly.GetTypes()
				.Where(t => !t.IsAbstract && t.IsClass && _ApiControllerType.IsAssignableFrom(t)))
			{
				container.Register(controller, Reuse.Scoped);
			}
		}

		private static void RegisterDbContexts(this IContainer container)
		{
			container.Register<ICMDataContextFactory, CMDataContextFactory>(reuse: Reuse.Singleton);
			container.Register<entCM.ICMDataContext>(made: Made.Of(r => ServiceInfo.Of<ICMDataContextFactory>(), f => f.Create()));
		}

		private static void RegisterRepositories(this IContainer container)
		{
			container.Register<ICMRepository, CMRepository>();
		}

		private static void RegisterServices(this IContainer container)
		{
			// NOTE: Registers ISession provider to work with injected Request
			container.Register<ISession>(Made.Of(() => GetSession(Arg.Of<HttpRequestMessage>())));

			var log = new Log4NetLoggingService();
			container.UseInstance<ILoggingService>(log);

			iDom.IAzureStorageConfig azureStorageConfig = new Domain.AzureStorageConfig()
			{
				AccountKey = ConfigurationManager.AppSettings[dom.Consts.AzureStorageConfig_AccountKey],
				AccountName = ConfigurationManager.AppSettings[dom.Consts.AzureStorageConfig_AccountName],
				ImageContainer = ConfigurationManager.AppSettings[dom.Consts.AzureStorageConfig_ImageContainer],
				QueueName = ConfigurationManager.AppSettings[dom.Consts.AzureStorageConfig_QueueName],
				ThumbnailContainer = ConfigurationManager.AppSettings[dom.Consts.AzureStorageConfig_ThumbnailContainer]
			};
			container.UseInstance<iDom.IAzureStorageConfig>(azureStorageConfig);

			container.Register<iDom.IAzureStorageManager, dom.AzureStorageManager>();
			container.Register<iDom.IUser, dom.User>();
		}
	}
}