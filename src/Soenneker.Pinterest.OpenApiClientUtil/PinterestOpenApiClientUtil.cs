using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Pinterest.HttpClients.Abstract;
using Soenneker.Pinterest.OpenApiClientUtil.Abstract;
using Soenneker.Pinterest.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Pinterest.OpenApiClientUtil;

public sealed class PinterestOpenApiClientUtil : IPinterestOpenApiClientUtil
{
    private readonly AsyncSingleton<PinterestOpenApiClient> _client;

    public PinterestOpenApiClientUtil(IPinterestOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<PinterestOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetStringStrict("Pinterest:AccessToken");
            string authHeaderName = configuration["Pinterest:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Pinterest:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient)
            {
                BaseUrl = configuration["Pinterest:ClientBaseUrl"] ?? "https://api.pinterest.com/v5"
            };

            return new PinterestOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<PinterestOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
