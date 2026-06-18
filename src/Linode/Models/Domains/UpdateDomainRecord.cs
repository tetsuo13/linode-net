using Linode.Models.Domains.Internal;

namespace Linode.Models.Domains;

/// <summary>
/// A Domain Record Update request object.
/// </summary>
public record UpdateDomainRecord
{
    /// <summary>
    /// The name of this Record. For requests, this property's actual usage
    /// and whether it is required depends on the type of record this
    /// represents:
    /// <list type="bullet">
    /// <item><description>
    /// <see cref="DomainRecordType.A"/> and
    /// <see cref="DomainRecordType.AAAA"/>: The hostname or FQDN of the
    /// Record.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.NS"/>: The subdomain, if any, to use with
    /// the Domain of the Record. Wildcard NS records (<c>*</c>) are not
    /// supported.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.MX"/>: The mail subdomain. For example,
    /// <c>sub</c> for the address <c>user@sub.example.com</c> under the
    /// <c>example.com</c> Domain.
    /// <list type="bullet">
    /// <item><description>
    /// The left-most subdomain component may be an asterisk (<c>*</c>) to
    /// designate a wildcard subdomain.
    /// </description></item>
    /// <item><description>
    /// Other subdomain components must only contain letters, digits, and
    /// hyphens, start with a letter, end with a letter or digit, and contain
    /// less than 64 characters.
    /// </description></item>
    /// <item><description>
    /// Must be an empty string (<c>""</c>) for a Null MX Record.
    /// </description></item>
    /// </list>
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.CNAME"/>: The hostname. Must be unique.
    /// Required.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.TXT"/>: The hostname.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.SRV"/>: Unused. Use the
    /// <see cref="Service"/> property to set the service name for this record.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.CAA"/>: The subdomain. Omit or enter an
    /// empty string (<c>""</c>) to apply to the entire Domain.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.PTR"/>: See guide on how to Configure Your
    /// Linode for Reverse DNS(rDNS).
    /// </description></item>
    /// </list>
    /// </summary>
    /// <seealso href="https://techdocs.akamai.com/cloud-computing/docs/configure-rdns-reverse-dns-on-a-compute-instance"/>
    public string? Name { get; init; }

    /// <summary>
    /// The port this Record points to. Only valid and required for
    /// <see cref="DomainRecordType.SRV"/> record requests.
    /// </summary>
    public int? Port { get; init; }

    /// <summary>
    /// The priority of the target host for this Record. Lower values are
    /// preferred. Only valid for MX and SRV record requests. Required for
    /// <see cref="DomainRecordType.SRV"/> record requests.
    /// <para/>
    /// Defaults to 0 for MX record requests. Must be 0 for Null MX records.
    /// </summary>
    public int? Priority { get; init; }

    /// <summary>
    /// The protocol this Record's service communicates with. An underscore
    /// (<c>_</c>) is prepended automatically to the submitted value for this
    /// property. Only valid for <see cref="DomainRecordType.SRV"/> record
    /// requests.
    /// </summary>
    public string? Protocol { get; init; }

    /// <summary>
    /// The name of the service. An underscore (<c>_</c>) is prepended and a
    /// period (<c>.</c>) is appended automatically to the submitted value for
    /// this property. Only valid and required for
    /// <see cref="DomainRecordType.SRV"/> record requests.
    /// </summary>
    public string? Service { get; init; }

    /// <summary>
    /// The tag portion of a <see cref="DomainRecordType.CAA"/> record. Only
    /// valid and required for CAA record requests.
    /// </summary>
    public DomainRecordTag? Tag { get; init; }

    /// <summary>
    /// The target for this Record. For requests, this property's actual usage
    /// and whether it is required depends on the type of record this
    /// represents:
    /// <list type="bullet">
    /// <item><description>
    /// <see cref="DomainRecordType.A"/> and
    /// <see cref="DomainRecordType.AAAA"/>: The IP address. Use
    /// <c>[remote_addr]</c> to submit the IPv4 address of the request.
    /// Required.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.NS"/>: The name server. Must be a valid
    /// domain. Required.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.MX"/>: The mail server. Must be a valid
    /// domain unless creating a Null MX Record. Required.
    /// <list type="bullet">
    /// <item><description>
    /// Must have less than 254 total characters.
    /// </description></item>
    /// <item><description>
    /// The left-most domain component may be an asterisk (<c>*</c>) to
    /// designate a wildcard domain.
    /// </description></item>
    /// <item><description>
    /// Other domain components must only contain letters, digits, and
    /// hyphens, start with a letter, end with a letter or digit, and contain
    /// less than 64 characters.
    /// </description></item>
    /// <item><description>
    /// To create a Null MX Record, first remove any additional MX records,
    /// then create an MX record with empty strings (<c>""</c>) for the
    /// <c>target</c> and <c>name</c>. If a Domain has a Null MX record, new
    /// MX records cannot be created.
    /// </description></item>
    /// </list>
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.CNAME"/>: The alias. Must be a valid
    /// domain. Required.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.TXT"/>: The value. Required.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.SRV"/>: The target domain or subdomain. If
    /// a subdomain is entered, it is automatically used with the Domain. To
    /// configure for a different domain, enter a valid FQDN. For example, the
    /// value <c>www</c> with a Domain for <c>example.com</c> results in a
    /// target set to <c>www.example.com</c>, whereas the value
    /// <c>sample.com</c> results in a target set to <c>sample.com</c>.
    /// Required.
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.CAA"/>: The value. For
    /// <see cref="DomainRecordTag.Issue"/> or
    /// <see cref="DomainRecordTag.IssueWild"/> tags, the domain of your
    /// certificate issuer. For the <see cref="DomainRecordTag.Iodef"/> tag, a
    /// contact or submission URL (domain, http, https, or mailto).
    /// Requirements depend on the tag for this record:
    /// <list type="bullet">
    /// <item><description>
    /// <see cref="DomainRecordTag.Issue"/>: The domain of your certificate
    /// issuer. Must include a valid domain. May include additional parameters
    /// separated with semicolons (<c>;</c>), for example:
    /// <c>www.example.com; foo=bar</c>
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordTag.IssueWild"/>: The domain of your wildcard
    /// certificate issuer. Must be a valid domain and must not start with an
    /// asterisk (<c>*</c>).
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordTag.Iodef"/>: Must be either (1) a valid
    /// domain, (2) a valid domain prepended with <c>http://</c> or
    /// <c>https://</c>, or (3) a valid email address prepended with
    /// <c>mailto:</c>.
    /// </description></item>
    /// </list>
    /// </description></item>
    /// <item><description>
    /// <see cref="DomainRecordType.PTR"/>: Required.
    /// </description></item>
    /// </list>
    /// </summary>
    /// <seealso href="https://datatracker.ietf.org/doc/html/rfc7505">
    /// Null MX record
    /// </seealso>
    public string? Target { get; init; }

    /// <summary>
    /// "Time to Live" - the amount of time in seconds that this Domain's
    /// records may be cached by resolvers or other domain servers. Valid
    /// values are 300, 3600, 7200, 14400, 28800, 57600, 86400, 172800,
    /// 345600, 604800, 1209600, and 2419200 - any other value will be rounded
    /// to the nearest valid value.
    /// </summary>
    public int? TtlSec { get; init; }

    /// <summary>
    /// The relative weight of this Record used in the case of identical
    /// priority. Higher values are preferred. Only valid and required for
    /// <see cref="DomainRecordType.SRV"/> record requests.
    /// </summary>
    public int? Weight { get; init; }

    internal UpdateDomainRecordRequest ToRequest() => new()
    {
        Name = Name,
        Port = Port,
        Priority = Priority,
        Protocol = Protocol,
        Service = Service,
        Tag = Tag,
        Target = Target,
        TtlSec = TtlSec,
        Weight = Weight
    };
}
