using System.Text.Json.Serialization;

namespace Linode.Models.Tags.Internal;

internal sealed record CreateTagRequest
{
    [JsonPropertyName("label")]
    public required string Label { get; set; }

    [JsonPropertyName("domains")]
    public List<int>? Domains { get; set; }

    [JsonPropertyName("nodebalancers")]
    public List<int>? NodeBalancers { get; set; }

    [JsonPropertyName("volumes")]
    public List<int>? Volumes { get; set; }
}
