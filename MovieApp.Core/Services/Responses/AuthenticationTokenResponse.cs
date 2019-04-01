using Newtonsoft.Json;

namespace MovieApp.Core.Services.Responses
{
    public class AuthenticationTokenResponse
    {
        [JsonProperty("success")] public bool HasSuccess { get; set; }

        [JsonProperty("expires_at")] public string ExpiresAt { get; set; }

        [JsonProperty("request_token")] public string Token { get; set; }
    }
}