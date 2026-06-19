using System.Text.Json.Serialization;

namespace Linode.Models.Domains.Internal;

internal sealed record CloneDomainRequest
{
    [JsonPropertyName("domain")]
    public required string TargetDomain { get; init; }
}
