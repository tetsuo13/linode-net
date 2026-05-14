using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Models;
using Linode.Models.Domains;
using Linode.Models.Domains.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal class DomainsRecordsOperation : IDomainsRecordsOperation
{
    private const string BasePath = "domains";

    private readonly IHttpConnection _httpConnection;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DomainsRecordsOperation()
    {
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");
    }

    public DomainsRecordsOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower)
            }
        };
    }

    public async Task<Response<DomainRecord>> Create(int domainId, CreateDomainRecord record,
        CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);

        var path = $"{BasePath}/{domainId}/records";

        return await _httpConnection.PostRequest<DomainRecord, CreateDomainRecordRequest, DomainRecordResponse>(path,
            record.ToRequest(), cancellationToken).ConfigureAwait(false);
    }

    public async Task<Response<IReadOnlyList<DomainRecord>>> List(int domainId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);

        var path = $"{BasePath}/{domainId}/records";

        return await _httpConnection.GetPagedResult<DomainRecord, DomainRecordResponse>(path, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response<DomainRecord>> Get(int domainId, int recordId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(recordId, 1);

        using var response = await _httpConnection.HttpClient.GetAsync($"{BasePath}/{domainId}/records/{recordId}", cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<DomainRecord, DomainRecordResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response<DomainRecord>> Update(int domainId, int recordId, UpdateDomainRecord record,
        CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(recordId, 1);

        var recordRequest = record.ToRequest();
        var body = JsonSerializer.Serialize(recordRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await _httpConnection.HttpClient.PutAsync($"{BasePath}/{domainId}/records/{recordId}", httpContent,
            cancellationToken).ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<DomainRecord, DomainRecordResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response> Delete(int domainId, int recordId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(recordId, 1);

        var path = $"{BasePath}/{domainId}/records/{recordId}";

        using var response = await _httpConnection.HttpClient.DeleteAsync(path, cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _httpConnection.CheckForHttpResponseErrors<Domain>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }
}
