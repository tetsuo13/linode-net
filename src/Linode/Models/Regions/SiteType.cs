namespace Linode.Models.Regions;

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
