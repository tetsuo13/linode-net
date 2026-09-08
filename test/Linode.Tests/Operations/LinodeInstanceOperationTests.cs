using System.Net;
using Linode.Operations;
using Linode.Tests.TestHelpers;
using Linode.Tests.TestHelpers.Models;

namespace Linode.Tests.Operations;

public class LinodeInstanceOperationTests
{
    [Fact]
    public async Task List_ReturnsOneDomain()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{LinodeModelHelper.DefaultLinodeJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(LinodeModelHelper.DefaultLinodeInstance, response.Data[0]);
    }

    [Theory]
    [InlineData(0)]
    public async Task Get_Params_ThrowsException(int linodeId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        await Assert.ThrowsAnyAsync<Exception>(() => operation.Get(linodeId, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task Get_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>([LinodeModelHelper.DefaultLinodeJsonResponse]);
        var response = await operation.Get(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, LinodeModelHelper.DefaultLinodeInstance);
    }

    [Fact]
    public async Task Delete_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        var response = await operation.Delete(42, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }

    [Fact]
    public async Task Delete_InvalidId_ThrowsException()
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
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
        var operation = container.Create<LinodeInstanceOperation>(statusCode, [json]);
        var response = await operation.Delete(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Theory]
    [InlineData(42, null)]
    [InlineData(42, 13)]
    public async Task Boot_Ok(int linodeId, int? configId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        var response = await operation.Boot(linodeId, configId, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }

    [Theory]
    [InlineData(0, null)]
    [InlineData(0, 13)]
    public async Task Boot_InvalidId_ThrowsException(int linodeId, int? configId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            operation.Boot(linodeId, configId, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task Boot_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>(statusCode, [json]);
        var response = await operation.Boot(42, null, TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Theory]
    [InlineData(42, null)]
    [InlineData(42, 13)]
    public async Task Reboot_Ok(int linodeId, int? configId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        var response = await operation.Reboot(linodeId, configId, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }

    [Theory]
    [InlineData(0, null)]
    [InlineData(0, 13)]
    public async Task Reboot_InvalidId_ThrowsException(int linodeId, int? configId)
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            operation.Reboot(linodeId, configId, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task Reboot_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>(statusCode, [json]);
        var response = await operation.Reboot(42, null, TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task Shutdown_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        var response = await operation.Shutdown(42, TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }

    [Fact]
    public async Task Shutdown_InvalidId_ThrowsException()
    {
        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>();
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            operation.Shutdown(0, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task Shutdown_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<LinodeInstanceOperation>(statusCode, [json]);
        var response = await operation.Shutdown(42, TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }
}
