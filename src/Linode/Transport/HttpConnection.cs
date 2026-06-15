using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Helpers;
using Linode.Models;
using Linode.Models.Internal;

namespace Linode.Transport;

internal class HttpConnection : IHttpConnection
{
    public HttpClient HttpClient { get; }
    public JsonSerializerOptions JsonSerializerOptions { get; }

    public HttpConnection(HttpClient httpClient)
    {
        HttpClient = httpClient;

        JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                // To avoid additional attributes, convert the names to
                // lowercase knowing they're single words. No worries about
                // next word's starting casing.
                new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower),

                new TaggedObjectConverter()
            }
        };
    }

    public async Task<Response<IReadOnlyList<TModel>>> GetPagedResult<TModel, TApiResponse>(string path,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TModel>
    {
        var results = new List<TModel>();
        var currentPage = 1;
        int totalPages;

        do
        {
            using var response = await HttpClient.GetAsync($"{path}?page={currentPage}", cancellationToken)
                .ConfigureAwait(false);

            var httpResponseError = await CheckForHttpResponseErrors<IReadOnlyList<TModel>>(response,
                cancellationToken).ConfigureAwait(false);

            if (httpResponseError.HasError)
            {
                return httpResponseError.ErrorResponse;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            PagedData<TApiResponse>? pagedData;

            try
            {
                pagedData = JsonSerializer.Deserialize<PagedData<TApiResponse>>(jsonResponse, JsonSerializerOptions);

                if (pagedData is null)
                {
                    return Response.Failure<IReadOnlyList<TModel>>([
                        new ErrorResponse { Reason = "Error deserializing response" }
                    ]);
                }
            }
            catch (Exception e)
            {
                return Response.Failure<IReadOnlyList<TModel>>([
                    new ErrorResponse { Reason = $"Error deserializing response: {e.Message}" }
                ]);
            }

            results.AddRange(pagedData.Data.Select(x => x.ToDomain()));

            totalPages = pagedData.Pages;
            currentPage = pagedData.Page + 1;
        } while (currentPage - 1 < totalPages);

        return Response.Success<IReadOnlyList<TModel>>(results.AsReadOnly());
    }

    public async Task<(bool HasError, Response<T> ErrorResponse)> CheckForHttpResponseErrors<T>(
        HttpResponseMessage httpResponse, CancellationToken cancellationToken)
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            return (false, Response.Failure<T>(null));
        }

        try
        {
            var jsonResponse = await JsonHelpers.GetChildObjectFromJson(httpResponse.Content, "errors", cancellationToken)
                .ConfigureAwait(false);
            var errors = JsonSerializer.Deserialize<List<ErrorResponse>>(jsonResponse, JsonSerializerOptions);

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
            var domain = JsonSerializer.Deserialize<TApiResponse>(jsonResponse, JsonSerializerOptions);

            if (domain is null)
            {
                return Response.Failure<TModel>([new ErrorResponse { Reason = "Error deserializing response" }]);
            }

            return Response.Success(domain.ToDomain());
        }
        catch (Exception e)
        {
            return Response.Failure<TModel>([
                new ErrorResponse { Reason = $"Error deserializing response: {e.Message}" }
            ]);
        }
    }

    public async Task<Response<TResponse>> PostRequest<TResponse, TRequest, TApiResponse>(string path, TRequest model,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TResponse>
        where TRequest : notnull
    {
        var body = JsonSerializer.Serialize(model, JsonSerializerOptions);

        using var httpContent = new StringContent(body);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, path);
        request.Content = httpContent;

        using var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

        return await GetDomainObjectFromResponse<TResponse, TApiResponse>(response, cancellationToken)
            .ConfigureAwait(false);
    }
}
