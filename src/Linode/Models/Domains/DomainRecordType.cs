namespace Linode.Models.Domains;

public enum DomainRecordType
{
    /// <summary>
    /// An A (Address) record matches a domain name to an IPv4 address,
    /// specifically the address of the machine hosting the desired resource
    /// for the domain.
    /// </summary>
    A,

    /// <summary>
    /// AAAA (also called quad A) records are the same as <see cref="A"/>
    /// records, but store the IPv6 address of the machine instead of the IPv4
    /// address.
    /// </summary>
    AAAA,

    /// <summary>
    /// NS (Name Server) records specify the name servers used for a domain or
    /// subdomain.
    /// </summary>
    NS,

    /// <summary>
    /// An MX (mail exchanger) record sets the mail delivery destination for a
    /// domain or subdomain.
    /// </summary>
    MX,

    /// <summary>
    /// A CNAME (Canonical Name) record maps one subdomain to another
    /// subdomain, a root domain, or even a different domain entirely.
    /// </summary>
    CNAME,

    /// <summary>
    /// TXT (text) records stores text and can be used to provide information
    /// about the domain.
    /// </summary>
    TXT,

    /// <summary>
    /// An SRV (service) record provides the target hostname and port for a
    /// given service.
    /// </summary>
    SRV,

    /// <summary>
    /// A PTR record, or Pointer record, is used in reverse DNS lookups to map
    /// an IP address to a domain name, essentially the opposite of an
    /// <see cref="A"/> record which maps a domain name to an IP address.
    /// </summary>
    PTR,

    /// <summary>
    /// A CAA (Certification Authority Authorization) record allows the owner
    /// of a domain to specify which certificate authority (or authorities)
    /// are allowed to issue TLS/SSL certificates for their domain.
    /// </summary>
    CAA
}
