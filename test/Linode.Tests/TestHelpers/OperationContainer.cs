using System.Net;
using Linode.Models;
using Linode.Transport;

namespace Linode.Tests.TestHelpers;

internal sealed class OperationContainer : IDisposable
{
    private MockHttpMessageHandler? _httpMessageHandler;
    private HttpClient? _httpClient;

    ~OperationContainer()
    {
        Dispose(false);
    }

    public TOperation Create<TOperation>()
        where TOperation : class, new() =>
        Create<TOperation>(HttpStatusCode.OK, []);

    public TOperation Create<TOperation>(string jsonResponse)
        where TOperation : class, new() =>
        Create<TOperation>(HttpStatusCode.OK, [jsonResponse]);

    public TOperation Create<TOperation>(List<string> jsonResponses)
        where TOperation : class, new() =>
        Create<TOperation>(HttpStatusCode.OK, jsonResponses);

    public TOperation Create<TOperation>(HttpStatusCode statusCode, List<string> jsonResponses)
        where TOperation : class, new()
    {
        _httpMessageHandler = new MockHttpMessageHandler(statusCode, jsonResponses);
        _httpClient = new HttpClient(_httpMessageHandler);
        _httpClient.BaseAddress = new Uri("https://api.linode.com/v4");

        var httpConnection = new HttpConnection(_httpClient);

        var operation = Activator.CreateInstance(typeof(TOperation), httpConnection) as TOperation;
        Assert.NotNull(operation);

        return operation;
    }

    public static void AssertErrorResponse<TResponse>(TResponse response, string expectedReason)
        where TResponse : Response
    {
        Assert.False(response.Successful);
        Assert.NotNull(response.Errors);
        Assert.Single(response.Errors);
        Assert.Null(response.Errors[0].Field);
        Assert.Equal(expectedReason, response.Errors[0].Reason);
    }

    public static void AssertValidDomainResponse<T>(Response<T> response, T expectedData)
        where T : class
    {
        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Equivalent(expectedData, response.Data);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _httpMessageHandler?.Dispose();
            _httpMessageHandler = null;
            _httpClient?.Dispose();
            _httpClient = null;
        }
    }
}
