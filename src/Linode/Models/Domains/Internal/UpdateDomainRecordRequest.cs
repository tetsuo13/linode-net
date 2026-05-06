using System.Text.Json.Serialization;

namespace Linode.Models.Domains.Internal;

internal sealed record UpdateDomainRecordRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("port")]
    public int? Port { get; init; }

    [JsonPropertyName("priority")]
    public int? Priority { get; init; }

    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    [JsonPropertyName("service")]
    public string? Service { get; init; }

    [JsonPropertyName("tag")]
    public DomainRecordTag? Tag { get; init; }

    [JsonPropertyName("target")]
    public string? Target { get; init; }

    [JsonPropertyName("ttl_sec")]
    public int? TtlSec { get; init; }

    [JsonPropertyName("weight")]
    public int? Weight { get; init; }
}
