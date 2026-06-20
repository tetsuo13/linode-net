using Linode.Models;
using Linode.Models.MaintenancePolicies;
using Linode.Models.MaintenancePolicies.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal sealed class MaintenancePoliciesOperation : IMaintenancePoliciesOperation
{
    private const string BasePath = "maintenance/policies";

    private readonly IHttpConnection _httpConnection;

    public MaintenancePoliciesOperation() =>
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");

    public MaintenancePoliciesOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
    }

    public async Task<Response<IReadOnlyList<MaintenancePolicy>>> List(CancellationToken cancellationToken) =>
        await _httpConnection
            .GetPagedResult<MaintenancePolicy, MaintenancePolicyResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);
}
