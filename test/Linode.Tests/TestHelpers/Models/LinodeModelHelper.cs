using Linode.Models;
using Linode.Models.Linode;

namespace Linode.Tests.TestHelpers.Models;

public static class LinodeModelHelper
{
    public const string DefaultLinodeJsonResponse = """
                                                    {
                                                      "alerts": {
                                                        "cpu": 180,
                                                        "io": 10000,
                                                        "network_in": 10,
                                                        "network_out": 10,
                                                        "transfer_quota": 80
                                                      },
                                                      "backups": {
                                                        "available": true,
                                                        "enabled": true,
                                                        "last_successful": "2018-01-01T00:01:01",
                                                        "schedule": {
                                                          "day": "Saturday",
                                                          "window": "W22"
                                                        }
                                                      },
                                                      "capabilities": [
                                                        "Block Storage Encryption"
                                                      ],
                                                      "created": "2018-01-01T00:01:01",
                                                      "disk_encryption": "disabled",
                                                      "has_user_data": true,
                                                      "host_uuid": "1a2bcd34e5f67gh8ij901234567kl89mn01opqr2",
                                                      "hypervisor": "kvm",
                                                      "id": 123,
                                                      "image": "linode/debian10",
                                                      "interface_generation": "linode",
                                                      "ipv4": [
                                                        "203.0.113.1",
                                                        "192.0.2.1"
                                                      ],
                                                      "ipv6": "2001:DB8::/128",
                                                      "label": "linode123",
                                                      "lke_cluster_id": 1,
                                                      "locks": [
                                                        "cannot_delete"
                                                      ],
                                                      "maintenance_policy": "linode/migrate",
                                                      "placement_group": {
                                                        "id": 528,
                                                        "label": "PG_Miami_failover",
                                                        "migrating_to": 2468,
                                                        "placement_group_policy": "strict",
                                                        "placement_group_type": "anti_affinity:local"
                                                      },
                                                      "region": "us-east",
                                                      "site_type": "core",
                                                      "specs": {
                                                        "accelerated_devices": 0,
                                                        "disk": 81920,
                                                        "gpus": 0,
                                                        "memory": 4096,
                                                        "transfer": 4000,
                                                        "vcpus": 2
                                                      },
                                                      "status": "running",
                                                      "tags": [
                                                        "example tag",
                                                        "another example"
                                                      ],
                                                      "type": "g6-standard-1",
                                                      "updated": "2018-01-01T00:01:01",
                                                      "watchdog_enabled": true
                                                    }
                                                    """;

    public static readonly LinodeInstance DefaultLinodeInstance = new()
    {
        Alerts = new Alerts
        {
            Cpu = 180,
            Io = 10_000,
            NetworkIn = 10,
            NetworkOut = 10,
            TransferQuota = 80
        },
        Backups = new Backups
        {
            Available = true,
            Enabled = true,
            LastSuccessful = new DateTime(2018, 1, 1, 0, 1, 1),
            Schedule = new BackupSchedule
            {
                Day = BackupScheduleDay.Saturday,
                Window = BackupScheduleWindow.W22
            }
        },
        Capabilities = ["Block Storage Encryption"],
        Created = new DateTime(2018, 1, 1, 0, 1, 1),
        DiskEncryption = ToggleType.Disabled,
        HasUserData = true,
        HostUuid = "1a2bcd34e5f67gh8ij901234567kl89mn01opqr2",
        Hypervisor = HypervisorType.Kvm,
        Id = 123,
        Image = "linode/debian10",
        InterfaceGeneration = InterfaceGenerationType.Linode,
        Ipv4 =
        [
            "203.0.113.1",
            "192.0.2.1"
        ],
        Ipv6 = "2001:DB8::/128",
        Label = "linode123",
        LkeClusterId = 1,
        Locks = ["cannot_delete"],
        MaintenancePolicy = MaintenancePolicyType.LinodeMigrate,
        PlacementGroup = new PlacementGroup
        {
            Id = 528,
            Label = "PG_Miami_failover",
            MigratingTo = 2468,
            PlacementGroupPolicy = PlacementGroupPolicyType.Strict,
            PlacementGroupType = PlacementGroupType.AntiAffinityLocal
        },
        Region = "us-east",
        SiteType = SiteType.Core,
        Specs = new LinodeSpecs
        {
            AcceleratedDevices = 0,
            Disk = 81_920,
            Gpus = 0,
            Memory = 4096,
            Transfer = 4000,
            Vcpus = 2
        },
        Status = LinodeStatus.Running,
        Tags =
        [
            "example tag",
            "another example"
        ],
        Type = "g6-standard-1",
        Updated = new DateTime(2018, 1, 1, 0, 1, 1),
        WatchdogEnabled = true
    };
}
