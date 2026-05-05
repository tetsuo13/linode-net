using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Helpers;
using Linode.Models;
using Linode.Models.Internal;
using Linode.Operations;

namespace Linode;

internal sealed class LinodeClient : ILinodeClient, ICore
{
    public IDomainsOperation Domains { get; }

    public HttpClient HttpClient { get; }

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public LinodeClient(HttpClient httpClient)
    {
        HttpClient = httpClient;

        Domains = new DomainsOperation(this);

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }

    public async Task<(bool HasError, Response<T> ErrorResponse)> CheckForHttpResponseErrors<T>(HttpResponseMessage httpResponse,
        CancellationToken cancellationToken)
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            return (false, Response.Failure<T>(null));
        }

        const string example = """
                               {
                                 "errors" : [ {
                                   "reason" : "soa_email required when type=master",
                                   "field" : "soa_email"
                                 } ]
                               }
                               """;

        try
        {
            var jsonResponse = await GetChildObjectFromJson(httpResponse.Content, "errors", cancellationToken)
                .ConfigureAwait(false);
            var errors = JsonSerializer.Deserialize<List<ErrorResponse>>(jsonResponse, _jsonSerializerOptions);

            return (true, Response.Failure<T>(errors));
        }
        catch (Exception)
        {
            // Couldn't find and/or deserialize error object. Use generic
            // response instead.
        }

        var errorResponse = new ErrorResponse
        {
            Reason = !string.IsNullOrEmpty(httpResponse.ReasonPhrase)
                ? $"Unsuccessful response: {httpResponse.ReasonPhrase}"
                : "Unsuccessful status code response"
        };

        return (true, Response.Failure<T>([errorResponse]));
    }

    public async Task<Response<IReadOnlyList<TModel>>> GetPagedResult<TModel, TApiResponse>(string path,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TModel>
    {
        var results = new List<TModel>();
        var currentPage = 1;
        int processedPage;

        do
        {
            using var response = await HttpClient.GetAsync($"{path}?page={currentPage}", cancellationToken)
                .ConfigureAwait(false);

            var httpResponseError = await CheckForHttpResponseErrors<IReadOnlyList<TModel>>(response, cancellationToken)
                .ConfigureAwait(false);

            if (httpResponseError.HasError)
            {
                return httpResponseError.ErrorResponse;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            PagedData<TApiResponse>? pagedData;

            try
            {
                pagedData = JsonSerializer.Deserialize<PagedData<TApiResponse>>(jsonResponse, _jsonSerializerOptions);

                if (pagedData is null)
                {
                    return Response.Failure<IReadOnlyList<TModel>>([new ErrorResponse
                    {
                        Reason = "Error deserializing response"
                    }]);
                }
            }
            catch (Exception e)
            {
                return Response.Failure<IReadOnlyList<TModel>>([new ErrorResponse
                {
                    Reason = $"Error deserializing response: {e.Message}"
                }]);
            }

            results.AddRange(pagedData.Data.Select(x => x.ToDomain()));

            currentPage++;
            processedPage = pagedData.Page;
        } while (processedPage >= currentPage);

        return Response.Success<IReadOnlyList<TModel>>(results.AsReadOnly());
    }

    public async Task<Response<TModel>> GetDomainObjectFromResponse<TModel, TApiResponse>(HttpResponseMessage response,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TModel>
    {
        var httpResponseError = await CheckForHttpResponseErrors<TModel>(response, cancellationToken)
            .ConfigureAwait(false);

        if (httpResponseError.HasError)
        {
            return httpResponseError.ErrorResponse;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var domain = JsonSerializer.Deserialize<TApiResponse>(jsonResponse, _jsonSerializerOptions);

            if (domain is null)
            {
                return Response.Failure<TModel>([new ErrorResponse
                {
                    Reason = "Error deserializing response"
                }]);
            }

            return Response.Success(domain.ToDomain());
        }
        catch (Exception e)
        {
            return Response.Failure<TModel>([new ErrorResponse
            {
                Reason = $"Error deserializing response: {e.Message}"
            }]);
        }
    }

    public async Task<string> GetChildObjectFromJson(HttpContent content, string topLevelElement,
        CancellationToken cancellationToken)
    {
        var jsonResponse = await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        using var doc = JsonDocument.Parse(jsonResponse);
        return doc.RootElement.GetProperty(topLevelElement).GetRawText();
    }
}
