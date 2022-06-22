using System.Net.Http.Headers;
using Polly;

namespace TwitchClient.Infrastructure.Common.Http;

public interface IHttpRequestBuilder
{
        HttpRequest Build();
        IHttpRequestBuilder WithUrl(Uri url);
        IHttpRequestBuilder WithUrl(string url);
        IHttpRequestBuilder WithBody(HttpContent body);
        IHttpRequestBuilder WithMethod(HttpMethod method);
        IHttpRequestBuilder WithContentType(string contentType);
        IHttpRequestBuilder WithAccept(string accept);
        IHttpRequestBuilder WithHeader(string name, string value);
        IHttpRequestBuilder WithAuthorization(AuthenticationHeaderValue auth);
        IHttpRequestBuilder WithAuthorization(string scheme, string parameter);
        IHttpRequestBuilder WithClient(HttpClient client);
        IHttpRequestBuilder WithRetryPolicy(AsyncPolicy<HttpResponseMessage> policy);
        IHttpRequestBuilder WithResponseProcessing(IHttpResponsePipelineBehavior behavior);
}