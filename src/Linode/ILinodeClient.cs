using Linode.Operations;

namespace Linode;

/// <summary>
/// Interface for interacting with various operations in Linode.
/// </summary>
public interface ILinodeClient
{
    /// <summary>
    /// Operations related to domains in Linode's DNS Manager.
    /// </summary>
    IDomainsOperation Domains { get; }
}
