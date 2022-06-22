using System.Net.Http.Json;
using Polly;

namespace TwitchClient.Infrastructure.Common.Http;

public class HttpRequest : IHttpRequest
{
    private readonly HttpClient _client;
    private readonly HttpRequestMessage _requestMessage;
    private readonly IEnumerable<IHttpResponsePipelineBehavior> _pipelineBehaviors;
    private readonly AsyncPolicy<HttpResponseMessage>? _policy;

    internal HttpRequest(HttpClient client, HttpRequestMessage requestMessage, IEnumerable<IHttpResponsePipelineBehavior> pipelineBehaviors, AsyncPolicy<HttpResponseMessage>? policy = null)
    {
        _client = client;
        _requestMessage = requestMessage;
        _pipelineBehaviors = pipelineBehaviors;
        _policy = policy;
    }

    public Task<TResponse> Send<TResponse>()
        => Send<TResponse>(CancellationToken.None);

    public async  Task<TResponse> Send<TResponse>(CancellationToken cancellationToken)
    {
        var response = await (_policy?.ExecuteAsync(SendRequest, cancellationToken)
                       ?? SendRequest(cancellationToken));

        await ProcessResponseBehaviors(response, cancellationToken);

        var deserializedObject = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
        return deserializedObject ?? throw new Exception();
    }

    private async Task ProcessResponseBehaviors(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (_pipelineBehaviors is null)
        {
            return;
        }
        foreach (var behavior in _pipelineBehaviors)
        {
            var result = await behavior.Apply(response, cancellationToken);
            if (!result)
            {
                break;
            }
        }
    }

    private Task<HttpResponseMessage> SendRequest(CancellationToken cancellationToken) => _client.SendAsync(_requestMessage, cancellationToken);
}