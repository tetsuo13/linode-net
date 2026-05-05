using Linode.Models;
using Linode.Models.Domains;

namespace Linode.Operations;

public interface IDomainsRecordsOperation
{
    /// <summary>
    /// Returns a paginated list of Records configured on a Domain in Linode's
    /// DNS Manager.
    /// </summary>
    /// <param name="domainId">The ID of the Domain we are accessing Records for.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-domain-records"/>
    Task<Response<IReadOnlyList<DomainRecord>>> List(int domainId, CancellationToken cancellationToken);

    /// <summary>
    /// View a single Record on this Domain.
    /// </summary>
    /// <param name="domainId">The ID of the Domain whose Record you are accessing.</param>
    /// <param name="recordId">The ID of the Record you are accessing.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-domain-record"/>
    Task<Response<DomainRecord>> Get(int domainId, int recordId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a Record on this Domain.
    /// </summary>
    /// <param name="domainId">The ID of the Domain whose Record you are accessing.</param>
    /// <param name="recordId">The ID of the Record you are accessing.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/delete-domain-record"/>
    Task<Response> Delete(int domainId, int recordId, CancellationToken cancellationToken);
}
