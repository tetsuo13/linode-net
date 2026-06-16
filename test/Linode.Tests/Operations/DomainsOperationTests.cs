using System.Net;
using Linode.Models.Domains;
using Linode.Operations;
using Linode.Tests.TestHelpers;
using Linode.Tests.TestHelpers.Models;

namespace Linode.Tests.Operations;

public class DomainsOperationTests
{
    [Fact]
    public async Task Create_Ok()
    {
        var model = new CreateDomain
        {
            DomainName = "example.org",
            ExpireSec = 300,
            RefreshSec = 301,
            RetrySec = 302,
            SoaEmail = "admin@example.org",
            Status = DomainStatus.Active,
            Type = DomainType.Master,
            Tags = ["example tag", "another tag"],
            TtlExp = 303
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(DomainModelHelper.DefaultDomainJsonResponse);
        var response = await operation.Create(model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainModelHelper.DefaultDomain);
    }

    [Fact]
    public async Task List_ReturnsOneDomain()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{DomainModelHelper.DefaultDomainJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(DomainModelHelper.DefaultDomain, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
            {
              "data": [{{DomainModelHelper.DefaultDomainJsonResponse}}],
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

        var expected2 = DomainModelHelper.DefaultDomain with
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

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(DomainModelHelper.DefaultDomain, response.Data[0]);
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

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task ImportFromRemoteNameserver_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>([DomainModelHelper.DefaultDomainJsonResponse]);
        var response = await operation.ImportFromRemoteNameserver("example.com", "examplenameserver.com",
            TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainModelHelper.DefaultDomain);
    }

    [Theory]
    [InlineData(null, "example.org")]
    [InlineData("", "example.org")]
    [InlineData("example.org", null)]
    [InlineData("example.org", "")]
    public async Task ImportFromRemoteNameserver_InvalidParams_ThrowsException(string? name, string? remoteNameserver)
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.ImportFromRemoteNameserver(name!, remoteNameserver!,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Get_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>([DomainModelHelper.DefaultDomainJsonResponse]);
        var response = await operation.Get(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainModelHelper.DefaultDomain);
    }

    [Fact]
    public async Task Update_Ok()
    {
        var model = new UpdateDomain
        {
            DomainName = "example.org",
            RetrySec = 302,
            TtlExp = 303
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>([DomainModelHelper.DefaultDomainJsonResponse]);
        var response = await operation.Update(42, model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainModelHelper.DefaultDomain);
    }

    [Fact]
    public async Task Delete_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>();
        var response = await operation.Delete(42, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }

    [Fact]
    public async Task Delete_InvalidId_ThrowsException()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>();
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            operation.Delete(0, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task Delete_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(statusCode, [json]);
        var response = await operation.Delete(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task GetDomainZoneFile_Ok()
    {
        // lang=json
        const string jsonResponse = """
                                    {
                                      "zone_file": [
                                        "; example.com [123]",
                                        "$TTL 864000",
                                        "@  IN  SOA  ns1.linode.com. user.example.com. 2021000066 14400 14400 1209600 86400",
                                        "@    NS  ns1.linode.com.",
                                        "@    NS  ns2.linode.com.",
                                        "@    NS  ns3.linode.com.",
                                        "@    NS  ns4.linode.com.",
                                        "@    NS  ns5.linode.com."
                                      ]
                                    }
                                    """;
        var expected = new List<string>
        {
            "; example.com [123]",
            "$TTL 864000",
            "@  IN  SOA  ns1.linode.com. user.example.com. 2021000066 14400 14400 1209600 86400",
            "@    NS  ns1.linode.com.",
            "@    NS  ns2.linode.com.",
            "@    NS  ns3.linode.com.",
            "@    NS  ns4.linode.com.",
            "@    NS  ns5.linode.com."
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(jsonResponse);
        var response = await operation.GetDomainZoneFile(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, expected);
    }

    [Fact]
    public async Task GetDomainZoneFile_InvalidId_ThrowsException()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>();
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            operation.GetDomainZoneFile(0, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task GetDomainZoneFile_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode,
        string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(statusCode, [json]);
        var response = await operation.GetDomainZoneFile(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Theory]
    [InlineData(0, "example.org")]
    [InlineData(42, null)]
    [InlineData(42, "")]
    [InlineData(42, " ")]
    public async Task Clone_InvalidParams_ThrowsException(int id, string? targetName)
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.Clone(id, targetName!,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Clone_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsOperation>(DomainModelHelper.DefaultDomainJsonResponse);
        var response = await operation.Clone(42, "example.org", TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainModelHelper.DefaultDomain);
    }
}
