using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace Linode;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLinodeApi(this IServiceCollection services, string pat)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(pat);

        services.AddHttpClient<ILinodeClient, LinodeClient>(client =>
        {
            client.BaseAddress = new Uri(new Uri("https://api.linode.com/"), new Uri("v4/", UriKind.Relative));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pat);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}
