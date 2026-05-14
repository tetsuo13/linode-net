namespace Linode.Models.Domains;

/// <summary>
/// The CAA record tags define the type of rule being applied.
/// </summary>
public enum DomainRecordTag
{
    /// <summary>
    /// Authorizes a CA to issue any certificate type for the domain.
    /// </summary>
    Issue,

    /// <summary>
    /// Authorizes a CA to issue wildcard certificates only.
    /// </summary>
    IssueWild,

    /// <summary>
    /// Sends violation reports to your team when unauthorized requests occur.
    /// </summary>
    Iodef
}
