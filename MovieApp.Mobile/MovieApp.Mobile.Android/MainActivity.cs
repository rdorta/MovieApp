using Android.App;
using Android.Content.PM;
using MovieApp.Core;
using MvvmCross.Forms.Platforms.Android.Views;

namespace MovieApp.Mobile.Droid
{
    [Activity(Label = "Movie App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, CoreMovieApp, App>
    {
    }
}