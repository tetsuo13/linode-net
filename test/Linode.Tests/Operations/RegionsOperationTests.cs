using System.Net;
using Linode.Models;
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
                  "axfr_ips": [],
                  "description": null,
                  "domain": "example.com",
                  "expire_sec": 400,
                  "id": 5678,
                  "master_ips": [],
                  "refresh_sec": 401,
                  "retry_sec": 402,
                  "soa_email": "admin@example.com",
                  "status": "active",
                  "tags": [
                    "a tag",
                    "another example"
                  ],
                  "ttl_sec": 403,
                  "type": "master"
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
            DomainName = "example.com",
            ExpireSec = 400,
            Id = 5678,
            RefreshSec = 401,
            RetrySec = 402,
            SoaEmail = "admin@example.com",
            Tags = ["a tag", "another example"],
            TtlExp = 403
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(jsonResponses);
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
        var operation = container.Create<DomainsOperation>(statusCode, [json]);
        var response = await operation.List(TestContext.Current.CancellationToken);

        AssertErrorResponse(response, reason);
    }

    private static void AssertErrorResponse<TResponse>(TResponse response, string expectedReason)
        where TResponse : Response
    {
        Assert.False(response.Successful);
        Assert.NotNull(response.Errors);
        Assert.Single(response.Errors);
        Assert.Null(response.Errors[0].Field);
        Assert.Equal(expectedReason, response.Errors[0].Reason);
    }
}
