using System.Text.Json.Serialization;

namespace Linode.Models.Internal;

internal record ImportDomainRequest
{
    [JsonPropertyName("domain")]
    public required string Domain { get; init; }

    [JsonPropertyName("remote_nameserver")]
    public required string RemoteNameserver { get; init; }
}

