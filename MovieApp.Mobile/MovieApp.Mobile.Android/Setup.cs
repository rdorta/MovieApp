using MovieApp.Core;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;

namespace MovieApp.Mobile.Droid
{
    public class Setup : MvxFormsAndroidSetup<CoreMovieApp, App>
    {
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            // Fix for a bug when using the back button and the presenter can't be found on the IoCProvider
            var formsPresenter = base.CreateFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton(formsPresenter);
            return formsPresenter;
        }
    }
}