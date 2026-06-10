namespace Linode.Models.Regions;

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
