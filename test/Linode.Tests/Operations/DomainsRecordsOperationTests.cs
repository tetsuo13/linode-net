using Linode.Models.Domains;
using Linode.Operations;
using Linode.Tests.TestHelpers;
using Linode.Tests.TestHelpers.Models;

namespace Linode.Tests.Operations;

public class DomainsRecordsOperationTests
{
    [Theory]
    [InlineData(0)]
    public async Task Create_InvalidDomainIdParam_ThrowsException(int domainId)
    {
        var model = new CreateDomainRecord { Type = DomainRecordType.A, Target = "192.0.2.0" };
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.Create(domainId, model,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Create_Ok()
    {
        var model = new CreateDomainRecord { Type = DomainRecordType.A, Target = "192.0.2.0" };
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>([DomainRecordsModelHelper.DefaultDomainRecordJsonResponse]);
        var response = await operation.Create(42, model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainRecordsModelHelper.DefaultDomainRecord);
    }

    [Theory]
    [InlineData(0)]
    public async Task List_InvalidDomainIdParam_ThrowsException(int domainId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.List(domainId, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task List_ReturnsOneDomainRecord()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{DomainRecordsModelHelper.DefaultDomainRecordJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>(jsonResponse);
        var response = await operation.List(42, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(DomainRecordsModelHelper.DefaultDomainRecord, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
              {
                "data": [{{DomainRecordsModelHelper.DefaultDomainRecordJsonResponse}}],
                "page": 1,
                "pages": 2,
                "results": 2
              }
              """,
            """
            {
              "data": [
                {
                  "created": "2019-01-01T00:01:01",
                  "id": 654321,
                  "name": "test-b",
                  "port": 80,
                  "priority": 55,
                  "protocol": null,
                  "service": null,
                  "tag": null,
                  "target": "192.0.3.0",
                  "ttl_sec": 604801,
                  "type": "A",
                  "updated": "2019-01-01T00:01:01",
                  "weight": 55
                }
              ],
              "page": 2,
              "pages": 2,
              "results": 2
            }
            """
        };

        var expected2 = DomainRecordsModelHelper.DefaultDomainRecord with
        {
            Created = new DateTime(2019, 1, 1, 0, 1, 1, DateTimeKind.Utc),
            Id = 654321,
            Name = "test-b",
            Priority = 55,
            Target = "192.0.3.0",
            TtlSec = 604801,
            Updated = new DateTime(2019, 1, 1, 0, 1, 1, DateTimeKind.Utc),
            Weight = 55
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>(jsonResponses);
        var response = await operation.List(42, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(DomainRecordsModelHelper.DefaultDomainRecord, response.Data[0]);
        Assert.Equivalent(expected2, response.Data[1]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(42, 0)]
    [InlineData(0, 42)]
    public async Task Get_Params_ThrowsException(int domainId, int recordId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.Get(domainId, recordId,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Get_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>([DomainRecordsModelHelper.DefaultDomainRecordJsonResponse]);
        var response = await operation.Get(42, 13, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainRecordsModelHelper.DefaultDomainRecord);
    }

    [Fact]
    public async Task Update_Ok()
    {
        var model = new UpdateDomainRecord
        {
            Name = "test",
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>([DomainRecordsModelHelper.DefaultDomainRecordJsonResponse]);
        var response = await operation.Update(42, 13, model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, DomainRecordsModelHelper.DefaultDomainRecord);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(42, 0)]
    [InlineData(0, 42)]
    public async Task Delete_Params_ThrowsException(int domainId, int recordId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.Delete(domainId, recordId,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Delete_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>();
        var response = await operation.Delete(42, 13, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }
}
