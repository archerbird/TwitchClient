using TwitchClient.Domain.Common;
using TwitchClient.Domain.Common.Enums;
namespace TwitchClient.Domain.Users;

public class User
{
    public BroadcasterType BraodcasterType { get; set; }
    public string? Description { get; set; }
    public string? DisplayName { get; set; }
    public string? Id { get; set; }
    public string? Login { get; set; }
    public string? OfflineImageUrl { get; set; }
    public string? ProfileImageUrl { get; set; }
    public UserType Type { get; set; }
    public int ViewCount { get; set; }
    public Email Email { get; set; }
    public DateTime? CreatedAt { get; set; }
}