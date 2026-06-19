using System.Net;
using Linode.Models.NetworkTransferPrices;
using Linode.Operations;
using Linode.Tests.TestHelpers;
using Linode.Tests.TestHelpers.Models;

namespace Linode.Tests.Operations;

public class NetworkTransferPricesOperationTests
{
    [Fact]
    public async Task List_ReturnsOneDomain()
    {
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{NetworkTransferPricesModelHelper.DefaultPriceJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<NetworkTransferPricesOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(NetworkTransferPricesModelHelper.DefaultPrice, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
            {
              "data": [{{NetworkTransferPricesModelHelper.DefaultPriceJsonResponse}}],
              "page": 1,
              "pages": 2,
              "results": 2
            }
            """,
            """
            {
              "data": [
                {
                  "id": "network_transfer2",
                  "label": "Network Transfer 2",
                  "price": {
                    "hourly": 0.007,
                    "monthly": null
                  },
                  "region_prices": [
                    {
                      "hourly": 0.021,
                      "id": "us-west",
                      "monthly": null
                    }
                  ],
                  "transfer": 42
                }
              ],
              "page": 2,
              "pages": 2,
              "results": 2
            }
            """
        };

        var expected2 = NetworkTransferPricesModelHelper.DefaultPrice with
        {
            Id = "network_transfer2",
            Label = "Network Transfer 2",
            Price = new Price { Hourly = 0.007M },
            RegionPrices =
            [
                new RegionPrice { Hourly = 0.021M, Id = "us-west" }
            ],
            Transfer = 42
        };

        using var container = new OperationContainer();
        var operation = container.Create<NetworkTransferPricesOperation>(jsonResponses);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(NetworkTransferPricesModelHelper.DefaultPrice, response.Data[0]);
        Assert.Equivalent(expected2, response.Data[1]);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task List_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<NetworkTransferPricesOperation>(statusCode, [json]);
        var response = await operation.List(TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }
}
