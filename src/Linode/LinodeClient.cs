using Linode.Operations;
using Linode.Transport;

namespace Linode;

internal sealed class LinodeClient : ILinodeClient
{
    public IDomainsOperation Domains { get; }
    public IRegionsOperation Regions { get; }

    public LinodeClient(HttpClient httpClient)
    {
        var httpConnection = new HttpConnection(httpClient);

        Domains = new DomainsOperation(httpConnection);
        Regions = new RegionsOperation(httpConnection);
    }
}
