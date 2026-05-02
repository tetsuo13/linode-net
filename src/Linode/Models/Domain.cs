using Linode.Models.Internal;

namespace Linode.Models;

/// <summary>
/// A domain zonefile in our DNS system. You must own the domain name and tell
/// your registrar to use Linode's nameservers in order for a domain in our
/// system to be treated as authoritative.
/// </summary>
public sealed record Domain
{
    /// <summary>
    /// The list of IPs that may perform a zone transfer for this domain. The
    /// total combined length of all data within this array cannot exceed 1000
    /// characters.
    /// </summary>
    public List<string> AxfrIps { get; set; }

    /// <summary>
    /// A description for this domain. This is for display purposes only.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The domain this domain represents. domain labels cannot be longer than
    /// 63 characters and must conform to RFC1035. domains must be unique on
    /// Linode's platform, including across different Linode accounts; there
    /// cannot be two domains representing the same domain.
    /// </summary>
    /// <seealso href="https://datatracker.ietf.org/doc/html/rfc1035"/>
    public required string DomainName { get; set; }

    /// <summary>
    /// The amount of time in seconds that may pass before this domain is no
    /// longer authoritative.
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Valid values are 0, 30, 120, 300, 3600, 7200, 14400, 28800, 57600,
    /// 86400, 172800, 345600, 604800, 1209600, and 2419200.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Any other value is rounded up to the nearest valid value.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// A value of 0 is equivalent to the default value of 1209600.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    public int ExpireSec { get; set; }

    /// <summary>
    /// This domain's unique ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// The IP addresses representing the master DNS for this domain. At least
    /// one value is required for type <see cref="DomainType.Slave"/> domains.
    /// The total combined length of all data within this array cannot exceed
    /// 1000 characters.
    /// </summary>
    public List<string> MasterIps { get; set; }
    public int RefreshSec { get; set; }
    public int RetrySec { get; set; }

    /// <summary>
    /// Start of Authority email address. This is required for type
    /// <see cref="DomainType.Master"/> domains.
    /// </summary>
    public string SoaEmail { get; set; }

    /// <summary>
    /// Used to control whether this domain is currently being rendered.
    /// </summary>
    public DomainStatus Status { get; set; }

    /// <summary>
    /// An array of tags applied to this object. Tags are for organizational
    /// purposes only.
    /// </summary>
    public List<string> Tags { get; set; }

    public int TtlExp { get; set; }

    /// <summary>
    /// Whether this domain represents the authoritative source of information
    /// for the domain it describes or whether it is a read-only copy of a
    /// master.
    /// </summary>
    public DomainType Type { get; set; }
}
