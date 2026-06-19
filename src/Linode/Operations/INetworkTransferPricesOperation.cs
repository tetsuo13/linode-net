using Linode.Models;
using Linode.Models.NetworkTransferPrices;

namespace Linode.Operations;

/// <summary>
/// Operations related to network transfer prices.
/// </summary>
public interface INetworkTransferPricesOperation
{
    /// <summary>
    /// Returns collection of network transfer prices, including any
    /// region-specific rates.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of regions.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-network-transfer-prices"/>
    Task<Response<IReadOnlyList<NetworkTransferPrice>>> List(CancellationToken cancellationToken);
}
