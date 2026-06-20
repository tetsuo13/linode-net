using Linode.Models;
using Linode.Models.MaintenancePolicies;

namespace Linode.Operations;

/// <summary>
/// Operations related to maintenance policies.
/// </summary>
public interface IMaintenancePoliciesOperation
{
    /// <summary>
    /// List all available maintenance policies that can be applied to your
    /// Linodes.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of policies.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-maintenance-policies"/>
    Task<Response<IReadOnlyList<MaintenancePolicy>>> List(CancellationToken cancellationToken);
}
