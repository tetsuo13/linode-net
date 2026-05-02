using Linode.Models;

namespace Linode.Operations;

public interface IDomainsOperation
{
    /// <summary>
    /// Adds a new Domain to Linode's DNS Manager. Linode is not a registrar,
    /// and you must own the domain before adding it here. Be sure to point
    /// your registrar to Linode's nameservers so that the records hosted here
    /// are used.
    /// </summary>
    /// <param name="createDomain">Information about the domain being registered.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>The created domain zonefile in the DNS system.</returns>
    Task<Response<Domain>> Create(CreateDomain createDomain, CancellationToken cancellationToken);

    /// <summary>
    /// This is a collection of Domains that you have registered in Linode's
    /// DNS Manager. Linode is not a registrar, and in order for these to work
    /// you must own the domains and point your registrar at Linode's
    /// nameservers.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of domain zonefiles.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-domains"/>
    Task<Response<IReadOnlyList<Domain>>> List(CancellationToken cancellationToken);

    /// <summary>
    /// Update information about a domain in Linode's DNS Manager.
    /// </summary>
    /// <param name="id">The ID of the domain to access.</param>
    /// <param name="updateDomain">The domain to update.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>The updated domain zonefile in the DNS system.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/put-domain"/>
    Task<Response<Domain>> Update(int id, UpdateDomain updateDomain, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a Domain from Linode's DNS Manager. The Domain will be removed
    /// from Linode's nameservers shortly after this operation completes. This
    /// also deletes all associated Domain Records.
    /// </summary>
    /// <param name="id">The ID of the Domain to access.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/delete-domain"/>
    Task<Response> Delete(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Returns the zone file for the last rendered zone for the specified domain.
    /// </summary>
    /// <param name="id">The ID of the Domain to access.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-domain-zone"/>
    Task<Response<IReadOnlyList<string>>> GetDomainZoneFile(int id, CancellationToken cancellationToken);
}
