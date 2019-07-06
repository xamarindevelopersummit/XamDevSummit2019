using Prism.Events;
using Prism.Navigation;

namespace XamDevSummit.ViewModels
{
    public class NonModalSubViewModel : ViewModelBase
    {
        public NonModalSubViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
            : base(navigationService, eventAggregator)
        {
        }
    }
}