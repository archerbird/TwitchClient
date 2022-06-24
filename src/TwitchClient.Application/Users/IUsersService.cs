using TwitchClient.Domain.Users;
namespace TwitchClient.Application.Users;

public interface IUsersService
{
    Task<User?> GetUserByLogin(string loginName, CancellationToken cancellationToken);
}
