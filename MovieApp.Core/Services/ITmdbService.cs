using System.Threading.Tasks;
using MovieApp.Core.Services.Responses;
using Refit;

namespace MovieApp.Core.Services
{
    [Headers("Accept: application/json, text/plain, */*", "Content-Type: application/json")]
    public interface ITmdbService
    {
        [Get("/movie/upcoming")]
        Task<PagedMovieResponse> GetMovies([Query] [AliasAs("api_key")] string apiKey, [Query] string language,
            [Query("page")] int page);

        [Get("/genre/movie/list")]
        Task<GenreResponse> GetGenres([Query] [AliasAs("api_key")] string apiKey, [Query] string language);
    }
}