namespace TwitchClient.Infrastructure.Common.Http;

public interface IHttpResponsePipelineBehavior
{
    Task<bool> Apply(HttpResponseMessage response, CancellationToken cancellationToken); 
}