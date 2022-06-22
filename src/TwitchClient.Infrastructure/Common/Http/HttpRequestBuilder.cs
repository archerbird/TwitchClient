using System.Net.Http.Headers;
using Polly;

namespace TwitchClient.Infrastructure.Common.Http;

internal class HttpRequestBuilder : IHttpRequestBuilder
{
    private readonly HttpRequestMessage _request = new();
    private HttpClient? _client;
    private readonly List<IHttpResponsePipelineBehavior> _pipelineBehaviors = new();
    private AsyncPolicy<HttpResponseMessage>? _policy;

    public HttpRequest Build()
    {
        if(_client == null)
        {
            throw new InvalidOperationException("Client must be set");
        }

        return new HttpRequest(_client, _request, _pipelineBehaviors, _policy);
    }

    public IHttpRequestBuilder WithAccept(string accept)
    {
        throw new NotImplementedException();
    }

    public IHttpRequestBuilder WithAuthorization(AuthenticationHeaderValue auth)
    {
        _request.Headers.Authorization = auth;
        return this;
    }

    public IHttpRequestBuilder WithAuthorization(string scheme, string parameter)
    {
        _request.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        return this;
    }

    public IHttpRequestBuilder WithBody(HttpContent body)
    {
        _request.Content = body;
        return this;
    }

    public IHttpRequestBuilder WithClient(HttpClient client)
    {
        _client = client;
        return this;
    }

    public IHttpRequestBuilder WithContentType(string contentType)
    {
        if(_request.Content is null)
        {
            _request.Content = new StringContent("");
        }
        _request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        return this;
    }

    public IHttpRequestBuilder WithHeader(string name, string value)
    {
        throw new NotImplementedException();
    }

    public IHttpRequestBuilder WithMethod(HttpMethod method)
    {
        _request.Method = method;
        return this;
    }

    public IHttpRequestBuilder WithUrl(Uri url)
    {
        _request.RequestUri = url;
        return this;
    }

    public IHttpRequestBuilder WithUrl(string url)
    {
        _request.RequestUri = new Uri(url);
        return this;
    }

    public IHttpRequestBuilder WithRetryPolicy(AsyncPolicy<HttpResponseMessage> policy)
    {
        _policy = policy;
        return this;
    }

    public IHttpRequestBuilder WithResponseProcessing(IHttpResponsePipelineBehavior behavior)
    {
        _pipelineBehaviors.Add(behavior);
        return this;
    }
}