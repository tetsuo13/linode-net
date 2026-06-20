using System.Net;
using Linode.Models.MaintenancePolicies;
using Linode.Operations;
using Linode.Tests.TestHelpers;
using Linode.Tests.TestHelpers.Models;

namespace Linode.Tests.Operations;

public class MaintenancePoliciesOperationTests
{
    [Fact]
    public async Task List_ReturnsOneDomain()
    {
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{MaintenancePoliciesModelHelper.DefaultPolicyJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<MaintenancePoliciesOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(MaintenancePoliciesModelHelper.DefaultPolicy, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
            {
              "data": [{{MaintenancePoliciesModelHelper.DefaultPolicyJsonResponse}}],
              "page": 1,
              "pages": 2,
              "results": 2
            }
            """,
            """
            {
              "data": [
                {
                  "description": "Powers off the Linode at the start of the maintenance event and reboots it once the maintenance finishes. Recommended for maximizing performance.",
                  "is_default": false,
                  "label": "Power Off / Power On",
                  "notification_period_sec": 604800,
                  "slug": "linode/power_off_on",
                  "type": "power_off_on"
                }
              ],
              "page": 2,
              "pages": 2,
              "results": 2
            }
            """
        };

        var expected2 = new MaintenancePolicy
        {
            Description = "Powers off the Linode at the start of the maintenance event and reboots it once the maintenance finishes. Recommended for maximizing performance.",
            IsDefault = false,
            Label = "Power Off / Power On",
            NotificationPeriodSec = 604_800,
            Slug = "linode/power_off_on",
            Type = PolicyType.PowerOffOn
        };

        using var container = new OperationContainer();
        var operation = container.Create<MaintenancePoliciesOperation>(jsonResponses);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(MaintenancePoliciesModelHelper.DefaultPolicy, response.Data[0]);
        Assert.Equivalent(expected2, response.Data[1]);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task List_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<MaintenancePoliciesOperation>(statusCode, [json]);
        var response = await operation.List(TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }
}
