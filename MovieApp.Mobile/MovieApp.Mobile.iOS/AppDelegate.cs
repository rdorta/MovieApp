using FFImageLoading.Forms.Platform;
using Foundation;
using MovieApp.Core;
using MvvmCross.Forms.Platforms.Ios.Core;

namespace MovieApp.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<CoreMovieApp, App>, CoreMovieApp, App>
    {

        protected override void LoadFormsApplication()
        {
            CachedImageRenderer.Init();
            base.LoadFormsApplication();
        }

    }
}