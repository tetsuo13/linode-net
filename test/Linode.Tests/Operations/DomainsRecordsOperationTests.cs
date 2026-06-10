using Linode.Models.Domains;
using Linode.Operations;
using Linode.Tests.TestHelpers;

namespace Linode.Tests.Operations;

public class DomainsRecordsOperationTests
{
    // lang=json
    private const string DefaultDomainRecordJsonResponse = """
                                                           {
                                                             "created": "2018-01-01T00:01:01",
                                                             "id": 123456,
                                                             "name": "test",
                                                             "port": 80,
                                                             "priority": 50,
                                                             "protocol": null,
                                                             "service": null,
                                                             "tag": null,
                                                             "target": "192.0.2.0",
                                                             "ttl_sec": 604800,
                                                             "type": "A",
                                                             "updated": "2018-01-01T00:01:01",
                                                             "weight": 50
                                                           }
                                                           """;

    private readonly DomainRecord _defaultDomainRecord = new()
    {
        Created = new DateTime(2018, 1, 1, 0, 1, 1, DateTimeKind.Utc),
        Id = 123456,
        Name = "test",
        Port = 80,
        Priority = 50,
        Target = "192.0.2.0",
        TtlSec = 604800,
        Type = DomainRecordType.A,
        Updated = new DateTime(2018, 1, 1, 0, 1, 1, DateTimeKind.Utc),
        Weight = 50
    };

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
        var operation = container.Create<DomainsRecordsOperation>([DefaultDomainRecordJsonResponse]);
        var response = await operation.Create(42, model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, _defaultDomainRecord);
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
                                        "data": [{{DefaultDomainRecordJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>(jsonResponse);
        var response = await operation.List(42, TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(_defaultDomainRecord, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
              {
                "data": [{{DefaultDomainRecordJsonResponse}}],
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

        var expected2 = _defaultDomainRecord with
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

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(_defaultDomainRecord, response.Data[0]);
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
        var operation = container.Create<DomainsRecordsOperation>([DefaultDomainRecordJsonResponse]);
        var response = await operation.Get(42, 13, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, _defaultDomainRecord);
    }

    [Fact]
    public async Task Update_Ok()
    {
        var model = new UpdateDomainRecord
        {
            Name = "test",
        };

        using var container = new OperationContainer();
        var operation = container.Create<DomainsRecordsOperation>([DefaultDomainRecordJsonResponse]);
        var response = await operation.Update(42, 13, model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, _defaultDomainRecord);
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

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
    }
}
