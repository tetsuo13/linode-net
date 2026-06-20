namespace Linode.Models.MaintenancePolicies;

/// <summary>
/// Information about maintenance policies.
/// </summary>
public record MaintenancePolicy
{
    /// <summary>
    /// A brief explanation of the maintenance policy's intended behavior.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Indicates whether this policy is the default one applied when creating
    /// a Linode.
    /// </summary>
    public bool IsDefault { get; init; }

    /// <summary>
    /// The display name for the maintenance policy.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// Number of seconds before the maintenance event triggers. A value of 0
    /// means no prior notification.
    /// </summary>
    public int NotificationPeriodSec { get; init; }

    /// <summary>
    /// Unique identifier for the maintenance policy. System policies are
    /// prefixed with <c>linode/</c>.
    /// </summary>
    public required string Slug { get; init; }

    /// <summary>
    /// The type of policy.
    /// </summary>
    public PolicyType Type { get; init; }
}

/// <summary>
/// Type of policy.
/// </summary>
public enum PolicyType
{
    /// <summary>
    /// Indicates migrate.
    /// </summary>
    Migrate,

    /// <summary>
    /// Indicates power off/on.
    /// </summary>
    PowerOffOn
}
