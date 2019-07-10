using CodeGenHero.Logging;
using MSC.ConferenceMate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.ConferenceMate.Domain
{
	public abstract class DomainBase
	{
		public DomainBase(ILoggingService log, ICMRepository repository)
		{
			Log = log;
			Repo = repository;
		}

		protected ILoggingService Log { get; set; }
		protected ICMRepository Repo { get; set; }
	}
}