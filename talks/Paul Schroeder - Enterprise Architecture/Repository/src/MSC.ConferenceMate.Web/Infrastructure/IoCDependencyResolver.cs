using CodeGenHero.Logging;
using CodeGenHero.Logging.Log4net;
using DryIoc;
using MSC.ConferenceMate.Repository;
using MSC.ConferenceMate.Repository.Entities.CM;
using MSC.ConferenceMate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MSC.ConferenceMate.Web.Infrastructure
{
	public static class IoCDependencyResolver
	{
		private static readonly Type _ApiControllerType = typeof(ApiController);
		private static readonly Assembly _CurrentAssembly = typeof(MvcApplication).Assembly;
		private static readonly Type _MvcControllerType = typeof(System.Web.Mvc.Controller);

		public static ISession GetSession(HttpRequestMessage request)
		{
			// TODO: This is just a sample. Insert whatever session management logic you need.
			var session = new Session();
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

			// Register MVC Controllers
			foreach (Type controller in _CurrentAssembly.GetTypes()
				.Where(t => !t.IsAbstract && t.IsClass && _MvcControllerType.IsAssignableFrom(t)))
			{
				container.Register(controller, Reuse.Scoped);
			}
		}

		private static void RegisterDbContexts(this IContainer container)
		{
			// register DbContexs eg.
			//container.Register<DbContext>(made: Made.Of(() => new DbContext()));
			container.Register<ICMDataContextFactory, CMDataContextFactory>(Reuse.Singleton);
			container.Register<ICMDataContext>(made: Made.Of(r => ServiceInfo.Of<ICMDataContextFactory>(), f => f.Create()));
		}

		private static void RegisterRepositories(this IContainer container)
		{
			// register repos eg.
			container.Register<ICMRepository, CMRepository>();
		}

		private static void RegisterServices(this IContainer container)
		{
			// register services eg.
			//container.Register<IUserService, UserService>();

			// NOTE: Registers ISession provider to work with injected Request
			container.Register<ISession>(Made.Of(() => GetSession(Arg.Of<HttpRequestMessage>())));

			//TODO: Register ILoggerFactory in DryIoc container using Microsoft.Extensions.Logging - https://stackoverflow.com/questions/48911710/register-iloggerfactory-in-dryioc-container?rq=1
			// https://dotnetfiddle.net/GE2Dp2
			var log = new Log4NetLoggingService();
			container.UseInstance<ILoggingService>(log);
			//container.Register<IAppSettingsService, AppSettingsService>(Reuse.Singleton);
		}
	}
}