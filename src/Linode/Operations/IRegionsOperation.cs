using Linode.Models;
using Linode.Models.Regions;

namespace Linode.Operations;

/// <summary>
/// Operations related to regions for Linode services.
/// </summary>
public interface IRegionsOperation
{
    /// <summary>
    /// Lists the regions available for Linode services. Not all services are
    /// guaranteed to be available in all regions.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of regions.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-regions"/>
    Task<Response<IReadOnlyList<Region>>> List(CancellationToken cancellationToken);

    /// <summary>
    /// Returns availability data for all regions.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of region availability.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-regions-availability"/>
    Task<Response<IReadOnlyList<RegionAvailability>>> ListAvailability(CancellationToken cancellationToken);

    /// <summary>
    /// Returns a single Region.
    /// </summary>
    /// <param name="regionId">The ID of the region to get.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with the Region.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-region"/>
    Task<Response<Region>> Get(int regionId, CancellationToken cancellationToken);

    /// <summary>
    /// Returns availability data for a single Region.
    /// </summary>
    /// <param name="regionId"></param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with the Region availability.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-region-availability"/>
    Task<Response<RegionAvailability>> GetAvailability(int regionId, CancellationToken cancellationToken);
}
