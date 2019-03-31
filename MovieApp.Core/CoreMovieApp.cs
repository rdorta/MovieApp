using MovieApp.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace MovieApp.Core
{
    public class CoreMovieApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<UpcomingMoviesListViewModel>();
        }
    }
}