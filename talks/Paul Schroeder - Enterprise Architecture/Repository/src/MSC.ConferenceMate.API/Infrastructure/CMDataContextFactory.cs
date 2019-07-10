using MSC.ConferenceMate.Repository.Entities.CM;
using MSC.ConferenceMate.API.Infrastructure.Interface;

namespace MSC.ConferenceMate.API.Infrastructure
{
	public class CMDataContextFactory : ICMDataContextFactory
	{
		public CMDataContextFactory()
		{
		}

		public ICMDataContext Create()
		{
			string connectionString = "ConferenceMateDB";
			return new CMDataContext(connectionString: connectionString);
		}
	}
}