using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSC.ConferenceMate.Web;
using MSC.ConferenceMate.Web.Controllers;

namespace MSC.ConferenceMate.Web.Tests.Controllers
{
	// See: http://krzysztofjakielaszek.com/2017/03/31/dryioc-fast-small-full-featured-ioc-container-for-net/
	//[TestClass]
	//public class IoCDependencyResolverTests
	//{
	//	[TestMethod]
	//	public void DependencyResolverTest()
	//	{
	//		HttpConfiguration config = new HttpConfiguration();
	//		DryIoc.IContainer container = new DryIoc.Container(rules => rules.WithoutThrowOnRegisteringDisposableTransient())
	//			.WithWebApi(config);

	//		container.RegisterDependencies();

	//		KeyValuePair<ServiceRegistrationInfo, ContainerException>[] verification = container.VerifyResolutions();

	//		Assert.IsTrue(verification.Length == 0);
	//	}
	//}
}