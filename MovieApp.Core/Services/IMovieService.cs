using System;
using System.Collections.Generic;
using MovieApp.Core.ViewModels.Models;

namespace MovieApp.Core.Services
{
    public interface IMovieService
    {
        void GetUpcomingMovies(IObserver<IEnumerable<MovieModel>> observer);
    }
}