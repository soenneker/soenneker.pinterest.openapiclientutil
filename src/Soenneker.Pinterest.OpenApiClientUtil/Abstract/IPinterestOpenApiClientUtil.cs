using Soenneker.Pinterest.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Pinterest.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IPinterestOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<PinterestOpenApiClient> Get(CancellationToken cancellationToken = default);
}
