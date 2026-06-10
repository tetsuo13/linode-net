namespace Linode.Models.Regions;

/// <summary>
/// Compute instance availability information by Type and Region.
/// </summary>
public record RegionAvailability
{
    /// <summary>
    /// Whether the compute instance type is available in the region.
    /// </summary>
    public bool Available { get; init; }

    /// <summary>
    /// The compute instance Type ID.
    /// </summary>
    public required string Plan { get; init; }

    /// <summary>
    /// The Region ID.
    /// </summary>
    public required string Region { get; init; }
}
