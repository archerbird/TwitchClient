using TwitchClient.Domain.Common.Enums;
using TwitchClient.Domain.Common;

namespace TwitchClient.Infrastructure.Users;

public class UserRepresentation
{
    public string? Broadcaster_type { get; set; }
    public string? Description { get; set; }
    public string? Display_name { get; set; }
    public string? Id { get; set; }
    public string? Login { get; set; }
    public string? Offline_image_url { get; set; }
    public string? Profile_image_url { get; set; }
    public string? Type { get; set; }
    public int View_count { get; set; }
    public string? Email { get; set; }
    public string? Created_at { get; set; }

    internal TwitchClient.Domain.Users.User Map()
    {
        return new Domain.Users.User
        {
            BroadcasterType = Broadcaster_type switch
            {
                "partner" => BroadcasterType.Partner,
                "affiliate" => BroadcasterType.Affiliate,
                _ => BroadcasterType.None
            },
            Description = Description,
            DisplayName = Display_name,
            Id = Id,
            Login = Login,
            OfflineImageUrl = Offline_image_url,
            ProfileImageUrl = Profile_image_url,
            Type = Type switch
            {
                "staff" => UserType.Staff,
                "admin" => UserType.Admin,
                "global_mod" => UserType.GlobalMod,
                _ => UserType.None
            },
            ViewCount = View_count,
            Email = Email is null ? null : Domain.Common.Email.Parse(Email),
            CreatedAt = Created_at is null ? null :  DateTime.Parse(Created_at)
        };
    }
}
