namespace Linode.Models.Regions;

/// <summary>
/// Regions where you can deploy Akamai Cloud services. Note that some
/// services may not be available in all regions.
/// </summary>
public record Region
{
    /// <summary>
    /// A list of capabilities of this region.
    /// </summary>
    public List<string> Capabilities { get; set; } = [];

    /// <summary>
    /// The country where this region resides.
    /// </summary>
    public required string Country { get; set; }

    /// <summary>
    /// The unique ID of this Region.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Detailed location information for this region, including city, state
    /// or region, and country.
    /// </summary>
    public required string Label { get; set; }

    /// <summary>
    /// Lists the services in this region that support metrics and alerts use
    /// with Akamai Cloud Pulse (ACLP).
    /// </summary>
    public required Monitors Monitors { get; set; }

    /// <summary>
    /// The limits for placement groups in this region.
    /// </summary>
    public required PlacementGroupLimits PlacementGroupLimits { get; set; }

    /// <summary>
    /// Addresses for this region's DNS resolvers.
    /// </summary>
    public required Resolvers Resolvers { get; set; }

    /// <summary>
    /// This region's site type.
    /// </summary>
    public SiteType SiteType { get; set; }

    /// <summary>
    /// This region's current operational status.
    /// </summary>
    public RegionStatus Status { get; set; }
}
