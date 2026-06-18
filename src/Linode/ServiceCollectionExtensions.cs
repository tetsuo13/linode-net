using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Linode;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> containing service descriptors.
    /// </param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers the Linode API client.
        /// </summary>
        /// <param name="pat">
        /// A personal access token with sufficient permission or roles for the
        /// desired operations.
        /// </param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when services or <paramref name="pat"/> is
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="pat"/> is empty or only white space.
        /// </exception>
        public IServiceCollection AddLinodeApi(string pat)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentException.ThrowIfNullOrWhiteSpace(pat);

            services.AddHttpClient<ILinodeClient, LinodeClient>(client =>
            {
                client.BaseAddress = new Uri(new Uri("https://api.linode.com/"), new Uri("v4/", UriKind.Relative));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", pat);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Linode-net", version));
            });

            return services;
        }
    }
}
