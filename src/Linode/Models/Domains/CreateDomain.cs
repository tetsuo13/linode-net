using Linode.Models.Domains.Internal;

namespace Linode.Models.Domains;

/// <summary>
/// Information about the domain you are registering.
/// </summary>
public record CreateDomain
{
    /// <summary>
    /// The list of IPs that may perform a zone transfer for this domain. The
    /// total combined length of all data within this array cannot exceed 1000
    /// characters.
    /// </summary>
    public List<string>? AxfrIps { get; set; }

    /// <summary>
    /// A description for this domain. This is for display purposes only.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The domain this domain represents. Domain labels cannot be longer than
    /// 63 characters and must conform to RFC1035. domains must be unique on
    /// Linode's platform, including across different Linode accounts; there
    /// cannot be two domains representing the same domain.
    /// </summary>
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
    public int? ExpireSec { get; set; }

    /// <summary>
    /// The IP addresses representing the master DNS for this domain. At least
    /// one value is required for type <see cref="DomainType.Slave"/> domains.
    /// The total combined length of all data within this array cannot exceed
    /// 1000 characters.
    /// </summary>
    public List<string>? MasterIps { get; set; }

    /// <summary>
    /// The amount of time in seconds before this domain should be refreshed.
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
    /// A value of 0 is equivalent to the default value of 14400.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    public int? RefreshSec { get; set; }

    /// <summary>
    /// The interval, in seconds, at which a failed refresh should be retried.
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
    /// A value of 0 is equivalent to the default value of 14400.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    public int? RetrySec { get; set; }

    /// <summary>
    /// Start of Authority email address. This is required for type
    /// <see cref="DomainType.Master"/> domains.
    /// </summary>
    public string? SoaEmail { get; set; }

    /// <summary>
    /// Used to control whether this domain is currently being rendered.
    /// </summary>
    public DomainStatus? Status { get; set; }

    /// <summary>
    /// An array of tags applied to this object. Tags are for organizational
    /// purposes only.
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// "Time to Live" - the amount of time in seconds that this domain's
    /// records may be cached by resolvers or other domain servers.
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
    /// A value of 0 is equivalent to the default value of 86400.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    public int? TtlExp { get; set; }

    /// <summary>
    /// Whether this domain represents the authoritative source of information
    /// for the domain it describes (<see cref="DomainType.Master"/>), or
    /// whether it is a read-only copy of a master
    /// (<see cref="DomainType.Slave"/>).
    /// </summary>
    public required DomainType Type { get; set; }

    internal CreateDomainRequest ToRequest() => new()
    {
        AxfrIps = AxfrIps,
        Description = Description,
        Domain = DomainName,
        ExpireSec = ExpireSec,
        MasterIps = MasterIps,
        RefreshSec = RefreshSec,
        RetrySec = RetrySec,
        SoaEmail = SoaEmail,
        Status = Status,
        Tags = Tags,
        TtlExp = TtlExp,
        Type = Type
    };
}
