using System.Net;
namespace TwitchClient.Infrastructure.Common.Http
{
    public class ConfirmSuccessBehavior : IHttpResponsePipelineBehavior
    {
        public Task<bool> Apply(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {

            }

            return Task.FromResult(true);
        }
    }
}