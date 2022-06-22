using TwitchClient.Domain;

namespace TwitchClient.Application.Authentication;

public interface IAuthenticationService
{
    string? ClientId { get; }
    Task<AppAccessToken> GetToken(CancellationToken cancellationToken);
    Task<AppAccessToken> RefreshAppAccessToken(CancellationToken cancellationToken);
}