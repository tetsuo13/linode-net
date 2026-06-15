namespace Linode.Models.Volumes;

/// <summary>
/// A Block Storage volume on your account.
/// </summary>
public record Volume : ITaggedObject
{
    /// <summary>
    /// When this volume was created.
    /// </summary>
    public required DateTime Created { get; init; }

    /// <summary>
    /// Whether encryption is enabled on this volume.
    /// </summary>
    public ToggleType Toggle { get; init; }

    /// <summary>
    ///The full file system path for the volume, based on its <c>label</c>.
    /// The path is <c>/dev/disk/by-id/scsi-0Linode_Volume_label</c>.
    /// </summary>
    public required string FileSystemPath { get; init; }

    /// <summary>
    /// The storage type of this volume.
    /// </summary>
    public HardwareType HardwareType { get; init; }

    /// <summary>
    /// The unique identifier for the volume.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Indicates whether the volume is successfully attached to a Linode and
    /// ready for read and write operations.
    /// </summary>
    public bool IoReady { get; init; }

    /// <summary>
    /// The name of the volume. A <c>label</c> can be up to 32 characters long
    /// and contain alphanumeric characters, hyphens, and underscores. This
    /// value is also used in the volume's <see cref="FileSystemPath"/>.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// The unique identifier of the Linode this volume is attached to, if
    /// applicable.
    /// </summary>
    public int? LinodeId { get; init; }

    /// <summary>
    /// The name of the Linode this volume is attached to, if applicable.
    /// </summary>
    public string? LinodeLabel { get; init; }

    /// <summary>
    /// A resource lock applied to the Block Storage volume. You can
    /// optionally set up this lock to prevent you from inadvertently
    /// deleting the volume.
    /// </summary>
    public IReadOnlyList<string> Locks { get; init; } = [];

    /// <summary>
    /// The unique identifier for the region where the volume lives.
    /// </summary>
    public required string Region { get; init; }

    /// <summary>
    /// The volume's size, in gigabytes.
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// The current status of the volume.
    /// </summary>
    public VolumeStatus Status { get; init; }

    /// <summary>
    /// Any tags applied to this object.
    /// </summary>
    public IReadOnlyList<string> Tags { get; init; } = [];

    /// <summary>
    /// When this volume was last updated.
    /// </summary>
    public required DateTime Updated { get; init; }
}
