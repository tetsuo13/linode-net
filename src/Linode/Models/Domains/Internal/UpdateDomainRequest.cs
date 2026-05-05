using System.Text.Json.Serialization;

namespace Linode.Models.Domains.Internal;

internal class UpdateDomainRequest
{
    [JsonPropertyName("axfr_ips")]
    public List<string> AxfrIps { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("domain")]
    public string Domain { get; init; }

    [JsonPropertyName("expire_sec")]
    public int ExpireSec { get; init; }

    [JsonPropertyName("master_ips")]
    public List<string> MasterIps { get; init; }

    [JsonPropertyName("refresh_sec")]
    public int RefreshSec { get; init; }

    [JsonPropertyName("retry_sec")]
    public int RetrySec { get; init; }

    [JsonPropertyName("soa_email")]
    public string SoaEmail { get; init; }

    [JsonPropertyName("status")]
    public DomainStatus Status { get; init; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; }

    [JsonPropertyName("ttl_sec")]
    public int TtlExp { get; init; }

    [JsonPropertyName("type")]
    public DomainType Type { get; init; }
}
