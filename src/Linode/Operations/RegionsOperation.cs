using Linode.Models;
using Linode.Models.Regions;
using Linode.Models.Regions.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal class RegionsOperation : IRegionsOperation
{
    private const string BasePath = "regions";

    private readonly IHttpConnection _httpConnection;

    public RegionsOperation() =>
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");

    public RegionsOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
    }

    public async Task<Response<IReadOnlyList<Region>>> List(CancellationToken cancellationToken) =>
        await _httpConnection.GetPagedResult<Region, RegionResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);

    public async Task<Response<IReadOnlyList<RegionAvailability>>> ListAvailability(CancellationToken cancellationToken)
    {
        const string path = $"{BasePath}/availability";
        return await _httpConnection
            .GetPagedResult<RegionAvailability, RegionAvailabilityResponse>(path, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response<Region>> Get(int regionId, CancellationToken cancellationToken)
    {
        using var response = await _httpConnection.HttpClient.GetAsync($"{BasePath}/{regionId}", cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<Region, RegionResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response<RegionAvailability>> GetAvailability(int regionId, CancellationToken cancellationToken)
    {
        var path = $"{BasePath}/{regionId}/availability";
        using var response = await _httpConnection.HttpClient.GetAsync(path, cancellationToken).ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<RegionAvailability, RegionAvailabilityResponse>(
                response, cancellationToken)
            .ConfigureAwait(false);
    }
}
