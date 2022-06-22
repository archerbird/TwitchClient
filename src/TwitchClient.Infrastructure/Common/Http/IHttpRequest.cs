namespace TwitchClient.Infrastructure.Common.Http;

public interface IHttpRequest
{
        Task<TResponse> Send<TResponse>();
        Task<TResponse> Send<TResponse>(CancellationToken cancellationToken);
}
