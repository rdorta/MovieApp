using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace MovieApp.Core.ViewModels
{
    public class UpcomingMoviesListViewModel : BaseViewModel
    {
        public UpcomingMoviesListViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(
            logProvider, navigationService)
        {
        }
    }
}