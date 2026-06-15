using System.Net;
using System.Text;
using Linode.Models;
using Linode.Models.Tags;
using Linode.Models.Volumes;
using Linode.Operations;
using Linode.Tests.TestHelpers;

namespace Linode.Tests.Operations;

public class TagsOperationTests
{
    // lang=json
    private const string DefaultTagsJsonResponse = """
                                                   {
                                                     "label": "example tag"
                                                   }
                                                   """;

    private readonly Tag _defaultTag = new() { Label = "example tag" };

    private const string DefaultVolumeJsonResponse = """
                                                     {
                                                       "created": "2025-01-01T00:01:01",
                                                       "encryption": "enabled",
                                                       "filesystem_path": "/dev/disk/by-id/scsi-0Linode_Volume_my-volume",
                                                       "hardware_type": "nvme",
                                                       "id": 12345,
                                                       "io_ready": true,
                                                       "label": "Video-file-storage",
                                                       "linode_id": 12346,
                                                       "linode_label": "linode123",
                                                       "locks": [
                                                         "cannot_delete"
                                                       ],
                                                       "region": "us-iad",
                                                       "size": 30,
                                                       "status": "active",
                                                       "tags": [
                                                         "blk-stg-volume-1",
                                                         "videos-storage"
                                                       ],
                                                       "updated": "2025-01-01T00:01:01"
                                                     }
                                                     """;

    private readonly Volume _defaultVolume = new()
    {
        Created = new DateTime(2025, 1, 1, 0, 1, 1),
        Toggle = ToggleType.Enabled,
        FileSystemPath = "/dev/disk/by-id/scsi-0Linode_Volume_my-volume",
        HardwareType = HardwareType.Nvme,
        Id = 12345,
        IoReady = true,
        Label = "Video-file-storage",
        LinodeId = 12346,
        LinodeLabel = "linode123",
        Locks = ["cannot_delete"],
        Region = "us-iad",
        Size = 30,
        Status = VolumeStatus.Active,
        Tags =
        [
            "blk-stg-volume-1",
            "videos-storage"
        ],
        Updated = new DateTime(2025, 1, 1, 0, 1, 1)
    };

    // lang=json
    private const string DefaultLinodeJsonResponse = """
                                                     {
                                                       "alerts": {
                                                         "cpu": 180,
                                                         "io": 10000,
                                                         "network_in": 10,
                                                         "network_out": 10,
                                                         "transfer_quota": 80
                                                       },
                                                       "backups": {
                                                         "available": true,
                                                         "enabled": true,
                                                         "last_successful": "2018-01-01T00:01:01",
                                                         "schedule": {
                                                           "day": "Saturday",
                                                           "window": "W22"
                                                         }
                                                       },
                                                       "capabilities": [
                                                         "Block Storage Encryption"
                                                       ],
                                                       "created": "2018-01-01T00:01:01",
                                                       "disk_encryption": "disabled",
                                                       "group": "Linode-Group",
                                                       "has_user_data": true,
                                                       "host_uuid": "1a2bcd34e5f67gh8ij901234567kl89mn01opqr2",
                                                       "hypervisor": "kvm",
                                                       "id": 123,
                                                       "image": "linode/debian13",
                                                       "interface_generation": "linode",
                                                       "ipv4": [
                                                         "203.0.113.1",
                                                         "192.0.2.1"
                                                       ],
                                                       "ipv6": "2001:DB8::/128",
                                                       "label": "linode123",
                                                       "lke_cluster_id": 1,
                                                       "placement_group": {
                                                         "id": 528,
                                                         "label": "PG_Miami_failover",
                                                         "placement_group_policy": "strict",
                                                         "placement_group_type": "anti-affinity:local"
                                                       },
                                                       "region": "us-east",
                                                       "specs": {
                                                         "disk": 81920,
                                                         "gpus": 0,
                                                         "memory": 4096,
                                                         "transfer": 4000,
                                                         "vcpus": 2
                                                       },
                                                       "status": "running",
                                                       "tags": [
                                                         "example tag",
                                                         "another example"
                                                       ],
                                                       "type": "g6-standard-1",
                                                       "updated": "2018-01-01T00:01:01",
                                                       "watchdog_enabled": true
                                                     }
                                                     """;

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

    [Fact]
    public async Task List_ReturnsOneTag()
    {
        // lang=json
        const string jsonResponse = $$"""
                                      {
                                        "data": [{{DefaultTagsJsonResponse}}],
                                        "page": 1,
                                        "pages": 1,
                                        "results": 1
                                      }
                                      """;

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(jsonResponse);
        var response = await operation.List(TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equivalent(_defaultTag, response.Data[0]);
    }

    [Fact]
    public async Task List_ReturnsTwoPages()
    {
        var jsonResponses = new List<string>
        {
            $$"""
              {
                "data": [{{DefaultTagsJsonResponse}}],
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

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Equal(2, response.Data.Count);
        Assert.Equivalent(_defaultTag, response.Data[0]);
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
        var operation = container.Create<TagsOperation>(statusCode, [json]);
        var response = await operation.List(TestContext.Current.CancellationToken);

        OperationContainer.AssertErrorResponse(response, reason);
    }

    [Fact]
    public async Task Create_Ok()
    {
        var model = new CreateTag { Label = "example tag" };

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(DefaultTagsJsonResponse);
        var response = await operation.Create(model, TestContext.Current.CancellationToken);

        OperationContainer.AssertValidDomainResponse(response, new Tag { Label = "example tag" });
    }

    [Fact]
    public async Task Delete_Ok()
    {
        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>();
        var response = await operation.Delete("example tag", TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
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

    [Fact]
    public async Task ListTaggedObjects_ReturnsOneTag()
    {
        var taggedObjects = new List<KeyValuePair<TaggedObjectType, string>>
        {
            new(TaggedObjectType.Volume, DefaultVolumeJsonResponse)
        };
        var jsonResponse = GenerateTaggedObjectsJsonResponse(taggedObjects);

        using var container = new OperationContainer();
        var operation = container.Create<TagsOperation>(jsonResponse);
        var response = await operation.ListTaggedObjects("derp", TestContext.Current.CancellationToken);

        Assert.True(response.Successful);
        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Single(response.Data);
        Assert.Equal(TaggedObjectType.Volume, response.Data[0].Type);
        Assert.Equivalent(_defaultVolume, response.Data[0].Data);
    }
}
