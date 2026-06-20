using Linode.Operations;

namespace Linode;

/// <summary>
/// Interface for interacting with various operations in Linode.
/// </summary>
public interface ILinodeClient
{
    /// <summary>
    /// Operations related to domains in Linode's DNS Manager.
    /// </summary>
    IDomainsOperation Domains { get; }

    /// <summary>
    /// Operations related to maintenance policies.
    /// </summary>
    IMaintenancePoliciesOperation MaintenancePolicies { get; }

    /// <summary>
    /// Operations related to network transfer prices.
    /// </summary>
    INetworkTransferPricesOperation NetworkTransferPrices { get; }

    /// <summary>
    /// Operations related to regions.
    /// </summary>
    IRegionsOperation Regions { get; }

    /// <summary>
    /// Operations related to tags on the account.
    /// </summary>
    ITagsOperation Tags { get; }
}
