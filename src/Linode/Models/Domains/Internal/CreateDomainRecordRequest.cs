using System.Text.Json.Serialization;

namespace Linode.Models.Domains.Internal;

internal sealed record CreateDomainRecordRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("port")]
    public int? Port { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("protocol")]
    public string? Protocol { get; set; }

    [JsonPropertyName("service")]
    public string? Service { get; set; }

    [JsonPropertyName("tag")]
    public DomainRecordTag? Tag { get; set; }

    [JsonPropertyName("target")]
    public string? Target { get; set; }

    [JsonPropertyName("ttl_sec")]
    public int? TtlSec { get; set; }

    [JsonPropertyName("type")]
    public required DomainRecordType Type { get; set; }

    [JsonPropertyName("weight")]
    public int? Weight { get; set; }
}
