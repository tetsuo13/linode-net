using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Linode.Internal;

internal sealed record LinodeInstanceResponse : IMapsTo<LinodeInstance>
{
    [JsonPropertyName("alerts")]
    public required AlertsResponse Alerts { get; init; }

    [JsonPropertyName("backups")]
    public required BackupsResponse Backups { get; init; }

    [JsonPropertyName("capabilities")]
    public IReadOnlyList<string> Capabilities { get; init; } = [];

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }

    [JsonPropertyName("disk_encryption")]
    public ToggleType? DiskEncryption { get; init; }

    [JsonPropertyName("has_user_data")]
    public bool HasUserData { get; init; }

    [JsonPropertyName("host_uuid")]
    public required string HostUuid { get; init; }

    [JsonPropertyName("hypervisor")]
    public HypervisorType Hypervisor { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("image")]
    public string? Image { get; init; }

    [JsonPropertyName("interface_generation")]
    public InterfaceGenerationType InterfaceGeneration { get; init; }

    [JsonPropertyName("ipv4")]
    public required IReadOnlyList<string> Ipv4 { get; init; }

    [JsonPropertyName("ipv6")]
    public string? Ipv6 { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("lke_cluster_id")]
    public int? LkeClusterId { get; init; }

    [JsonPropertyName("locks")]
    public IReadOnlyList<string> Locks { get; init; } = [];

    [JsonPropertyName("maintenance_policy")]
    public MaintenancePolicyTypeResponse MaintenancePolicy { get; init; }

    [JsonPropertyName("placement_group")]
    public PlacementGroupResponse? PlacementGroup { get; init; }

    [JsonPropertyName("region")]
    public required string Region { get; init; }

    [JsonPropertyName("site_type")]
    public SiteType SiteType { get; init; }

    [JsonPropertyName("specs")]
    public required LinodeSpecsResponse Specs { get; init; }

    [JsonPropertyName("status")]
    public LinodeStatus Status { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string> Tags { get; init; } = [];

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("updated")]
    public DateTime Updated { get; init; }

    [JsonPropertyName("watchdog_enabled")]
    public bool WatchdogEnabled { get; init; }

    public LinodeInstance ToDomain()
    {
        PlacementGroup? placementGroup = null;

        if (PlacementGroup is not null)
        {
            placementGroup = new PlacementGroup
            {
                Id = PlacementGroup.Id,
                Label = PlacementGroup.Label,
                MigratingTo = PlacementGroup.MigratingTo,
                PlacementGroupPolicy = PlacementGroup.PlacementGroupPolicy,
                PlacementGroupType = PlacementGroup.PlacementGroupType
            };
        }

        return new LinodeInstance
        {
            Alerts = new Alerts
            {
                Cpu = Alerts.Cpu,
                Io = Alerts.Io,
                NetworkIn = Alerts.NetworkIn,
                NetworkOut = Alerts.NetworkOut,
                TransferQuota = Alerts.TransferQuota
            },
            Backups = new Backups
            {
                Available = Backups.Available,
                Enabled = Backups.Enabled,
                LastSuccessful = Backups.LastSuccessful,
                Schedule = new BackupSchedule
                {
                    Day = Backups.Schedule.Day,
                    Window = Backups.Schedule.Window
                }
            },
            Capabilities = Capabilities,
            Created = Created,
            DiskEncryption = DiskEncryption,
            HasUserData = HasUserData,
            HostUuid = HostUuid,
            Hypervisor = Hypervisor,
            Id = Id,
            Image = Image,
            InterfaceGeneration = InterfaceGeneration,
            Ipv4 = Ipv4,
            Ipv6 = Ipv6,
            Label = Label,
            LkeClusterId = LkeClusterId,
            Locks = Locks,
            MaintenancePolicy = MaintenancePolicy switch
            {
                MaintenancePolicyTypeResponse.LinodeMigrate => MaintenancePolicyType.LinodeMigrate,
                MaintenancePolicyTypeResponse.LinodePowerOffOn => MaintenancePolicyType.LinodePowerOffOn,
                _ => throw new NotSupportedException()
            },
            PlacementGroup = placementGroup,
            Region = Region,
            SiteType = SiteType,
            Specs = new LinodeSpecs
            {
                AcceleratedDevices = Specs.AcceleratedDevices,
                Disk = Specs.Disk,
                Gpus = Specs.Gpus,
                Memory = Specs.Memory,
                Transfer = Specs.Transfer,
                Vcpus = Specs.Vcpus
            },
            Status = Status,
            Tags = Tags,
            Type = Type,
            Updated = Updated,
            WatchdogEnabled = WatchdogEnabled
        };
    }
}

internal sealed record AlertsResponse
{
    [JsonPropertyName("cpu")]
    public int Cpu { get; init; }

    [JsonPropertyName("io")]
    public int Io { get; init; }

    [JsonPropertyName("network_in")]
    public int NetworkIn { get; init; }

    [JsonPropertyName("network_out")]
    public int NetworkOut { get; init; }

    [JsonPropertyName("transfer_quota")]
    public int TransferQuota { get; init; }
}

internal sealed record BackupsResponse
{
    [JsonPropertyName("available")]
    public bool Available { get; init; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("last_successful")]
    public DateTime? LastSuccessful { get; init; }

    [JsonPropertyName("schedule")]
    public required BackupScheduleResponse Schedule { get; init; }
}

internal sealed record BackupScheduleResponse
{
    [JsonPropertyName("day")]
    public BackupScheduleDay? Day { get; init; }

    [JsonPropertyName("window")]
    public BackupScheduleWindow? Window { get; init; }
}

internal sealed record PlacementGroupResponse
{
    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("migrating_to")]
    public int? MigratingTo { get; init; }

    [JsonPropertyName("placement_group_policy")]
    public PlacementGroupPolicyType PlacementGroupPolicy { get; init; }

    [JsonPropertyName("placement_group_type")]
    public PlacementGroupType PlacementGroupType { get; init; }
}

internal sealed record LinodeSpecsResponse
{
    [JsonPropertyName("accelerated_devices")]
    public int AcceleratedDevices { get; init; }

    [JsonPropertyName("disk")]
    public int Disk { get; init; }

    [JsonPropertyName("gpus")]
    public int Gpus { get; init; }

    [JsonPropertyName("memory")]
    public int Memory { get; init; }

    [JsonPropertyName("transfer")]
    public int Transfer { get; init; }

    [JsonPropertyName("vcpus")]
    public int Vcpus { get; init; }
}

internal enum MaintenancePolicyTypeResponse
{
    [JsonPropertyName("linode/migrate")]
    LinodeMigrate,

    [JsonPropertyName("linode/power_off_on")]
    LinodePowerOffOn
}
