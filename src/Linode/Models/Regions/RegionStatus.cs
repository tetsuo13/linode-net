namespace Linode.Models.Regions;

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
