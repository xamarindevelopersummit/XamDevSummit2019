using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSC.ConferenceMate.Web.Infrastructure
{
	public interface ISession
	{
		string Token { get; }
	}

	public class Session : ISession
	{
		private string _token;

		public Session()
		{
			Console.WriteLine("Session()");
		}

		public string Token
		{
			get { return _token ?? (_token = Guid.NewGuid().ToString()); }
		}
	}
}