using Prism.Events;
using Prism.Navigation;

namespace XamDevSummit.ViewModels
{
    public class ModalSubViewModel : ViewModelBase
    {
        public ModalSubViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
            : base(navigationService, eventAggregator)
        {
        }
    }
}