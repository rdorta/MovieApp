using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieApp.Core.Services.Responses
{
    public class PagedMovieResponse
    {
        [JsonProperty("page")] public int Page { get; set; }

        [JsonProperty("results")] public IEnumerable<MovieResponse> Results { get; set; }

        [JsonProperty("dates")] public MovieDateResponse Dates { get; set; }

        [JsonProperty("total_pages")] public int TotalPages { get; set; }

        [JsonProperty("total_results")] public int TotalResults { get; set; }
    }
}