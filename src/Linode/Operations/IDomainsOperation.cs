using Linode.Models;
using Linode.Models.Domains;

namespace Linode.Operations;

/// <summary>
/// Operations related to domains in Linode's DNS Manager.
/// </summary>
public interface IDomainsOperation
{
    /// <summary>
    /// Operations related to domain records in Linode's DNS Manager.
    /// </summary>
    IDomainsRecordsOperation Records { get; }

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
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/post-domain"/>
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
    /// Imports a domain zone from a remote nameserver. See Linode API
    /// documentation for list of IPs that remote nameserver must allow zone
    /// transfers (AXFR) from.
    /// </summary>
    /// <param name="name">The domain to import.</param>
    /// <param name="remoteNameserver">The remote nameserver that allows zone transfers (AXFR).</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/post-import-domain"/>
    Task<Response<Domain>> ImportFromRemoteNameserver(string name, string remoteNameserver,
        CancellationToken cancellationToken);

    /// <summary>
    /// This is a single Domain that you have registered in Linode's DNS Manager.
    /// </summary>
    /// <param name="id">The ID of the Domain to access.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-domain"/>
    Task<Response<Domain>> Get(int id, CancellationToken cancellationToken);

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
    /// Clones a Domain and all associated DNS records from a Domain that is
    /// registered in Linode's DNS manager.
    /// </summary>
    /// <param name="id">ID of the Domain to clone.</param>
    /// <param name="targetName">The new domain for the clone.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns></returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/post-clone-domain"/>
    Task<Response<Domain>> Clone(int id, string targetName, CancellationToken cancellationToken);

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
