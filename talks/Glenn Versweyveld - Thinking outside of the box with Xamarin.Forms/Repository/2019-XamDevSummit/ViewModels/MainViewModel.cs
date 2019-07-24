using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using XamDevSummit.Services.Interfaces;

namespace XamDevSummit.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IPageDialogService PageDialogService { get; }
        public IPopupService PopupService { get; }

        private DelegateCommand _nonModalCommand;
        public DelegateCommand NonModalCommand => _nonModalCommand ?? (_nonModalCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("NonModalSubPage")));

        private DelegateCommand _modalCommand;
        public DelegateCommand ModalCommand => _modalCommand ?? (_modalCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync("ModalSubPage", useModalNavigation: true)));

        private DelegateCommand _fabCommand;
        public DelegateCommand FabCommand => _fabCommand ?? (_fabCommand = new DelegateCommand(() => IsFabButtonVisible = !IsFabButtonVisible));

        private DelegateCommand _chatCommand;
        public DelegateCommand ChatCommand => _chatCommand ?? (_chatCommand = new DelegateCommand(async () => await PageDialogService.DisplayAlertAsync("Chat", "Look ma a chat!", "Ok")));

        private DelegateCommand _popupCommand;
        public DelegateCommand PopupCommand => _popupCommand ?? (_popupCommand = new DelegateCommand(() => PopupService.DisplayPopup("Popup", "Look our own popup", new DelegateCommand<Controls.PopupResultEventArgs>(async (result) =>
        {
            var t = result;
        }))));

        public MainViewModel(IPopupService popupService, IPageDialogService pageDialogService, INavigationService navigationService, IEventAggregator eventAggregator) : base(navigationService, eventAggregator)
        {
            PageDialogService = pageDialogService;
            PopupService = popupService;
        }
    }
}