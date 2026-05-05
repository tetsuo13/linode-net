using System.Text.Json.Serialization;

namespace Linode.Models.Domains.Internal;

public record CloneDomainRequest
{
    [JsonPropertyName("domain")]
    public required string TargetDomain { get; init; }
}
