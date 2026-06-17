using Linode.Models.Domains;

namespace Linode.Tests.TestHelpers.Models;

public static class DomainRecordsModelHelper
{
    public const string DefaultDomainRecordJsonResponse = """
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

    public static readonly DomainRecord DefaultDomainRecord = new()
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
}
