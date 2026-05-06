using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Helpers;
using Linode.Models;
using Linode.Models.Domains;
using Linode.Models.Domains.Internal;

namespace Linode.Operations;

internal class DomainsRecordsOperation : IDomainsRecordsOperation
{
    private const string BasePath = "domains";

    private readonly ICore _core;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DomainsRecordsOperation(ICore core)
    {
        _core = core;
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

        if (!record.IsValid)
        {
            throw new InvalidDataException($"Invalid domain, check ${nameof(CreateDomainRecord.IsValid)} property first");
        }

        var path = $"{BasePath}/{domainId}/records";

        return await _core.PostRequest<DomainRecord, CreateDomainRecordRequest, DomainRecordResponse>(path,
            record.ToRequest(), cancellationToken).ConfigureAwait(false);

        var domainRequest = record.ToRequest();
        var body = JsonSerializer.Serialize(domainRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{BasePath}/{domainId}/records");
        request.Content = httpContent;

        using var response = await _core.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

        return await _core.GetDomainObjectFromResponse<DomainRecord, DomainRecordResponse>(response, cancellationToken)
            .ConfigureAwait(false);
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

    public async Task<Response<DomainRecord>> Update(int domainId, int recordId, UpdateDomainRecord record,
        CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(domainId, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(recordId, 1);

        var recordRequest = record.ToRequest();
        var body = JsonSerializer.Serialize(recordRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await _core.HttpClient.PutAsync($"{BasePath}/{domainId}/records/{recordId}", httpContent,
            cancellationToken).ConfigureAwait(false);

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
