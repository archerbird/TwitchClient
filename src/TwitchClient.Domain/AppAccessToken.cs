namespace TwitchClient.Domain;
public class AppAccessToken
{
    public string? Token { get; init; }
    public int ExpirySeconds { get; set; }
}
