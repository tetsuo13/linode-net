using Linode.Operations;

namespace Linode;

public interface ILinodeClient
{
    IDomainsOperation Domains { get; }
}
