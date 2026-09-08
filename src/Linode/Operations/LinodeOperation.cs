using Linode.Transport;

namespace Linode.Operations;

internal sealed class LinodeOperation : ILinodeOperation
{
    public ILinodeInstanceOperation Instance { get;}

    private readonly IHttpConnection _httpConnection;

    public LinodeOperation() =>
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");

    public LinodeOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
        Instance = new LinodeInstanceOperation(_httpConnection);
    }
}
