using System.Text.Json.Serialization;

namespace Linode.Models.Internal;

public record CloneDomainRequest
{
    [JsonPropertyName("domain")]
    public required string TargetDomain { get; init; }
}
