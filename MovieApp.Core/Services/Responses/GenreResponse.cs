using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieApp.Core.Services.Responses
{
    public class GenreResponse
    {
        [JsonProperty("genres")] public IEnumerable<GenreDefinitionResponse> Genres { get; set; }
    }

    public class GenreDefinitionResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}