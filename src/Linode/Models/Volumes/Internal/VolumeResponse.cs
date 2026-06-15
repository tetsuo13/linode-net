using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Volumes.Internal;

internal sealed record VolumeResponse : IMapsTo<Volume>
{
    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("encryption")]
    public ToggleType Toggle { get; init; }

    [JsonPropertyName("filesystem_path")]
    public required string FileSystemPath { get; init; }

    [JsonPropertyName("hardware_type")]
    public HardwareType HardwareType { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("io_ready")]
    public bool IoReady { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("linode_id")]
    public int? LinodeId { get; init; }

    [JsonPropertyName("linode_label")]
    public string? LinodeLabel { get; init; }

    [JsonPropertyName("locks")]
    public IReadOnlyList<string> Locks { get; init; } = [];

    [JsonPropertyName("region")]
    public required string Region { get; init; }

    [JsonPropertyName("size")]
    public int Size { get; init; }

    [JsonPropertyName("status")]
    public VolumeStatus Status { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string> Tags { get; init; } = [];

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }

    public Volume ToDomain() =>
        new()
        {
            Created = Created,
            Toggle = Toggle,
            FileSystemPath = FileSystemPath,
            HardwareType = HardwareType,
            Id = Id,
            IoReady = IoReady,
            Label = Label,
            LinodeId = LinodeId,
            LinodeLabel = LinodeLabel,
            Locks = Locks,
            Region = Region,
            Size = Size,
            Status = Status,
            Tags = Tags,
            Updated = Updated
        };
}
