using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Tags.Internal;

internal sealed record TaggedObjectResponse : IMapsTo<TaggedObject>
{
    [JsonPropertyName("data")]
    public required ITaggedObject Data { get; init; }

    [JsonPropertyName("type")]
    public TaggedObjectType Type { get; init; }

    public TaggedObject ToDomain() =>
        new()
        {
            Data = Data,
            Type = Type
        };
}
