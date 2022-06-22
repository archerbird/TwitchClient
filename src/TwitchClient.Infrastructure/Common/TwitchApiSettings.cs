namespace TwitchClient.Infrastructure.Common;

public class TwitchApiSettings
{
    public static readonly string SectionName = "TwitchApiSettings";
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}