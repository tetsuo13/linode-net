using System.Net;
using System.Text;
using Linode.Models.Tags;
using Linode.Operations;
using Linode.Tests.TestHelpers;
using Linode.Tests.TestHelpers.Models;

namespace Linode.Tests.Operations;

public class TagsOperationTests
{
    [Fact]
    public async Task List_ReturnsOneTag()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{TagModelHelper.DefaultTagsJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(TagModelHelper.DefaultTag, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
              {
                "data": [{{TagModelHelper.DefaultTagsJsonResponse}}],
                "page": 1,
                "pages": 2,
                "results": 2
              }
              """,
            """
            {
              "data": [
                {
                  "label": "another tag"
                }
              ],
              "page": 2,
              "pages": 2,
              "results": 2
            }
            """
        };

        var expected2 = new Tag { Label = "another tag" };

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(jsonResponses);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(TagModelHelper.DefaultTag, response.Data[0]);
        Assert.Equivalent(expected2, response.Data[1]);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task List_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        var json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(statusCode, [json]);
        var response = await operation.List(TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task Create_Ok()
    {
        var model = new CreateTag { Label = "example tag" };

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(TagModelHelper.DefaultTagsJsonResponse);
        var response = await operation.Create(model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, new Tag { Label = "example tag" });
    }

    [Fact]
    public async Task Delete_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>();
        var response = await operation.Delete("example tag", TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
    }

    [Theory]
    [InlineData(null, typeof(ArgumentNullException))]
    [InlineData("", typeof(ArgumentException))]
    [InlineData(" ", typeof(ArgumentException))]
    public async Task Delete_InvalidId_ThrowsException(string? label, Type exceptionType)
    {
        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>();
        await Assert.ThrowsAsync(exceptionType, () => operation.Delete(label!, TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not found")]
    [InlineData(HttpStatusCode.Unauthorized, "Invalid Token")]
    public async Task Delete_InvalidHttpResponseStatus_ReturnsErrorResponse(HttpStatusCode statusCode, string reason)
    {
        // lang=json
        string json = $$"""{ "errors": [{ "reason": "{{reason}}" }] }""";

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(statusCode, [json]);
        var response = await operation.Delete("example", TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Theory]
    [InlineData(TaggedObjectType.Domain, DomainModelHelper.DefaultDomainJsonResponse)]
    [InlineData(TaggedObjectType.Linode, LinodeModelHelper.DefaultLinodeJsonResponse)]
    [InlineData(TaggedObjectType.NodeBalancer, NodeBalancerModelHelper.DefaultJsonResponse)]
    [InlineData(TaggedObjectType.Volume, VolumeModelHelper.DefaultVolumeJsonResponse)]
    public async Task ListTaggedObjects_ReturnsOneTag(TaggedObjectType taggedObjectType, string json)
    {
        var taggedObjects = new List<KeyValuePair<TaggedObjectType, string>>
        {
            new(taggedObjectType, json)
        };
        var jsonResponse = GenerateTaggedObjectsJsonResponse(taggedObjects);

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(jsonResponse);
        var response = await operation.ListTaggedObjects("derp", TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equal(taggedObjectType, response.Data[0].Type);

        switch (taggedObjectType)
        {
            case TaggedObjectType.Domain:
                Assert.Equivalent(DomainModelHelper.DefaultDomain, response.Data[0].Data);
                break;

            case TaggedObjectType.Linode:
                Assert.Equivalent(LinodeModelHelper.DefaultLinodeInstance, response.Data[0].Data);
                break;

            case TaggedObjectType.NodeBalancer:
                Assert.Equivalent(NodeBalancerModelHelper.DefaultNodeBalancer, response.Data[0].Data);
                break;

            case TaggedObjectType.Volume:
                Assert.Equivalent(VolumeModelHelper.DefaultVolume, response.Data[0].Data);
                break;

            default:
                throw new NotSupportedException($"Missing case for tagged object type {taggedObjectType}");
        }
    }

    [Fact]
    public async Task ListTaggedObjects_ReturnsMultiple()
    {
        var taggedObjects = new List<KeyValuePair<TaggedObjectType, string>>
        {
            new(TaggedObjectType.Domain, DomainModelHelper.DefaultDomainJsonResponse),
            new(TaggedObjectType.Volume, VolumeModelHelper.DefaultVolumeJsonResponse)
        };
        var jsonResponse = GenerateTaggedObjectsJsonResponse(taggedObjects);

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(jsonResponse);
        var response = await operation.ListTaggedObjects("derp", TestContext.Current.CancellationToken);

        Assert.Null(response.Errors);
        Assert.True(response.Successful);
        Assert.NotNull(response.Data);
        Assert.Equal(taggedObjects.Count, response.Data.Count);

        Assert.Equal(TaggedObjectType.Domain, response.Data[0].Type);
        Assert.Equivalent(DomainModelHelper.DefaultDomain, response.Data[0].Data);

        Assert.Equal(TaggedObjectType.Volume, response.Data[1].Type);
        Assert.Equivalent(VolumeModelHelper.DefaultVolume, response.Data[1].Data);
    }

    private static string GenerateTaggedObjectsJsonResponse(List<KeyValuePair<TaggedObjectType, string>> taggedObjects)
    {
        var json = new StringBuilder();
        json.AppendLine("""
                        {
                          "data": [
                        """);

        foreach (var kvp in taggedObjects)
        {
            json.Append('{');
            json.Append($"\"data\": {kvp.Value},");
            json.Append($"\"type\": \"{kvp.Key.ToString().ToLowerInvariant()}\"");
            json.Append("},");
        }

        // Remove trailing comma.
        json.Remove(json.Length - 1, 1);

        json.AppendLine("""
                          ],
                          "page": 1,
                          "pages": 1,
                          "results": 1
                        }
                        """);

        return json.ToString();
    }
}
