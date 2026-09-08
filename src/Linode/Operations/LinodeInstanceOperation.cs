using System.Net.Http.Headers;
using System.Text.Json;
using Linode.Models;
using Linode.Models.Linode;
using Linode.Models.Linode.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal sealed class LinodeInstanceOperation : ILinodeInstanceOperation
{
    private const string BasePath = "linode/instances";

    private readonly IHttpConnection _httpConnection;

    public LinodeInstanceOperation() =>
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");

    public LinodeInstanceOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
    }

    public async Task<Response<IReadOnlyList<LinodeInstance>>> List(CancellationToken cancellationToken) =>
        await _httpConnection.GetPagedResult<LinodeInstance, LinodeInstanceResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);

    public async Task<Response<LinodeInstance>> Get(int linodeId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(linodeId, 1);

        using var response = await _httpConnection.HttpClient.GetAsync($"{BasePath}/{linodeId}", cancellationToken)
            .ConfigureAwait(false);

        return await _httpConnection.GetDomainObjectFromResponse<LinodeInstance, LinodeInstanceResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Response> Delete(int linodeId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(linodeId, 1);

        using var response = await _httpConnection.HttpClient.DeleteAsync($"{BasePath}/{linodeId}", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _httpConnection.CheckForHttpResponseErrors<LinodeInstance>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }

    public async Task<Response> Boot(int linodeId, int? configId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(linodeId, 1);

        var path = $"{BasePath}/{linodeId}/boot";
        return await PostWithOptionalConfigId(path, configId, cancellationToken);
    }

    public async Task<Response> Reboot(int linodeId, int? configId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(linodeId, 1);

        var path = $"{BasePath}/{linodeId}/reboot";
        return await PostWithOptionalConfigId(path, configId, cancellationToken);
    }

    public async Task<Response> Shutdown(int linodeId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(linodeId, 1);

        var path = $"{BasePath}/{linodeId}/shutdown";
        using var response = await _httpConnection.HttpClient.PostAsync(path, null, cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _httpConnection.CheckForHttpResponseErrors<LinodeInstance>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }

    private async Task<Response> PostWithOptionalConfigId(string path, int? configId,
        CancellationToken cancellationToken)
    {
        StringContent? httpContent = null;

        try
        {
            if (configId.HasValue)
            {
                var bootRequest = new LinodeConfigIdRequest
                {
                    ConfigId = configId.Value
                };
                var body = JsonSerializer.Serialize(bootRequest, _httpConnection.JsonSerializerOptions);

                httpContent = new StringContent(body);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            using var response = await _httpConnection.HttpClient.PostAsync(path, httpContent, cancellationToken)
                .ConfigureAwait(false);

            var httpResponseError = await _httpConnection
                .CheckForHttpResponseErrors<LinodeInstance>(response, cancellationToken)
                .ConfigureAwait(false);

            return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
        }
        finally
        {
            httpContent?.Dispose();
        }
    }
}
