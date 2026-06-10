namespace Linode.Models.Regions;

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
