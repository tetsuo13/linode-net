using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Domains.Internal;

internal sealed record DomainRecordResponse : IMapsTo<DomainRecord>
{
    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("port")]
    public int Port { get; init; }

    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    [JsonPropertyName("service")]
    public string? Service { get; init; }

    [JsonPropertyName("tag")]
    public DomainRecordTag? Tag { get; init; }

    [JsonPropertyName("target")]
    public required string Target { get; init; }

    [JsonPropertyName("ttl_sec")]
    public required int TtlSec { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(AllCapsEnumJsonConverter<DomainRecordType>))]
    public required DomainRecordType Type { get; init; }

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }

    [JsonPropertyName("weight")]
    public int Weight { get; init; }

    public DomainRecord ToDomain() =>
        new()
        {
            Created = Created,
            Id = Id,
            Name = Name,
            Port = Port,
            Priority = Priority,
            Protocol = Protocol,
            Service = Service,
            Tag = Tag,
            Target = Target,
            TtlSec = TtlSec,
            Type = Type,
            Updated = Updated,
            Weight = Weight
        };
}
