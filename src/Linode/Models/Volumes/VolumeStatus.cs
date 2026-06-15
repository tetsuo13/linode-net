namespace Linode.Models.Volumes;

/// <summary>
/// The current status of a volume.
/// </summary>
public enum VolumeStatus
{
    /// <summary>
    /// The API is creating the volume and it's not ready for use.
    /// </summary>
    Creating,

    /// <summary>
    /// The volume is online and ready for use.
    /// </summary>
    Active,

    /// <summary>
    /// The volume's capacity is being upgraded.
    /// </summary>
    Resizing,

    /// <summary>
    /// The volume's encryption keys are being rotated to new values. Requests
    /// to resize, delete, or clone a volume fail during encryption key
    /// rotation.
    /// </summary>
    KeyRotating
}
