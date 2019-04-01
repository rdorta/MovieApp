using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Core.Services;
using MovieApp.Core.ViewModels.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MovieApp.Core.ViewModels
{
    public class UpcomingMoviesListViewModel : BaseViewModel, IObserver<IEnumerable<MovieModel>>
    {
        private readonly IMovieService _movieService;
        private List<MovieModel> _movieHolder = new List<MovieModel>();
        private string _searchFilter;

        public UpcomingMoviesListViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMovieService movieService) : base(
            logProvider, navigationService)
        {
            _movieService = movieService;
            Movies = new MvxObservableCollection<MovieModel>();
        }

        public MvxObservableCollection<MovieModel> Movies { get;  }

        public override Task Initialize()
        {
            IsLoading = true;
            _movieService.GetUpcomingMovies(this);
            return Task.CompletedTask;
        }

        public void OnCompleted()
        {
            FillMovies(_movieHolder);
        }

        private void FillMovies(IEnumerable<MovieModel> list)
        {
            Movies.Clear();
            foreach (var movie in list.OrderByDescending(n => n.ReleaseDate))
            {
                Movies.Add(movie);
            }

            IsLoading = false;
        }

        public string SearchFilter
        {
            get => _searchFilter;
            set => SetProperty(ref _searchFilter,value);
        }

        public MvxCommand ApplySearchCommand => new MvxCommand(() =>
        {
            var filteredList = _movieHolder.Where(n => n.Name.Contains(SearchFilter));
            FillMovies(filteredList);
        });

        public MvxCommand ClearFilterCommand => new MvxCommand(() =>
        {
            FillMovies(_movieHolder);
        });

        public void OnError(Exception error)
        {
            // TODO: Add error message
            IsLoading = false;
        }

        public MvxAsyncCommand<MovieModel> GoToDetailCommand => new MvxAsyncCommand<MovieModel>(async movie =>
            {
                await NavigationService.Navigate<MovieDetailViewModel, MovieModel>(movie);
            });

        public void OnNext(IEnumerable<MovieModel> value)
        {
            foreach (var movieModel in value)
            {
                if (!_movieHolder.Any(n => n.Id == movieModel.Id))
                {
                    _movieHolder.Add(movieModel);
                }
                else
                {
                    _movieHolder.Remove(_movieHolder.Single(n => n.Id == movieModel.Id));
                    _movieHolder.Add(movieModel);
                }
            }
        }
    }
}