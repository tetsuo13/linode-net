using Linode.Models;
using Linode.Models.Internal;

namespace Linode.Helpers;

internal interface ICore
{
    HttpClient HttpClient { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="httpResponse"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/errors"/>
    Task<(bool HasError, Response<T> ErrorResponse)> CheckForHttpResponseErrors<T>(HttpResponseMessage httpResponse,
        CancellationToken cancellationToken);

    /// <summary>
    ///
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TApiResponse"></typeparam>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/pagination"/>
    Task<Response<IReadOnlyList<TModel>>> GetPagedResult<TModel, TApiResponse>(string path,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TModel>;

    Task<Response<TModel>> GetDomainObjectFromResponse<TModel, TApiResponse>(HttpResponseMessage response,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TModel>;

    Task<Response<TResponse>> PostRequest<TResponse, TRequest, TApiResponse>(string path, TRequest model,
        CancellationToken cancellationToken)
        where TApiResponse : IMapsTo<TResponse>
        where TRequest : notnull;
}
