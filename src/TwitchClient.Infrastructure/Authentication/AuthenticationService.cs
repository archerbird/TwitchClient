using Microsoft.Extensions.Options;
using TwitchClient.Application.Authentication;
using TwitchClient.Domain;
using TwitchClient.Infrastructure.Common;
using TwitchClient.Infrastructure.Common.Http;

namespace TwitchClient.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly TwitchApiSettings _settings;
    public static AppAccessToken? CurrentToken { get; private set; }

    public string? ClientId { get => _settings.ClientId; }

    public async Task<AppAccessToken> GetToken(CancellationToken cancellationToken)
    {
        return CurrentToken ??= await RefreshAppAccessToken(cancellationToken);
    }

    public AuthenticationService(HttpClient client, IOptions<TwitchApiSettings> twitchApiOptions)
    {
        _settings = twitchApiOptions.Value;
        _client = ConfigureClient(client, _settings);
    }

    public async Task<AppAccessToken> RefreshAppAccessToken(CancellationToken cancellationToken)
    {
        if(_settings.ClientId is null || _settings.ClientSecret is null)
        {
            throw new Exception("ClientId and ClientSecret must be set in appsettings.json");
        }

        var request = new HttpRequestBuilder()
            .WithClient(_client)
            .WithMethod(HttpMethod.Post)
            .WithUrl("https://id.twitch.tv/oauth2/token")
            .WithContentType("application/x-www-form-urlencoded")
            .WithBody(new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string,string>("client_id", _settings.ClientId),
                new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            }))
            .Build();

        CurrentToken = (await request.Send<ClientCredentialsFlowResponse>(cancellationToken)).Map();
        return CurrentToken;
    }

    private static HttpClient ConfigureClient(HttpClient client, TwitchApiSettings settings)
    {
        client.BaseAddress = new Uri("https://id.twitch.tv/oauth2/");
        client.DefaultRequestHeaders.Add("Client-Id", settings.ClientId);
        return client;
    }
}
