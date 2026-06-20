using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.MaintenancePolicies.Internal;

internal sealed class MaintenancePolicyResponse : IMapsTo<MaintenancePolicy>
{
    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("notification_period_sec")]
    public int NotificationPeriodSec { get; init; }

    [JsonPropertyName("slug")]
    public required string Slug { get; init; }

    [JsonPropertyName("type")]
    public PolicyType Type { get; init; }

    public MaintenancePolicy ToDomain() =>
        new()
        {
            Description = Description,
            IsDefault = IsDefault,
            Label = Label,
            NotificationPeriodSec = NotificationPeriodSec,
            Slug = Slug,
            Type = Type
        };
}
