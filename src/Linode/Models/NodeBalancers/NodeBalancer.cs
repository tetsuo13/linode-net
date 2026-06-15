namespace Linode.Models.NodeBalancers;

/// <summary>
/// Linode's load balancing solution. Can handle multiple ports, SSL
/// termination, and any number of backends. NodeBalancer ports are configured
/// with NodeBalancer configs, and each config is given one or more
/// NodeBalancer nodes that accepts traffic. The traffic should be routed to
/// the NodeBalancer's IP address, for the NodeBalancer to handle routing
/// individual requests to backends.
/// </summary>
public record NodeBalancer : ITaggedObject
{
    /// <summary>
    /// Throttle TCP connections per second for TCP, HTTP, and HTTPS
    /// configurations. Set to <c>0</c> (zero) to disable throttling.
    /// </summary>
    public int ClientConnectionThrottle { get; init; }

    /// <summary>
    /// When this NodeBalancer was created.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// This NodeBalancer's hostname, beginning with its IP address and ending
    /// with <i>.ip.linodeusercontent.com</i>.
    /// </summary>
    public required string Hostname { get; init; }

    /// <summary>
    /// This NodeBalancer's unique ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// This NodeBalancer's public IPv4 address.
    /// </summary>
    public required string Ipv4 { get; init; }

    /// <summary>
    /// This NodeBalancer's public IPv6 address.
    /// </summary>
    public string? Ipv6 { get; init; }

    /// <summary>
    /// This NodeBalancer's label. These must be unique on your Account.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// This NodeBalancer's related LKE cluster, if any. The value is
    /// <see langword="null"/> if this NodeBalancer isn't related to an LKE
    /// cluster.
    /// </summary>
    public LkeCluster? LkeCluster  {get; init; }

    /// <summary>
    /// Indicates if this NodeBalancer is protected by a lock to prevent
    /// accidental deletion. If the NodeBalancer has a <c>cannot_delete</c>
    /// lock, it can't be deleted, but its configurations and backend nodes
    /// can. If the NodeBalancer has a <c>cannot_delete_with_subresources</c>
    /// lock, both the NodeBalancer and attached resources such as
    /// configurations and backend nodes can't be deleted. Only account
    /// administrators can remove locks using the Delete a resource lock
    /// operation.
    /// </summary>
    public IReadOnlyList<string>? Locks { get; init; }

    /// <summary>
    /// The Region where this NodeBalancer is located. NodeBalancers only
    /// support backends in the same Region.
    /// </summary>
    public required string Region { get; init; }

    /// <summary>
    /// An array of Tags applied to this object. Tags are for organizational
    /// purposes only.
    /// </summary>
    public IReadOnlyList<string> Tags { get; init; } = [];

    /// <summary>
    /// Information about the amount of transfer this NodeBalancer has had so
    /// far this month.
    /// </summary>
    public required Transfer Transfer { get; init; }

    /// <summary>
    /// The type of NodeBalancer.
    /// </summary>
    public NodeBalancerType Type { get; init; }

    /// <summary>
    /// When this NodeBalancer was last updated.
    /// </summary>
    public DateTime Updated { get; init; }
}

/// <summary>
/// Linode Kubernetes Engine (LKE) cluster info.
/// </summary>
public record LkeCluster
{
    /// <summary>
    /// The ID of the related LKE cluster.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// The label of the related LKE cluster.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// The type for LKE clusters.
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    /// The URL where you can access the related LKE cluster.
    /// </summary>
    public required string Url { get; init; }
}

/// <summary>
/// Info for a NodeBalacer's transfer for the month.
/// </summary>
public record Transfer
{
    /// <summary>
    /// The total outbound transfer, in MB, used for this NodeBalancer this
    /// month.
    /// </summary>
    public decimal? In { get; init; }

    /// <summary>
    /// The total inbound transfer, in MB, used for this NodeBalancer this
    /// month.
    /// </summary>
    public decimal? Out { get; init; }

    /// <summary>
    /// The total transfer, in MB, used by this NodeBalancer this month.
    /// </summary>
    public decimal? Total { get; init; }
}
