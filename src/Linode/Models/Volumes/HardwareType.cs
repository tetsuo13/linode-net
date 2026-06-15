namespace Linode.Models.Volumes;

/// <summary>
/// Storage types for volumes.
/// </summary>
public enum HardwareType
{
    /// <summary>
    /// Emulates a hard disk drive for the volume.
    /// </summary>
    Hdd,

    /// <summary>
    /// Emulate a non-volatile memory express solid state drive.
    /// </summary>
    Nvme
}
