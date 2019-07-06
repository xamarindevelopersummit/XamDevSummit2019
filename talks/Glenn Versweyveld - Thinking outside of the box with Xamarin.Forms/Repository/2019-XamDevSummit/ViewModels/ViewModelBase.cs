using Prism.Mvvm;
using Prism.Navigation;

namespace XamDevSummit.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        public INavigationService NavigationService { get; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }
    }
}