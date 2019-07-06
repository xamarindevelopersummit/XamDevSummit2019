using Prism.Events;
using Prism.Navigation;

namespace XamDevSummit.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService, eventAggregator)
        {
        }
    }
}