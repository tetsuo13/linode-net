using Linode.Models.Regions;

namespace Linode.Tests.TestHelpers.Models;

public static class RegionModelHelper
{
    public const string DefaultRegionJsonResponse = """
                                                    {
                                                      "capabilities": [
                                                        "Linodes",
                                                        "Block Storage Encryption",
                                                        "Disk Encryption",
                                                        "Backups",
                                                        "NodeBalancers",
                                                        "Block Storage",
                                                        "Object Storage",
                                                        "GPU Linodes",
                                                        "Kubernetes",
                                                        "Cloud Firewall",
                                                        "Vlans",
                                                        "Block Storage Migrations",
                                                        "Managed Databases",
                                                        "Metadata",
                                                        "Placement Group",
                                                        "StackScripts",
                                                        "Maintenance Policy",
                                                        "Linode Interfaces"
                                                      ],
                                                      "country": "us",
                                                      "id": "us-east",
                                                      "label": "Newark, NJ",
                                                      "monitors": {
                                                        "alerts": [
                                                          "Managed Databases",
                                                          "NodeBalancers"
                                                        ],
                                                        "metrics": [
                                                          "Managed Databases",
                                                          "NodeBalancers"
                                                        ]
                                                      },
                                                      "placement_group_limits": {
                                                        "maximum_linodes_per_flexible_pg": 5,
                                                        "maximum_linodes_per_pg": 5,
                                                        "maximum_pgs_per_customer": null
                                                      },
                                                      "resolvers": {
                                                        "ipv4": "66.228.42.5,96.126.106.5,50.116.53.5,50.116.58.5,50.116.61.5,50.116.62.5,66.175.211.5,97.107.133.4,173.255.225.5,66.228.35.5",
                                                        "ipv6": "2600:3c03::7,2600:3c03::4,2600:3c03::9,2600:3c03::6,2600:3c03::3,2600:3c03::c,2600:3c03::5,2600:3c03::b,2600:3c03::2,2600:3c03::8"
                                                      },
                                                      "site_type": "core",
                                                      "status": "ok"
                                                    }
                                                    """;

    public static readonly Region DefaultRegion = new()
    {
        Capabilities =
        [
            "Linodes",
            "Block Storage Encryption",
            "Disk Encryption",
            "Backups",
            "NodeBalancers",
            "Block Storage",
            "Object Storage",
            "GPU Linodes",
            "Kubernetes",
            "Cloud Firewall",
            "Vlans",
            "Block Storage Migrations",
            "Managed Databases",
            "Metadata",
            "Placement Group",
            "StackScripts",
            "Maintenance Policy",
            "Linode Interfaces"
        ],
        Country = "us",
        Id = "us-east",
        Label = "Newark, NJ",
        Monitors = new Monitors
        {
            Alerts =
            [
                "Managed Databases",
                "NodeBalancers"
            ],
            Metrics =
            [
                "Managed Databases",
                "NodeBalancers"
            ]
        },
        PlacementGroupLimits = new PlacementGroupLimits
        {
            MaximumLinodesPerFlexiblePage = 5,
            MaximumLinodesPerPage = 5,
            MaximumPagesPerCustomer = null
        },
        Resolvers = new Resolvers
        {
            Ipv4 = "66.228.42.5,96.126.106.5,50.116.53.5,50.116.58.5,50.116.61.5,50.116.62.5,66.175.211.5,97.107.133.4,173.255.225.5,66.228.35.5",
            Ipv6 = "2600:3c03::7,2600:3c03::4,2600:3c03::9,2600:3c03::6,2600:3c03::3,2600:3c03::c,2600:3c03::5,2600:3c03::b,2600:3c03::2,2600:3c03::8"
        },
        SiteType = SiteType.Core,
        Status = RegionStatus.Ok
    };

    public const string DefaultRegionAvailabilityJsonResponse = """
                                                                {
                                                                  "available": true,
                                                                  "plan": "gpu-rtx6000-1.1",
                                                                  "region": "us-east"
                                                                }
                                                                """;

    public static readonly RegionAvailability DefaultRegionAvailability = new()
    {
        Available = true,
        Plan = "gpu-rtx6000-1.1",
        Region = "us-east"
    };
}
