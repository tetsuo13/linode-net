namespace Linode.Models.Domains;

/// <summary>
/// A single record on a Domain.
/// </summary>
public sealed record DomainRecord
{
    /// <summary>
    /// When this Domain Record was created.
    /// </summary>
    public required DateTime Created { get; init; }

    /// <summary>
    /// This Record's unique ID.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The name of this Record.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The port this Record points to. Only valid and required for SRV record
    /// requests.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// The priority of the target host for this Record. Lower values are
    /// preferred. Only valid for MX and SRV record requests. Required for SRV
    /// record requests.
    /// <para/>
    /// Defaults to 0 for MX record requests. Must be 0 for Null MX records.
    /// </summary>
    public int Priority { get; init; }

    /// <summary>
    /// The protocol this Record's service communicates with. An underscore
    /// (_) is prepended automatically to the submitted value for this
    /// property. Only valid for SRV record requests.
    /// </summary>
    public string? Protocol { get; init; }

    /// <summary>
    /// The name of the service. An underscore (_) is prepended and a period
    /// (.) is appended automatically to the submitted value for this
    /// property. Only valid and required for SRV record requests.
    /// </summary>
    public string? Service { get; init; }

    /// <summary>
    /// The tag portion of a CAA record. Only valid and required for CAA
    /// record requests.
    /// </summary>
    public DomainRecordTag? Tag { get; init; }

    /// <summary>
    /// The target for this Record.
    /// </summary>
    public required string Target { get; init; }

    /// <summary>
    /// "Time to Live" -- the amount of time in seconds that this Domain's
    /// records may be cached by resolvers or other domain servers. Valid
    /// values are 300, 3600, 7200, 14400, 28800, 57600, 86400, 172800,
    /// 345600, 604800, 1209600, and 2419200 -- any other value will be
    /// rounded to the nearest valid value.
    /// </summary>
    public required int TtlSec { get; init; }

    /// <summary>
    /// The type of Record this is in the DNS system. For example, A records
    /// associate a domain name with an IPv4 address, and AAAA records
    /// associate a domain name with an IPv6 address.
    /// </summary>
    /// <seealso href="https://techdocs.akamai.com/cloud-computing/docs/dns-record-types">
    /// DNS record types
    /// </seealso>
    public required DomainRecordType Type { get; init; }

    /// <summary>
    /// When this Domain Record was last updated.
    /// </summary>
    public required DateTime Updated { get; init; }

    /// <summary>
    /// The relative weight of this Record used in the case of identical
    /// priority. Higher values are preferred. Only valid and required for
    /// SRV record requests.
    /// </summary>
    public int Weight { get; init; }
}
