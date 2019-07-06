using Prism.Events;
using Prism.Navigation;

namespace XamDevSummit.ViewModels
{
    public class MasterDetailShellViewModel : ViewModelBase
    {
        public MasterDetailShellViewModel(INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService, eventAggregator)
        {
        }
    }
}