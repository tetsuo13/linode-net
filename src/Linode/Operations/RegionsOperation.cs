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
}
