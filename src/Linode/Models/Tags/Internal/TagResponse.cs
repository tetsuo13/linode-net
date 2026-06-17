using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Tags.Internal;

internal sealed class TagResponse : IMapsTo<Tag>
{
    [JsonPropertyName("label")]
    public required string Label { get; init; }

    public Tag ToDomain() =>
        new()
        {
            Label = Label
        };
}
