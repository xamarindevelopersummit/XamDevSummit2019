using MSC.ConferenceMate.Repository.Entities.CM;

namespace MSC.ConferenceMate.Web.Infrastructure
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