using Linode.Operations;
using Linode.Transport;

namespace Linode;

internal sealed class LinodeClient : ILinodeClient
{
    public IDomainsOperation Domains { get; }
    public INetworkTransferPricesOperation NetworkTransferPrices { get; }
    public IRegionsOperation Regions { get; }
    public ITagsOperation Tags { get; }

    public LinodeClient(HttpClient httpClient)
    {
        var httpConnection = new HttpConnection(httpClient);

        Domains = new DomainsOperation(httpConnection);
        NetworkTransferPrices = new NetworkTransferPricesOperation(httpConnection);
        Regions = new RegionsOperation(httpConnection);
        Tags = new TagsOperation(httpConnection);
    }
}
