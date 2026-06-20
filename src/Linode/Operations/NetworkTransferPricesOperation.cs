using Linode.Models;
using Linode.Models.NetworkTransferPrices;
using Linode.Models.NetworkTransferPrices.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal sealed class NetworkTransferPricesOperation : INetworkTransferPricesOperation
{
    private const string BasePath = "network-transfer/prices";

    private readonly IHttpConnection _httpConnection;

    public NetworkTransferPricesOperation() =>
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");

    public NetworkTransferPricesOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
    }

    public async Task<Response<IReadOnlyList<NetworkTransferPrice>>> List(CancellationToken cancellationToken) =>
        await _httpConnection
            .GetPagedResult<NetworkTransferPrice, NetworkTransferPriceResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);
}
