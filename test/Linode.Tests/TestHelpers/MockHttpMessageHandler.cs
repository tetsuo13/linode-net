using System.Net;
using System.Text;

namespace Linode.Tests.TestHelpers;

internal class MockHttpMessageHandler : HttpMessageHandler
{
    private int _currentResponseIndex = 0;

    private readonly HttpStatusCode _statusCode;
    private readonly List<string> _responseContent;
    private readonly bool _throwException;

    public MockHttpMessageHandler(List<string> responseContent)
        : this(HttpStatusCode.OK, responseContent)
    {
    }

    public MockHttpMessageHandler(HttpStatusCode statusCode, List<string> responseContent)
    {
        _statusCode = statusCode;
        _responseContent = responseContent;
        _throwException = false;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (_throwException)
        {
            throw new HttpRequestException("Simulated exception");
        }

        if (_responseContent.Count == 0)
        {
            return  Task.FromResult(new HttpResponseMessage(_statusCode));
        }

        if (_currentResponseIndex < _responseContent.Count)
        {
            return Task.FromResult(new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_responseContent[_currentResponseIndex++], Encoding.UTF8,
                    "application/json")
            });
        }

        var error =
            $"Called {nameof(SendAsync)} {_currentResponseIndex + 1} times but only mocked {_responseContent.Count} responses";

        throw new IndexOutOfRangeException(error);
    }
}
