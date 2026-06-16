using Linode.Models.Domains;

namespace Linode.Tests.TestHelpers.Models;

public static class DomainModelHelper
{
    // lang=json
    public const string DefaultDomainJsonResponse = """
                                                     {
                                                       "axfr_ips": [],
                                                       "description": null,
                                                       "domain": "example.org",
                                                       "expire_sec": 300,
                                                       "id": 1234,
                                                       "master_ips": [],
                                                       "refresh_sec": 301,
                                                       "retry_sec": 302,
                                                       "soa_email": "admin@example.org",
                                                       "status": "active",
                                                       "tags": [
                                                         "example tag",
                                                         "another example"
                                                       ],
                                                       "ttl_sec": 303,
                                                       "type": "master"
                                                     }
                                                     """;

    public static readonly Domain DefaultDomain = new()
    {
        AxfrIps = [],
        DomainName = "example.org",
        ExpireSec = 300,
        Id = 1234,
        MasterIps = [],
        RefreshSec = 301,
        RetrySec = 302,
        SoaEmail = "admin@example.org",
        Status = DomainStatus.Active,
        Tags = ["example tag", "another example"],
        TtlExp = 303,
        Type = DomainType.Master
    };
}
