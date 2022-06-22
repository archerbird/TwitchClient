using System.Net;
using Microsoft.Extensions.Configuration;
using TwitchClient.Application.Authentication;
using TwitchClient.Application.Users;
using TwitchClient.Infrastructure.Authentication;
using TwitchClient.Infrastructure.Common;
using TwitchClient.Infrastructure.Users;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
        .Configure<TwitchApiSettings>(config.GetSection(TwitchApiSettings.SectionName))
            .AddHttpClient()
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IUsersService, UserService>();
    }
}
