using Linode.Models.MaintenancePolicies;

namespace Linode.Tests.TestHelpers.Models;

public static class MaintenancePoliciesModelHelper
{
    public const string DefaultPolicyJsonResponse = """
                                                    {
                                                      "description": "Migrates the Linode to a new host while it remains fully operational. Recommended for maximizing availability.",
                                                      "is_default": true,
                                                      "label": "Migrate",
                                                      "notification_period_sec": 10800,
                                                      "slug": "linode/migrate",
                                                      "type": "migrate"
                                                    }
                                                    """;

    public static readonly MaintenancePolicy DefaultPolicy = new()
    {
        Description = "Migrates the Linode to a new host while it remains fully operational. Recommended for maximizing availability.",
        IsDefault = true,
        Label = "Migrate",
        NotificationPeriodSec = 10_800,
        Slug = "linode/migrate",
        Type = PolicyType.Migrate
    };
}
