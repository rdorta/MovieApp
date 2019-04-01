namespace MovieApp.Core.Services
{
    public interface IApiService
    {
        ITmdbService Speculative { get; }
        ITmdbService UserInitiated { get; }
        ITmdbService Background { get; }
    }
}