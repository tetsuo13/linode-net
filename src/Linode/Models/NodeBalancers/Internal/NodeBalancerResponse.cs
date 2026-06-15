using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.NodeBalancers.Internal;

internal sealed record NodeBalancerResponse : IMapsTo<NodeBalancer>
{
    [JsonPropertyName("client_conn_throttle")]
    public int ClientConnectionThrottle { get; init; }

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }

    [JsonPropertyName("hostname")]
    public required string Hostname { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("ipv4")]
    public required string Ipv4 { get; init; }

    [JsonPropertyName("ipv6")]
    public string? Ipv6 { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("lke_cluster")]
    public LkeClusterResponse? LkeCluster  {get; init; }

    [JsonPropertyName("locks")]
    public IReadOnlyList<string>? Locks { get; init; }

    [JsonPropertyName("region")]
    public required string Region { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string> Tags { get; init; } = [];

    [JsonPropertyName("transfer")]
    public required TransferResponse Transfer { get; init; }

    [JsonPropertyName("type")]
    public NodeBalancerType Type { get; init; }

    [JsonPropertyName("updated")]
    public DateTime Updated { get; init; }

    public NodeBalancer ToDomain()
    {
        LkeCluster? lkeCluster = null;

        if (LkeCluster is not null)
        {
            lkeCluster = new LkeCluster
            {
                Id = LkeCluster.Id, Label = LkeCluster.Label, Type = LkeCluster.Type, Url = LkeCluster.Url
            };
        }

        return new NodeBalancer
        {
            ClientConnectionThrottle = ClientConnectionThrottle,
            Created = Created,
            Hostname = Hostname,
            Id = Id,
            Ipv4 = Ipv4,
            Ipv6 = Ipv6,
            Label = Label,
            LkeCluster = lkeCluster,
            Locks = Locks,
            Region = Region,
            Tags = Tags,
            Transfer = new Transfer { In = Transfer.In, Out = Transfer.Out, Total = Transfer.Total },
            Type = Type,
            Updated = Updated
        };
    }
}

internal sealed record LkeClusterResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("url")]
    public required string Url { get; init; }
}

internal sealed record TransferResponse
{
    [JsonPropertyName("in")]
    public decimal? In { get; init; }

    [JsonPropertyName("out")]
    public decimal? Out { get; init; }

    [JsonPropertyName("total")]
    public decimal? Total { get; init; }
}
