namespace Linode.Models.Tags;

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
