using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MovieApp.Core.ViewModels
{
    public abstract class BaseViewModel : MvxNavigationViewModel
    {
        private bool _isLoading;

        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(
            logProvider, navigationService)
        {
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
    }


    public abstract class BaseViewModel<ParameterType> : MvxNavigationViewModel<ParameterType>
    {
        private bool _isLoading;

        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(
            logProvider, navigationService)
        {
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
    }
}