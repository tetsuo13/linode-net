using Linode.Models.NodeBalancers;

namespace Linode.Tests.TestHelpers.Models;

public static class NodeBalancerModelHelper
{
    public const string DefaultJsonResponse = """
                                              {
                                                "client_conn_throttle": 10,
                                                "created": "2018-01-01T00:01:01",
                                                "hostname": "192.0.2.1.ip.linodeusercontent.com",
                                                "id": 12345,
                                                "ipv4": "203.0.113.1",
                                                "ipv6": null,
                                                "label": "balancer12345",
                                                "lke_cluster": {
                                                  "id": 12345,
                                                  "label": "lkecluster12345",
                                                  "type": "lkecluster",
                                                  "url": "/v4/lke/clusters/12345"
                                                },
                                                "region": "us-east",
                                                "tags": [
                                                  "example tag",
                                                  "another example"
                                                ],
                                                "transfer": {
                                                  "in": 28.91200828552246,
                                                  "out": 3.5487728118896484,
                                                  "total": 32.46078109741211
                                                },
                                                "type": "premium",
                                                "updated": "2018-03-01T00:01:01"
                                              }
                                              """;

    public static readonly NodeBalancer DefaultNodeBalancer = new()
    {
        ClientConnectionThrottle = 10,
        Created = new DateTime(2018, 1, 1, 0, 1, 1),
        Hostname = "192.0.2.1.ip.linodeusercontent.com",
        Id = 12345,
        Ipv4 = "203.0.113.1",
        Ipv6 = null,
        Label = "balancer12345",
        LkeCluster = new LkeCluster
        {
            Id = 12345,
            Label = "lkecluster12345",
            Type = "lkecluster",
            Url = "/v4/lke/clusters/12345"
        },
        Region = "us-east",
        Tags =
        [
            "example tag",
            "another example"
        ],
        Transfer = new Transfer
        {
            In = 28.91200828552246M,
            Out = 3.5487728118896484M,
            Total = 32.46078109741211M
        },
        Type = NodeBalancerType.Premium,
        Updated = new DateTime(2018, 3, 1, 0, 1, 1)
    };
}
