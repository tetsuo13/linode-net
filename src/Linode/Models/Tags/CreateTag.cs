using Linode.Models.Tags.Internal;
using Linode.Operations;

namespace Linode.Models.Tags;

/// <summary>
/// New tag information.
/// </summary>
public record CreateTag
{
    /// <summary>
    /// The name of your tag. This is used for display purposes.
    /// </summary>
    public required string Label { get; set; }

    /// <summary>
    /// The <c>id</c> values for the domains where you want to apply the tag.
    /// You need <c>read_write</c> access to each domain. If you don't, the
    /// API won't create the tag and you'll receive an error. You can run the
    /// <seealso cref="IDomainsOperation.List"/> domains operation to store
    /// the <c>id</c> for desired domains and to review any <c>tags</c>
    /// currently applied.
    /// </summary>
    public List<int>? Domains { get; set; }

    /// <summary>
    /// The <c>id</c> values for the NodeBalancers where you want to apply the
    /// tag. You need <c>read_write</c> access to each NodeBalancer. If you
    /// don't, the API won't create the tag and you'll receive an error. You
    /// can run the List NodeBalancers operation to store the <c>id</c> for
    /// desired NodeBalancers and to review any <c>tags</c> currently applied.
    /// </summary>
    public List<int>? NodeBalancers { get; set; }

    /// <summary>
    /// The <c>id</c> values for the Linode volumes where you want to apply
    /// the tag. You need <c>read_write</c> access to each volume. If you
    /// don't, the API won't create the tag and you'll receive an error. You
    /// can run the List volumes operation to store the <c>id</c> for desired
    /// Linode volumes and to review any <c>tags</c> currently applied.
    /// </summary>
    public List<int>? Volumes { get; set; }

    internal CreateTagRequest ToRequest() => new()
    {
        Domains = Domains,
        Label = Label,
        NodeBalancers = NodeBalancers,
        Volumes = Volumes
    };
}
