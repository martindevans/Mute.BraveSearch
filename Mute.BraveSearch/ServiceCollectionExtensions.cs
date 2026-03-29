using Microsoft.Extensions.DependencyInjection;

namespace Mute.BraveSearch;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add a <see cref="IBraveSearchClient"/> to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="apiKey"></param>
    /// <returns></returns>
    public static IServiceCollection AddBraveSearch(this IServiceCollection services, string apiKey)
    {
        services.AddHttpClient<IBraveSearchClient, BraveSearchClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.search.brave.com/res/v1/");
        })
        .AddTypedClient<IBraveSearchClient>((client, services) => new BraveSearchClient(client, apiKey));

        return services;
    }
}
