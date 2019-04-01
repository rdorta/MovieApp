using System;
using System.Net.Http;
using Fusillade;
using Refit;

namespace MovieApp.Core.Services
{
    public class ApiService : IApiService
    {
        public const string ApiBaseAddress = "https://api.themoviedb.org/3";

        private readonly Lazy<ITmdbService> _background;
        private readonly Lazy<ITmdbService> _speculative;
        private readonly Lazy<ITmdbService> _userInitiated;

        public ApiService(string apiBaseAddress = null)
        {
            ITmdbService CreateClient(HttpMessageHandler messageHandler)
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(apiBaseAddress ?? ApiBaseAddress)
                };

                return RestService.For<ITmdbService>(client);
            }

            _background = new Lazy<ITmdbService>(() => CreateClient(
                new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Background)));

            _userInitiated = new Lazy<ITmdbService>(() => CreateClient(
                new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.UserInitiated)));

            _speculative = new Lazy<ITmdbService>(() => CreateClient(
                new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Speculative)));
        }

        public ITmdbService Background => _background.Value;

        public ITmdbService UserInitiated => _userInitiated.Value;

        public ITmdbService Speculative => _speculative.Value;
    }
}