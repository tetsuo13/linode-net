using System.Text.Json.Serialization;

namespace Linode.Models.Linode.Internal;

internal sealed record LinodeConfigIdRequest
{
    [JsonPropertyName("config_id")]
    public required int ConfigId { get; init; }
}
