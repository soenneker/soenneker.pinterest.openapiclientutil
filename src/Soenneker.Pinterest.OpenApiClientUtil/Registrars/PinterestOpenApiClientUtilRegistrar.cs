using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Pinterest.HttpClients.Registrars;
using Soenneker.Pinterest.OpenApiClientUtil.Abstract;

namespace Soenneker.Pinterest.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class PinterestOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="PinterestOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddPinterestOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddPinterestOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IPinterestOpenApiClientUtil, PinterestOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="PinterestOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddPinterestOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddPinterestOpenApiHttpClientAsSingleton()
                .TryAddScoped<IPinterestOpenApiClientUtil, PinterestOpenApiClientUtil>();

        return services;
    }
}
