using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Helpers;
using Linode.Models;
using Linode.Models.Internal;

namespace Linode.Operations;

internal sealed class DomainsOperation : IDomainsOperation
{
    private const string BasePath = "domains";

    private readonly ICore _core;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DomainsOperation(ICore core)
    {
        _core = core;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                // To avoid additional attributes, convert the names to
                // lowercase knowing they're single words. No worries about
                // next word's starting casing.
                new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower)
            }
        };
    }

    public async Task<Response<Domain>> Create(CreateDomain createDomain, CancellationToken cancellationToken)
    {
        if (!createDomain.IsValid)
        {
            throw new InvalidDataException($"Invalid domain, check ${nameof(CreateDomain.IsValid)} property first");
        }

        var domainRequest = createDomain.ToRequest();
        var body = JsonSerializer.Serialize(domainRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{BasePath}");
        request.Content = httpContent;

        using var response = await _core.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

        return await GetDomainFromResponse(response, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Response<IReadOnlyList<Domain>>> List(CancellationToken cancellationToken) =>
        await _core.GetPagedResult<Domain, DomainZoneFile>(BasePath, cancellationToken).ConfigureAwait(false);

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

        using var response = await _core.HttpClient.PostAsync($"{BasePath}/import", httpContent, cancellationToken)
            .ConfigureAwait(false);

        return await GetDomainFromResponse(response, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Response<Domain>> Get(int id, CancellationToken cancellationToken)
    {
        using var response = await _core.HttpClient.GetAsync($"{BasePath}/{id}", cancellationToken)
            .ConfigureAwait(false);

        return await GetDomainFromResponse(response, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Response<Domain>> Update(int id, UpdateDomain updateDomain, CancellationToken cancellationToken)
    {
        var domainRequest = updateDomain.ToRequest();
        var body = JsonSerializer.Serialize(domainRequest, _jsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await _core.HttpClient.PutAsync($"{BasePath}/{id}", httpContent, cancellationToken)
            .ConfigureAwait(false);

        return await GetDomainFromResponse(response, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Response> Delete(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);

        using var response = await _core.HttpClient.DeleteAsync($"{BasePath}/{id}", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _core.CheckForHttpResponseErrors<Domain>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }

    public async Task<Response<IReadOnlyList<string>>> GetDomainZoneFile(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);

        using var response = await _core.HttpClient.GetAsync($"{BasePath}/{id}/zone-file", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _core.CheckForHttpResponseErrors<IReadOnlyList<string>>(response,
            cancellationToken).ConfigureAwait(false);

        if (httpResponseError.HasError)
        {
            return httpResponseError.ErrorResponse;
        }

        var jsonResponse = await _core.GetChildObjectFromJson(response.Content, "zone_file", cancellationToken)
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

        using var response = await _core.HttpClient.PostAsync($"{BasePath}/{id}/clone", httpContent, cancellationToken)
            .ConfigureAwait(false);

        return await GetDomainFromResponse(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<Response<Domain>> GetDomainFromResponse(HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var httpResponseError = await _core.CheckForHttpResponseErrors<Domain>(response, cancellationToken)
            .ConfigureAwait(false);

        if (httpResponseError.HasError)
        {
            return httpResponseError.ErrorResponse;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var domain = JsonSerializer.Deserialize<DomainZoneFile>(jsonResponse, _jsonSerializerOptions);

            if (domain is null)
            {
                return Response.Failure<Domain>([new ErrorResponse
                {
                    Reason = "Error deserializing response"
                }]);
            }

            return Response.Success(domain.ToDomain());
        }
        catch (Exception e)
        {
            return Response.Failure<Domain>([new ErrorResponse
            {
                Reason = $"Error deserializing response: {e.Message}"
            }]);
        }
    }
}
