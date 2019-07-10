using MSC.ConferenceMate.Repository.Entities.CM;

namespace MSC.ConferenceMate.Web.Infrastructure
{
	public interface ICMDataContextFactory
	{
		ICMDataContext Create();
	}
}