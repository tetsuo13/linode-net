namespace Linode.Models.Tags;

/// <summary>
/// Object that has been tagged.
/// </summary>
public record TaggedObject
{
    /// <summary>
    /// Details on the specific object type the tag is assigned to.
    /// </summary>
    public required ITaggedObject Data { get; init; }

    /// <summary>
    /// The type of object the tag is applied to.
    /// </summary>
    public required TaggedObjectType Type { get; init; }
}

/// <summary>
/// The type of object the tag is applied to.
/// </summary>
public enum TaggedObjectType
{
    /// <summary>
    /// A domain object.
    /// </summary>
    Domain,

    /// <summary>
    /// A Linode object.
    /// </summary>
    Linode,

    /// <summary>
    /// A node balancer object.
    /// </summary>
    NodeBalancer,

    /// <summary>
    /// A volume object.
    /// </summary>
    Volume
}
