using System.Net.Http.Headers;
using Polly;
using TwitchClient.Application.Users;
using TwitchClient.Domain.Users;
using TwitchClient.Infrastructure.Common.Http;
using Microsoft.Extensions.Options;
using TwitchClient.Infrastructure.Common;
using TwitchClient.Application.Authentication;

namespace TwitchClient.Infrastructure.Users;

internal class UserService : IUsersService
{
    private readonly HttpClient _client;
    private readonly IAuthenticationService _authService;

    public UserService(HttpClient client, IAuthenticationService authenticationService)
    {
        _authService = authenticationService;
        //client.BaseAddress = new Uri("https://api.twitch.tv/helix/");
        client.DefaultRequestHeaders.Add("Client-Id", _authService.ClientId);
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await _authService.GetToken(cancellationToken)).Token))
        _client = client;
    }
    public async Task<User> GetUserByLogin(string loginName, CancellationToken cancellationToken)
    {
        var request = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUrl($"https://api.twitch.tv/helix/users?login={loginName}")
            .WithClient(_client)
            .WithAuthorization(new AuthenticationHeaderValue("Bearer", (await _authService.GetToken(cancellationToken)).Token))
            .WithRetryPolicy(
                Policy.HandleResult<HttpResponseMessage>(response =>
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .RetryAsync(1, async (_, _) => await _authService.RefreshAppAccessToken(cancellationToken)
                ))
            .Build();

        var user = (await request.Send<Response<UserRepresentation>>(cancellationToken)).Data.FirstOrDefault()?.Map();

        return user;
    }
}