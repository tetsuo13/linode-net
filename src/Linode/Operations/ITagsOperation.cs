using Linode.Models;
using Linode.Models.Tags;

namespace Linode.Operations;

/// <summary>
/// Operations related to tags on the account.
/// </summary>
public interface ITagsOperation
{
    /// <summary>
    /// Creates a new tag and lets you optionally add it to specific objects.
    /// Tags are labels you can attach to objects in your account. Use them to
    /// specify and group attributes of objects that are relevant to you.
    /// Currently, you can add a tag to your <c>linodes</c>, your
    /// <c>nodebalancers</c>, the <c>domains</c> for your Linodes, and the
    /// <c>volumes</c> on your Linodes.
    /// </summary>
    /// <param name="createTag">The new tag to create.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns><see cref="Response"/> object with the new tag.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/post-tag"/>
    Task<Response<Tag>> Create(CreateTag createTag, CancellationToken cancellationToken);

    /// <summary>
    /// Returns a paginated list of tags you've created on your account. This
    /// operation can only be accessed by account users with
    /// <i>unrestricted</i> access. Talk to your local account administrator
    /// about access management.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of tags.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-tags"/>
    Task<Response<IReadOnlyList<Tag>>> List(CancellationToken cancellationToken);

    /// <summary>
    /// Returns a paginated list of all objects you've tagged with the
    /// specified tag. The response includes a mixed collection of all object
    /// types. This operation can only be accessed by account users with
    /// <i>unrestricted access</i>. Talk to your local account administrator
    /// about access management.
    /// </summary>
    /// <param name="tagLabel">The label of the tag to access.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns>
    /// <see cref="Response"/> object with a collection of tagged objects.
    /// </returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/get-tagged-objects"/>
    Task<Response<IReadOnlyList<TaggedObject>>> ListTaggedObjects(string tagLabel,
        CancellationToken cancellationToken);

    /// <summary>
    /// Removes a tag from all objects and deletes it. This operation can only
    /// be accessed by account users with <i>unrestricted access</i>. Talk to
    /// your local account administrator about access management.
    /// </summary>
    /// <param name="tagLabel">The label of the tag to access.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to
    /// receive notice of cancellation.
    /// </param>
    /// <returns><see cref="Response"/> object indicating result.</returns>
    /// <seealso href="https://techdocs.akamai.com/linode-api/reference/delete-tag"/>
    Task<Response> Delete(string tagLabel, CancellationToken cancellationToken);
}
