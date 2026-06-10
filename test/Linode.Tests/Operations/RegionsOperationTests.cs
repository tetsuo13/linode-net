using System.Net;
using Linode.Models.Regions;
using Linode.Operations;
using Linode.Tests.TestHelpers;

namespace Linode.Tests.Operations;

public class RegionsOperationTests
{
    // lang=json
    private const string DefaultRegionJsonResponse = """
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

    private readonly Region _defaultRegion = new()
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

    // lang=json
    private const string DefaultRegionAvailabilityJsonResponse = """
                                                                 {
                                                                   "available": true,
                                                                   "plan": "gpu-rtx6000-1.1",
                                                                   "region": "us-east"
                                                                 }
                                                                 """;

    private readonly RegionAvailability _defaultRegionAvailability = new()
    {
        Available = true,
        Plan = "gpu-rtx6000-1.1",
        Region = "us-east"
    };

    [Fact]
    public async Task List_ReturnsOneRegion()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{DefaultRegionJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(_defaultRegion, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
            {
              "data": [{{DefaultRegionJsonResponse}}],
              "page": 1,
              "pages": 2,
              "results": 2
            }
            """,
            """
            {
              "data": [
                {
                  "capabilities": [
                    "Linodes",
                    "Block Storage Encryption"
                  ],
                  "country": "us",
                  "id": "us-west",
                  "label": "Fremont, CA",
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
                    "ipv4": "173.230.145.5, 173.230.147.5, 173.230.155.5, 173.255.212.5, 173.255.219.5, 173.255.241.5, 173.255.243.5, 173.255.244.5, 74.207.241.5, 74.207.242.5",
                    "ipv6": "2600:3c01::2, 2600:3c01::9, 2600:3c01::5, 2600:3c01::7, 2600:3c01::3, 2600:3c01::8, 2600:3c01::4, 2600:3c01::b, 2600:3c01::c, 2600:3c01::6"
                  },
                  "site_type": "core",
                  "status": "ok"
                }
              ],
              "page": 2,
              "pages": 2,
              "results": 2
            }
            """
        };

        var expected2 = _defaultRegion with
        {
            Capabilities =
            [
                "Linodes",
                "Block Storage Encryption"
            ],
            Country = "us",
            Id = "us-west",
            Label = "Fremont, CA",
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
                Ipv4 = "173.230.145.5, 173.230.147.5, 173.230.155.5, 173.255.212.5, 173.255.219.5, 173.255.241.5, 173.255.243.5, 173.255.244.5, 74.207.241.5, 74.207.242.5",
                Ipv6 = "2600:3c01::2, 2600:3c01::9, 2600:3c01::5, 2600:3c01::7, 2600:3c01::3, 2600:3c01::8, 2600:3c01::4, 2600:3c01::b, 2600:3c01::c, 2600:3c01::6"
            },
            SiteType = SiteType.Core,
            Status = RegionStatus.Ok
        };

        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>(jsonResponses);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(_defaultRegion, response.Data[0]);
        Assert.Equivalent(expected2, response.Data[1]);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task List_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>(statusCode, [json]);
        var response = await operation.List(TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task ListAvailability_ReturnsOneRegion()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{DefaultRegionAvailabilityJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>(jsonResponse);
        var response = await operation.ListAvailability(TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(_defaultRegionAvailability, response.Data[0]);
    }

    [Fact]
    public async Task ListAvailability_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
            {
              "data": [{{DefaultRegionAvailabilityJsonResponse}}],
              "page": 1,
              "pages": 2,
              "results": 2
            }
            """,
            """
            {
              "data": [
                {
                  "available": true,
                  "plan": "gpu-rtx6000-1.2",
                  "region": "us-west"
                }
              ],
              "page": 2,
              "pages": 2,
              "results": 2
            }
            """
        };

        var expected2 = new RegionAvailability
        {
            Available = true,
            Plan = "gpu-rtx6000-1.2",
            Region = "us-west"
        };

        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>(jsonResponses);
        var response = await operation.ListAvailability(TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(_defaultRegionAvailability, response.Data[0]);
        Assert.Equivalent(expected2, response.Data[1]);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task ListAvailability_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode,
        string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>(statusCode, [json]);
        var response = await operation.ListAvailability(TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task Get_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>([DefaultRegionJsonResponse]);
        var response = await operation.Get(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, _defaultRegion);
    }

    [Fact]
    public async Task GetAvailability_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<RegionsOperation>([DefaultRegionAvailabilityJsonResponse]);
        var response = await operation.GetAvailability(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, _defaultRegionAvailability);
    }
}
