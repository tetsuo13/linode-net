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

/// <summary>
/// Services in a region that support metrics and alerts use with Akamai
/// Cloud Pulse (ACLP).
/// </summary>
public record Monitors
{
    /// <summary>
    /// Each <c>service_type</c> supported for use in managing ACLP alerts in
    /// this region, for your account. A <c>service_type</c> identifies the
    /// Akamai Cloud Computing service.
    /// </summary>
    public List<string> Alerts { get; set; } = [];

    /// <summary>
    /// Each <c>service_type</c> supported for use in managing ACLP metrics in
    /// this region, for your account. A <c>service_type</c> identifies the
    /// Akamai Cloud Computing service.
    /// </summary>
    public List<string> Metrics { get; set; } = [];
}

/// <summary>
/// The limits for placement groups in a region.
/// </summary>
public record PlacementGroupLimits
{
    /// <summary>
    /// The maximum number of Linodes you can include in a placement group,
    /// when that placement group uses a <c>placement_group_policy</c> of
    /// <c>flexible</c>. Displayed as <see langword="null"/> if you don't have
    /// a limit. See Create placement group for more information on
    /// <c>placement_group_policy</c>.
    /// </summary>
    public int? MaximumLinodesPerFlexiblePage { get; set; }

    /// <summary>
    /// The maximum number of Linodes you can include in a placement group,
    /// when that placement group uses a <c>placement_group_policy</c> of
    /// <c>strict</c>. Displayed as <see langword="null"/> if you don't have a
    /// limit. See Create placement group for more information on
    /// <c>placement_group_policy</c>.
    /// </summary>
    public int? MaximumLinodesPerPage { get; set; }

    /// <summary>
    /// The maximum number of placement groups you can have in this region.
    /// Displayed as <see langword="null"/> if you don't have a limit.
    /// </summary>
    public int? MaximumPagesPerCustomer { get; set; }
}

/// <summary>
/// A region's operational status.
/// </summary>
public enum RegionStatus
{
    /// <summary>
    /// Currently OK.
    /// </summary>
    Ok,

    /// <summary>
    /// Currently experiencing an outage.
    /// </summary>
    Outage
}

/// <summary>
/// DNS resolvers for a region.
/// </summary>
public record Resolvers
{
    /// <summary>
    /// The IPv4 addresses for this region's DNS resolvers, separated by commas.
    /// </summary>
    public required string Ipv4 { get; set; }

    /// <summary>
    /// The IPv6 addresses for this region's DNS resolvers, separated by commas.
    /// </summary>
    public required string Ipv6 { get; set; }
}

/// <summary>
/// A region's type.
/// </summary>
public enum SiteType
{
    /// <summary>
    /// Indicates a traditional cloud computing region that offers all compute
    /// services.
    /// </summary>
    Core,

    /// <summary>
    /// Indicates sites that are globally dispersed to be closer to end users
    /// and workloads. These regions offer limited services.
    /// </summary>
    Distributed
}
