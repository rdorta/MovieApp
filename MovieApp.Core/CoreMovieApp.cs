using Akavache;
using MovieApp.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace MovieApp.Core
{
    public class CoreMovieApp : MvxApplication
    {

        public static string API_TOKEN = "1f54bd990f1cdfb230adb312546d765d";

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<UpcomingMoviesListViewModel>();

            BlobCache.ApplicationName = "MoviesApp";
        }
    }
}