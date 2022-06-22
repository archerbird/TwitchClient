namespace TwitchClient.Infrastructure.Authentication;

public static class TokenTranscoder
{
    internal static TwitchClient.Domain.AppAccessToken Map(this ClientCredentialsFlowResponse src)
    {
        return new Domain.AppAccessToken()
        {
            Token = src.Access_token,
            ExpirySeconds = src.Expires_in
        };
    }
}