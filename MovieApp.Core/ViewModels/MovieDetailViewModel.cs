using System;
using System.Collections.Generic;
using System.Text;
using MovieApp.Core.ViewModels.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace MovieApp.Core.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel<MovieModel>
    {
        private string _poster;
        private string _name;
        private string _overview;
        private DateTime _releaseDate;
        private string _genre;

        public MovieDetailViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public string Poster
        {
            get => _poster;
            set => SetProperty(ref _poster, value);
        }

        public string Genre
        {
            get => _genre;
            set => SetProperty(ref _genre,value);
        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Overview
        {
            get => _overview;
            set => SetProperty(ref _overview, value);
        }

        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set => SetProperty(ref _releaseDate, value);
        }

        public override void Prepare(MovieModel parameter)
        {
            Poster = parameter.Poster;
            Name = parameter.Name;
            Genre = parameter.Genre;
            Overview = parameter.Overview;
            ReleaseDate = parameter.ReleaseDate;
        }
    }
}
