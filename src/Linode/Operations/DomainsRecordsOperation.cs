using Linode.Helpers;
using Linode.Models;
using Linode.Models.Domains;
using Linode.Models.Domains.Internal;

namespace Linode.Operations;

internal class DomainsRecordsOperation : IDomainsRecordsOperation
{
    private const string BasePath = "domains";

    private readonly ICore _core;

    public DomainsRecordsOperation(ICore core)
    {
        _core = core;
    }

    public async Task<Response<IReadOnlyList<DomainRecord>>> List(int domainId, CancellationToken cancellationToken) =>
        await _core.GetPagedResult<DomainRecord, DomainRecordResponse>($"{BasePath}/{domainId}/records", cancellationToken)
            .ConfigureAwait(false);

    public async Task<Response<DomainRecord>> Get(int domainId, int recordId, CancellationToken cancellationToken)
    {
        using var response = await _core.HttpClient.GetAsync($"{BasePath}/{domainId}/records/{recordId}", cancellationToken)
            .ConfigureAwait(false);

        return await _core.GetDomainObjectFromResponse<DomainRecord, DomainRecordResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response> Delete(int domainId, int recordId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(recordId, 1);

        using var response = await _core.HttpClient.DeleteAsync($"{BasePath}/{domainId}/records/{recordId}", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _core.CheckForHttpResponseErrors<Domain>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }
}
