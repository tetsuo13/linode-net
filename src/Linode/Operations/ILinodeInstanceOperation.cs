using Linode.Models;
using Linode.Models.Linode;

namespace Linode.Operations;

/// <summary>
/// Operations related to Linode instances.
/// </summary>
public interface ILinodeInstanceOperation
{
    /// <summary>
    /// Returns all Linodes you have permission to view.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of Linode instances.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-linode-instances"/>
    Task<Response<IReadOnlyList<LinodeInstance>>> List(CancellationToken cancellationToken);

    /// <summary>
    /// Get a specific Linode based on its unique id.
    /// </summary>
    /// <param name="linodeId">ID of the Linode to look up.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a Linode instance.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-linode-instance"/>
    Task<Response<LinodeInstance>> Get(int linodeId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a Linode.
    /// </summary>
    /// <param name="linodeId">ID of the Linode to delete.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns><see cref="Response"/> object indicating outcome of request.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/delete-linode-instance"/>
    Task<Response> Delete(int linodeId, CancellationToken cancellationToken);

    /// <summary>
    /// Boots a Linode you have permission to modify.
    /// </summary>
    /// <param name="linodeId">ID of the Linode to boot.</param>
    /// <param name="configId">The Linode Config ID to boot into.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns><see cref="Response"/> object indicating outcome of request.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/delete-linode-instance"/>
    Task<Response> Boot(int linodeId, int? configId, CancellationToken cancellationToken);

    /// <summary>
    /// Reboots a Linode you have permission to modify. If any actions are
    /// currently running or queued, those actions must be completed first
    /// before you can initiate a reboot.
    /// </summary>
    /// <param name="linodeId">ID of the Linode to reboot.</param>
    /// <param name="configId">
    /// The Linode Config ID to reboot into. If <see langword="null"/> or
    /// omitted, the last booted config will be used. If there was no last
    /// booted config and this Linode only has one config, it will be used. If
    /// a config cannot be determined, an error will be returned.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns><see cref="Response"/> object indicating outcome of request.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/post-reboot-linode-instance"/>
    Task<Response> Reboot(int linodeId, int? configId, CancellationToken cancellationToken);

    /// <summary>
    /// Shuts down a Linode you have permission to modify. If any actions are
    /// currently running or queued, those actions must be completed first
    /// before you can initiate a shutdown.
    /// </summary>
    /// <param name="linodeId">ID of the Linode to shut down.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns><see cref="Response"/> object indicating outcome of request.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/post-shutdown-linode-instance"/>
    Task<Response> Shutdown(int linodeId, CancellationToken cancellationToken);
}
