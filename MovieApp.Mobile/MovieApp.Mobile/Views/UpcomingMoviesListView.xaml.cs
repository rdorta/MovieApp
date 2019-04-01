using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace MovieApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class UpcomingMoviesListView
    {
        public UpcomingMoviesListView()
        {
            InitializeComponent();
        }
    }
}