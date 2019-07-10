using MSC.ConferenceMate.Repository.Entities.CM;

namespace MSC.ConferenceMate.API.Infrastructure.Interface
{
	public interface ICMDataContextFactory
	{
		ICMDataContext Create();
	}
}