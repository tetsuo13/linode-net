namespace Linode.Models.Regions;

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
