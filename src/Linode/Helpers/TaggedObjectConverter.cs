using System.Text.Json;
using System.Text.Json.Serialization;
using Linode.Models.Domains;
using Linode.Models.Domains.Internal;
using Linode.Models.Linode;
using Linode.Models.Linode.Internal;
using Linode.Models.NodeBalancers;
using Linode.Models.NodeBalancers.Internal;
using Linode.Models.Tags;
using Linode.Models.Tags.Internal;
using Linode.Models.Volumes;
using Linode.Models.Volumes.Internal;

namespace Linode.Helpers;

internal sealed class TaggedObjectConverter : JsonConverter<TaggedObjectResponse>
{
    public override TaggedObjectResponse? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (!root.TryGetProperty("type", out var typeProp))
        {
            throw new JsonException("Missing type discriminator");
        }

        if (!root.TryGetProperty("data", out var dataProp))
        {
            throw new JsonException("Missing data discriminator");
        }

        return typeProp.GetString() switch
        {
            "domain" => new TaggedObjectResponse
            {
                Data = JsonSerializer.Deserialize<DomainResponse>(dataProp.GetRawText(), options)!.ToDomain(),
                Type = TaggedObjectType.Domain
            },

            "linode" => new TaggedObjectResponse
            {
                Data = JsonSerializer.Deserialize<LinodeInstanceResponse>(dataProp.GetRawText(), options)!.ToDomain(),
                Type = TaggedObjectType.Linode
            },

            "nodebalancer" => new TaggedObjectResponse
            {
                Data = JsonSerializer.Deserialize<NodeBalancerResponse>(dataProp.GetRawText(), options)!.ToDomain(),
                Type = TaggedObjectType.NodeBalancer
            },

            "volume" => new TaggedObjectResponse
            {
                Data = JsonSerializer.Deserialize<VolumeResponse>(dataProp.GetRawText(), options)!.ToDomain(),
                Type = TaggedObjectType.Volume
            },

            _ => throw new JsonException($"Unknown type {typeProp.GetString()}")
        };
    }

    public override void Write(Utf8JsonWriter writer, TaggedObjectResponse value, JsonSerializerOptions options)
    {
        switch (value.Data)
        {
            case Domain domain:
                JsonSerializer.Serialize(writer,
                    new TaggedObjectResponse { Data = domain, Type = TaggedObjectType.Domain }, options);
                break;

            case LinodeInstance linode:
                JsonSerializer.Serialize(writer,
                    new TaggedObjectResponse { Data = linode, Type = TaggedObjectType.Linode }, options);
                break;

            case NodeBalancer nodeBalancer:
                JsonSerializer.Serialize(writer,
                    new TaggedObjectResponse { Data = nodeBalancer, Type = TaggedObjectType.NodeBalancer }, options);
                break;

            case Volume volume:
                JsonSerializer.Serialize(writer,
                    new TaggedObjectResponse { Data = volume, Type = TaggedObjectType.Volume }, options);
                break;

            default:
                throw new NotSupportedException($"Type {value.GetType()} not supported");
        }
    }
}
