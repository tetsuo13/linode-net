namespace Linode.Models.Linode;

/// <summary>
/// A Linode object.
/// </summary>
public record LinodeInstance : ITaggedObject
{
    /// <summary>
    /// Configured alerts for the Linode instance.
    /// </summary>
    public required Alerts Alerts { get; init; }

    /// <summary>
    /// Information about this Linode's backups status.
    /// </summary>
    public required Backups Backups { get; init; }

    /// <summary>
    /// A list of capabilities this Linode supports.
    /// </summary>
    public IReadOnlyList<string> Capabilities { get; init; } = [];

    /// <summary>
    /// When this Linode was created.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// Indicates the local disk encryption setting for this Linode. If the
    /// Linode is part of an LKE cluster, the value is <see langword="null"/>.
    /// </summary>
    public ToggleType? DiskEncryption { get; init; }

    /// <summary>
    /// Whether this Linode was provisioned with <c>user_data</c> provided via
    /// the Metadata service.
    /// </summary>
    public bool HasUserData { get; init; }

    /// <summary>
    /// The Linode's host machine identifier.
    /// </summary>
    public required string HostUuid { get; init; }

    /// <summary>
    /// The virtualization software powering this Linode.
    /// </summary>
    public HypervisorType Hypervisor { get; init; }

    /// <summary>
    /// his Linode's unique identifier, which you need to provide for all
    /// operations impacting this Linode.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The identifier assigned to the disk image to be deployed to the new
    /// Linode. You can run the List images operation with authentication to
    /// view all available images, and store the <see cref="Id"/> from the
    /// applicable one. Official Linode images start with <c>linode/</c>,
    /// while any custom images available on your account start with
    /// <c>private/</c>. To create a disk from a <c>private/</c> image, you
    /// need <c>read_only</c> or <c>read_write</c> permissions for it. Run the
    /// Update a user's grants operation to adjust permissions for a
    /// <c>private/</c> image, or talk to your local account administrator.
    /// </summary>
    public string? Image { get; init; }

    /// <summary>
    /// Indicates how the linode was configured.
    /// </summary>
    public InterfaceGenerationType InterfaceGeneration { get; init; }

    /// <summary>
    /// This Linode's IPv4 Addresses. Each Linode is assigned a single public
    /// IPv4 address upon creation, and may get a single private IPv4 address if needed.
    /// </summary>
    public required IReadOnlyList<string> Ipv4 { get; init; }

    /// <summary>
    /// his Linode's IPv6 SLAAC address. This address is specific to a Linode,
    /// and may not be shared. If the Linode has not been assigned an IPv6
    /// address, the return value will be <see langword="null"/>.
    /// </summary>
    public string? Ipv6 { get; init; }

    /// <summary>
    /// Provides a name for the Linode. If not provided, the API generates one
    /// for it.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// The ID of the Kubernetes cluster if the Linode is part of cluster.
    /// </summary>
    public string? LkeClusterId { get; init; }

    /// <summary>
    /// A resource lock applied to the Linode. You can optionally set up this
    /// lock to prevent you from inadvertently deleting the Linode.
    /// </summary>
    public IReadOnlyList<string> Locks { get; init; } = [];

    /// <summary>
    /// The maintenance policy configured by the user for this Linode.
    /// </summary>
    public MaintenancePolicyType MaintenancePolicy { get; init; }

    /// <summary>
    /// Details on the placement group that this Linode belongs to. Empty if
    /// the Linode isn't in a placement group.
    /// </summary>
    public PlacementGroup? PlacementGroup { get; init; }

    /// <summary>
    /// Information about the resources available to this Linode.
    /// </summary>
    public required LinodeSpecs Specs { get; init; }

    /// <summary>
    /// A brief description of the Linode's current state. This value can
    /// change without direct action from you. For example, when a Linode goes
    /// into maintenance mode, its status is stopped.
    /// </summary>
    public LinodeStatus Status { get; init; }

    /// <summary>
    /// Tags to help you organize your content.
    /// </summary>
    public IReadOnlyList<string> Tags { get; init; } = [];

    /// <summary>
    /// The type that this Linode was deployed with.
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    /// When this Linode was last updated.
    /// </summary>
    public DateTime Updated { get; init; }

    /// <summary>
    /// The watchdog, named Lassie, is a Shutdown Watchdog that monitors your
    /// Linode and reboots it if it powers off unexpectedly. It works by
    /// issuing a boot job when your Linode powers off without a shutdown job
    /// being responsible. To prevent a loop, Lassie gives up if there have
    /// been more than 5 boot jobs issued within 15 minutes.
    /// </summary>
    public bool WatchdogEnabled { get; init; }
}

/// <summary>
/// Configured alerts.
/// </summary>
public record Alerts
{
    /// <summary>
    /// The percentage of CPU usage required to trigger an alert. If the
    /// average CPU usage over two hours exceeds this value, we'll send you an
    /// alert. Your Linode's total CPU capacity is represented as 100%,
    /// multiplied by its number of cores. For example, a two-core Linode's
    /// CPU capacity is represented as 200%. If you want to be alerted at 90%
    /// of a two-core Linode's CPU capacity, set the alert value to 180. The
    /// default value is 90% multiplied by the number of cores. If the value
    /// is set to 0 (zero), the alert is disabled.
    /// </summary>
    public int Cpu { get; init; }

    /// <summary>
    /// The amount of disk IO operation per second required to trigger an
    /// alert. If the average disk IO over two hours exceeds this value,
    /// we'll send you an alert. If set to 0 (zero), this alert is disabled.
    /// </summary>
    public int Io { get; init; }

    /// <summary>
    /// The amount of incoming traffic, in Mbit/s, required to trigger an
    /// alert. If the average incoming traffic over two hours exceeds this
    /// value, we'll send you an alert. If this is set to 0 (zero), the alert
    /// is disabled.
    /// </summary>
    public int NetworkIn { get; init; }

    /// <summary>
    /// The amount of outbound traffic, in Mbit/s, required to trigger an
    /// alert. If the average outbound traffic over two hours exceeds this
    /// value, we'll send you an alert. If this is set to 0 (zero), the alert
    /// is disabled.
    /// </summary>
    public int NetworkOut { get; init; }

    /// <summary>
    /// The percentage of network transfer that may be used before an alert is
    /// triggered. When this value is exceeded, we'll alert you. If this is
    /// set to 0 (zero), the alert is disabled.
    /// </summary>
    public int TransferQuota { get; init; }
}

/// <summary>
/// Information about this Linode's backups status.
/// </summary>
public record Backups
{
    /// <summary>
    /// Whether backups taken for this Linode are available for restoration.
    /// Backups undergoing maintenance are not available for restoration.
    /// </summary>
    public bool Available { get; init; }

    /// <summary>
    /// If this Linode has the Backup service enabled.
    /// </summary>
    public bool Enabled { get; init; }

    /// <summary>
    /// The last successful backup time. Displayed as <see langword="null"/>
    /// if there was no previous backup.
    /// </summary>
    public DateTime? LastSuccessful { get; init; }

    /// <summary>
    /// The backup schedule.
    /// </summary>
    public required BackupSchedule Schedule { get; init; }
}

/// <summary>
/// A backup schedule object.
/// </summary>
public record BackupSchedule
{
    /// <summary>
    /// The day of the week that your Linode's weekly backup is taken. If not
    /// set manually, a day will be chosen for you. Backups are taken every
    /// day, but backups taken on this day are preferred when selecting
    /// backups to retain for a longer period. If you don't set this manually,
    /// when backups are initially enabled, this may come back as
    /// <see cref="BackupScheduleDay.Scheduling"/> until the day is
    /// automatically selected.
    /// </summary>
    public BackupScheduleDay? Day { get; init; }

    /// <summary>
    /// When your backups will be taken, in UTC. A backup window is a
    /// two-hour span of time in which the backup may occur. For example,
    /// <see cref="BackupScheduleWindow.W10"/> indicates that your backups
    /// should be taken between 10:00 and 12:00. If you don't choose a backup
    /// window, the API automatically assigns one. If you don't set this
    /// manually, when backups are initially enabled, this may come back as
    /// <see cref="BackupScheduleWindow.Scheduling"/> until the window is
    /// automatically selected.
    /// </summary>
    public BackupScheduleWindow? Window { get; init; }
}

/// <summary>
/// The day of the week that your Linode's weekly backup is taken.
/// </summary>
public enum BackupScheduleDay
{
    /// <summary>
    /// Indicates a day of the week wasn't chosen.
    /// </summary>
    Scheduling,

    /// <summary>
    /// Indicates Sunday.
    /// </summary>
    Sunday,

    /// <summary>
    /// Indicates Monday.
    /// </summary>
    Monday,

    /// <summary>
    /// Indicates Tuesday.
    /// </summary>
    Tuesday,

    /// <summary>
    /// Indicates Wednesday.
    /// </summary>
    Wednesday,

    /// <summary>
    /// Indicates Thursday.
    /// </summary>
    Thursday,

    /// <summary>
    /// Indicates Friday.
    /// </summary>
    Friday,

    /// <summary>
    /// Indicates Saturday.
    /// </summary>
    Saturday
}

/// <summary>
/// A backup window is a two-hour span of time in which the backup may occur.
/// </summary>
public enum BackupScheduleWindow
{
    /// <summary>
    /// A backup window hasn't been set.
    /// </summary>
    Scheduling,

    /// <summary>
    /// Between 0:00 and 2:00.
    /// </summary>
    W0,

    /// <summary>
    /// Between 2:00 and 4:00.
    /// </summary>
    W2,

    /// <summary>
    /// Between 4:00 and 6:00.
    /// </summary>
    W4,

    /// <summary>
    /// Between 6:00 and 8:00.
    /// </summary>
    W6,

    /// <summary>
    /// Between 8:00 and 10:00.
    /// </summary>
    W8,

    /// <summary>
    /// Between 10:00 and 12:00.
    /// </summary>
    W10,

    /// <summary>
    /// Between 12:00 and 14:00.
    /// </summary>
    W12,

    /// <summary>
    /// Between 14:00 and 16:00.
    /// </summary>
    W14,

    /// <summary>
    /// Between 16:00 and 18:00.
    /// </summary>
    W16,

    /// <summary>
    /// Between 18:00 and 20:00.
    /// </summary>
    W18,

    /// <summary>
    /// Between 20:00 and 22:00.
    /// </summary>
    W20,

    /// <summary>
    /// Between 22:00 and 24:00.
    /// </summary>
    W22
}

/// <summary>
/// Types of hypervisors powering a Linode.
/// </summary>
public enum HypervisorType
{
    /// <summary>
    /// Indicates KVM.
    /// </summary>
    Kvm
}

/// <summary>
/// The type of interface used to generate the Linode.
/// </summary>
public enum InterfaceGenerationType
{
    /// <summary>
    /// Indicates legacy configuration profile interface.
    /// </summary>
    LegacyConfig,

    /// <summary>
    /// Indicates Linode interface.
    /// </summary>
    Linode
}

/// <summary>
/// The selected maintenance policy setting is used (whenever possible) during
/// planned maintenance. During some maintenance, it may not be possible to
/// perform a migration in which your Linode remains operational. In these
/// cases, the system will fall back to the power off / power on method and
/// your Linode will experience some amount of downtime.
/// </summary>
/// <seealso href="https://techdocs.akamai.com/cloud-computing/docs/host-maintenance-policy"/>
public enum MaintenancePolicyType
{
    /// <summary>
    /// Recommended for maximizing availability. Migrates the Linode to a new
    /// host while it is still running.
    /// </summary>
    LinodeMigrate,

    /// <summary>
    /// Recommended for maximizing performance. Powers off the Linode at the
    /// start of the maintenance event and reboots it once the maintenance
    /// finishes.
    /// </summary>
    LinodePowerOffOn
}

/// <summary>
/// Details on the placement group that this Linode belongs to.
/// </summary>
/// <seealso href="https://techdocs.akamai.com/cloud-computing/docs/work-with-placement-groups"/>
public record PlacementGroup
{
    /// <summary>
    /// The placement group's ID. You need to provide it for all operations
    /// that affect it.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The unique name set for the placement group.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// The unique identifier for the placement group to which this Linode is
    /// being migrated. Displayed as <see langword="null"/> if the Linode is
    /// not being migrated to a new placement group.
    /// </summary>
    public int? MigratingTo { get; init; }

    /// <summary>
    /// How requests to add future Linodes to your placement group are
    /// handled, and whether it remains compliant.
    /// </summary>
    public PlacementGroupPolicyType PlacementGroupPolicy { get; init; }

    /// <summary>
    /// How Linodes are distributed in your placement group.
    /// </summary>
    public PlacementGroupType PlacementGroupType { get; init; }

    /// <summary>
    /// The region where you've deployed the Linode.
    /// </summary>
    public required string Region { get; init; }

    /// <summary>
    /// The Linode region's site type.
    /// </summary>
    public SiteType SiteType { get; init; }
}

/// <summary>
/// Indicates how requests to add future Linodes to a placement group are
/// handled.
/// </summary>
public enum PlacementGroupPolicyType
{
    /// <summary>
    /// Don't assign a new Linode if it breaks the grouped-together or
    /// spread-apart model set by the <see cref="PlacementGroupType"/>. Use
    /// this to ensure the placement group stays compliant
    /// (<c>is_compliant: true</c>).
    /// </summary>
    Strict,

    /// <summary>
    /// Assign a new Linode, even if it breaks the grouped-together or
    /// spread-apart model set by the <see cref="PlacementGroupType"/>. This
    /// makes the group non-compliant (<c>is_compliant: false</c>). You need
    /// to wait for Akamai to move the offending Linode to make it compliant
    /// again, once the necessary capacity is available in the region. Offers
    /// flexibility to add future Linodes if compliance isn't an immediate
    /// concern.
    /// </summary>
    Flexible
}

/// <summary>
/// How Linodes are distributed in your placement group.
/// </summary>
public enum PlacementGroupType
{
    /// <summary>
    /// Places Linodes in separate hosts but still in the same region.
    /// </summary>
    AntiAffinityLocal
}

/// <summary>
/// A Linode region's site type.
/// </summary>
public enum SiteType
{
    /// <summary>
    /// Indicates a traditional cloud computing region that offers all compute
    /// services.
    /// </summary>
    Core,

    /// <summary>
    /// Indicates sites that are globally dispersed to be closer to end users
    /// and workloads. These regions offer limited services.
    /// </summary>
    Distributed
}

/// <summary>
/// Information about the resources available to a Linode.
/// </summary>
public record LinodeSpecs
{
    /// <summary>
    /// The number of video processing units (VPU) this Linode has access to.
    /// This applies to a NETINT Quadra T1U VPU-backed accelerated Linode.
    /// Displayed as 0 for all non-accelerated Linodes.
    /// </summary>
    public int AcceleratedDevices { get; init; }

    /// <summary>
    /// The amount of storage space, in MB, this Linode has access to. A
    /// typical Linode divides this space between a primary disk with an
    /// <see cref="LinodeInstance.Image"/> image deployed to it, and a swap
    /// disk, usually 512 MB. This is the default configuration created when
    /// deploying a Linode with an image through Create a Linode.
    /// </summary>
    public int Disk { get; init; }

    /// <summary>
    /// The number of graphical processing units (GPU) this Linode has access
    /// to. This applies to a GPU Linode. Displayed as <c>0</c> for all
    /// non-GPU Linodes.
    /// </summary>
    public int Gpus { get; init; }

    /// <summary>
    /// The amount of RAM, in MB, this Linode has access to. Typically, a
    /// Linode boots with all of its available RAM, but this can be configured
    /// in a config profile.
    /// </summary>
    public int Memory { get; init; }

    /// <summary>
    /// The amount of network transfer this Linode is allotted each month.
    /// </summary>
    public int Transfer { get; init; }

    /// <summary>
    /// The number of virtual central processing units (vCPU) this Linode has
    /// access to. This applies to a high memory Linode. Displayed as <c>0</c>
    /// for all non-high memory Linodes.
    /// </summary>
    public int Vcpus { get; init; }
}

/// <summary>
/// Indicates the Linode instance's current state.
/// </summary>
public enum LinodeStatus
{
    /// <summary>
    /// Indicates running.
    /// </summary>
    Running,

    /// <summary>
    /// Indicates offline.
    /// </summary>
    Offline,

    /// <summary>
    /// Indicates booting.
    /// </summary>
    Booting,

    /// <summary>
    /// Indicates you've assigned the Linode to a placement group, but the
    /// Linode is currently booting. Once the boot completes, the API
    /// completes the assignment and updates the Linode's
    /// <see cref="LinodeInstance.Status"/> accordingly.
    /// </summary>
    Busy,

    /// <summary>
    /// Indicates rebooting.
    /// </summary>
    Rebooting,

    /// <summary>
    /// Indicates shutting down.
    /// </summary>
    ShuttingDown,

    /// <summary>
    /// Indicates that the API is applying operating system or Marketplace
    /// applications on the Linode.
    /// </summary>
    Provisioning,

    /// <summary>
    /// Indicates deleting.
    /// </summary>
    Deleting,

    /// <summary>
    /// Indicates migrating.
    /// </summary>
    Migrating,

    /// <summary>
    /// Indicates rebuilding.
    /// </summary>
    Rebuilding,

    /// <summary>
    /// Indicates cloning.
    /// </summary>
    Cloning,

    /// <summary>
    /// Indicates restoring.
    /// </summary>
    Restoring,

    /// <summary>
    /// Indicates stopped.
    /// </summary>
    Stopped,

    /// <summary>
    /// Indicates that payment is past due on the Linode, so its use has been
    /// suspended.
    /// </summary>
    BillingSuspension
}
