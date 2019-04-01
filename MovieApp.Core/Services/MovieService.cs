using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Fusillade;
using MovieApp.Core.Services.Responses;
using MovieApp.Core.ViewModels.Models;
using Plugin.Connectivity;
using Polly;

namespace MovieApp.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IApiService _apiService;

        private Dictionary<int, string> _genres;

        public MovieService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public void GetUpcomingMovies(IObserver<IEnumerable<MovieModel>> observer)
        {
            var cache = BlobCache.UserAccount;
            var cachedMovies = cache.GetAndFetchLatest("upcoming",
                () => GetUpcomingMoviesAsync(Priority.UserInitiated),
                offset =>
                {
                    var elapsed = DateTimeOffset.Now - offset;
                    return elapsed > TimeSpan.FromMinutes(10);
                });

            cachedMovies.Subscribe(observer);
        }

        private async Task UpdateGenres()
        {
            var cache = BlobCache.UserAccount;
            var cachedGenres = cache.GetAndFetchLatest("genres",
                () => GetGenresAsync(Priority.UserInitiated),
                offset =>
                {
                    var elapsed = DateTimeOffset.Now - offset;
                    return elapsed > TimeSpan.FromMinutes(10);
                });

            var genres = await cachedGenres.LastOrDefaultAsync() ?? await cachedGenres.FirstOrDefaultAsync();
            _genres = new Dictionary<int, string>();
            foreach (var genre in genres)
            {
                if (!_genres.ContainsKey(genre.Id))
                {
                    _genres.Add(genre.Id, genre.Name);
                }
            }
        }

        private async Task<IEnumerable<GenreDefinitionResponse>> GetGenresAsync(Priority priority)
        {
            var genres = new List<GenreDefinitionResponse>();
            var keys = await BlobCache.UserAccount.GetAllKeys().FirstOrDefaultAsync();
            if (keys.Contains("genres"))
                genres = await BlobCache.UserAccount.GetObject<List<GenreDefinitionResponse>>("genres")
                    .FirstOrDefaultAsync();
            var currentPage = 1;
            var lastPage = 1;
            for (var x = currentPage; x <= lastPage; x++)
            {
                Task<GenreResponse> getGenresTask;
                switch (priority)
                {
                    case Priority.Background:
                        getGenresTask = _apiService.Background.GetGenres(CoreMovieApp.API_TOKEN, "en-US");
                        break;
                    case Priority.UserInitiated:
                        getGenresTask = _apiService.UserInitiated.GetGenres(CoreMovieApp.API_TOKEN, "en-US");
                        break;
                    case Priority.Speculative:
                        getGenresTask = _apiService.Speculative.GetGenres(CoreMovieApp.API_TOKEN, "en-US");
                        break;
                    default:
                        getGenresTask = _apiService.UserInitiated.GetGenres(CoreMovieApp.API_TOKEN, "en-US");
                        break;
                }

                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        var genreResponse = await Policy.Handle<WebException>()
                            .WaitAndRetryAsync
                            (
                                5,
                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                            )
                            .ExecuteAsync(async () => await getGenresTask);
                        genres.AddRange(genreResponse.Genres);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        
                    }
                    
                    
                }
            }

            return genres;
        }

        private async Task<List<MovieModel>> GetUpcomingMoviesAsync(Priority priority)
        {
            await UpdateGenres().ConfigureAwait(false);
            var movies = new List<MovieModel>();
            var keys = await BlobCache.UserAccount.GetAllKeys().FirstOrDefaultAsync();
            if (keys.Contains("upcoming"))
                movies = await BlobCache.UserAccount.GetObject<List<MovieModel>>("upcoming")
                    .FirstOrDefaultAsync();
            var currentPage = 1;
            var lastPage = 1;
            for (var x = currentPage; x <= lastPage; x++)
            {
                Task<PagedMovieResponse> getPagedMoviesTask;
                switch (priority)
                {
                    case Priority.Background:
                        getPagedMoviesTask =
                            _apiService.Background.GetMovies(CoreMovieApp.API_TOKEN, "en-US", currentPage);
                        break;
                    case Priority.UserInitiated:
                        getPagedMoviesTask =
                            _apiService.UserInitiated.GetMovies(CoreMovieApp.API_TOKEN, "en-US", currentPage);
                        break;
                    case Priority.Speculative:
                        getPagedMoviesTask =
                            _apiService.Speculative.GetMovies(CoreMovieApp.API_TOKEN, "en-US", currentPage);
                        break;
                    default:
                        getPagedMoviesTask =
                            _apiService.UserInitiated.GetMovies(CoreMovieApp.API_TOKEN, "en-US", currentPage);
                        break;
                }

                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        var pagedMovieResponse = await Policy.Handle<WebException>()
                            .WaitAndRetryAsync
                            (
                                5,
                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                            )
                            .ExecuteAsync(async () => await getPagedMoviesTask);
                        lastPage = pagedMovieResponse.TotalPages;
                        movies.AddRange(pagedMovieResponse.Results.Select(n => new MovieModel
                        {
                            Genre = string.Join(",", n.GenreIds.Select(g => _genres[g])),
                            Name = n.Title,
                            Id = n.Id,
                            Overview = n.Overview,
                            Poster = $"http://image.tmdb.org/t/p/w185{n.Poster}",
                            ReleaseDate = Convert.ToDateTime(n.ReleaseDate)
                        }));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                }

                currentPage++;
            }

            return movies;
        }
    }
}