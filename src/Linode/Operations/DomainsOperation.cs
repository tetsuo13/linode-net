using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Helpers;
using Linode.Models;
using Linode.Models.Domains;
using Linode.Models.Domains.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal sealed class DomainsOperation : IDomainsOperation
{
    private const string BasePath = "domains";

    public IDomainsRecordsOperation Records { get; }

    private readonly IHttpConnection _httpConnection;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DomainsOperation()
    {
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");
    }

    public DomainsOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
        Records = new DomainsRecordsOperation(_httpConnection);

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                // To avoid additional attributes, convert the names to
                // lowercase knowing they're single words. No worries about
                // next word's starting casing.
                new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower)
            }
        };
    }

    public async Task<Response<Domain>> Create(CreateDomain createDomain, CancellationToken cancellationToken) =>
        await _httpConnection.PostRequest<Domain, CreateDomainRequest, DomainResponse>($"{BasePath}",
            createDomain.ToRequest(), cancellationToken).ConfigureAwait(false);

    public async Task<Response<IReadOnlyList<Domain>>> List(CancellationToken cancellationToken) =>
        await _httpConnection.GetPagedResult<Domain, DomainResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);

    public async Task<Response<Domain>> ImportFromRemoteNameserver(string name, string remoteNameserver,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteNameserver);

        var importRequest = new ImportDomainRequest
        {
            Domain = name,
            RemoteNameserver = remoteNameserver
        };
        var body = JsonSerializer.Serialize(importRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await _httpConnection.HttpClient.PostAsync($"{BasePath}/import", httpContent, cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<Domain, DomainResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response<Domain>> Get(int id, CancellationToken cancellationToken)
    {
        using var response = await _httpConnection.HttpClient.GetAsync($"{BasePath}/{id}", cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<Domain, DomainResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response<Domain>> Update(int id, UpdateDomain updateDomain, CancellationToken cancellationToken)
    {
        var domainRequest = updateDomain.ToRequest();
        var body = JsonSerializer.Serialize(domainRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await _httpConnection.HttpClient.PutAsync($"{BasePath}/{id}", httpContent, cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<Domain, DomainResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response> Delete(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);

        using var response = await _httpConnection.HttpClient.DeleteAsync($"{BasePath}/{id}", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _httpConnection.CheckForHttpResponseErrors<Domain>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }

    public async Task<Response<IReadOnlyList<string>>> GetDomainZoneFile(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);

        using var response = await _httpConnection.HttpClient.GetAsync($"{BasePath}/{id}/zone-file", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _httpConnection.CheckForHttpResponseErrors<IReadOnlyList<string>>(response,
            cancellationToken).ConfigureAwait(false);

        if (httpResponseError.HasError)
        {
            return httpResponseError.ErrorResponse;
        }

        var jsonResponse = await JsonHelpers.GetChildObjectFromJson(response.Content, "zone_file", cancellationToken)
            .ConfigureAwait(false);

        var zoneFile = JsonSerializer.Deserialize<IReadOnlyList<string>>(jsonResponse, _jsonSerializerOptions);

        if (zoneFile is null)
        {
            return Response.Failure<IReadOnlyList<string>>([new ErrorResponse
            {
                Reason = "Error deserializing response"
            }]);
        }

        return Response.Success(zoneFile);
    }

    public async Task<Response<Domain>> Clone(int id, string targetName, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetName);

        if (targetName.Length > 253)
        {
            throw new ArgumentOutOfRangeException(nameof(targetName));
        }

        var importRequest = new CloneDomainRequest
        {
            TargetDomain = targetName
        };
        var body = JsonSerializer.Serialize(importRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await _httpConnection.HttpClient.PostAsync($"{BasePath}/{id}/clone", httpContent, cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<Domain, DomainResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }
}
