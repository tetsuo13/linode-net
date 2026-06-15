using System.Text.Json;
using Linode.Models;
using Linode.Models.Internal;
using Linode.Models.Tags;
using Linode.Models.Tags.Internal;
using Linode.Transport;

namespace Linode.Operations;

internal sealed class TagsOperation : ITagsOperation
{
    private const string BasePath = "tags";

    private readonly IHttpConnection _httpConnection;

    public TagsOperation() =>
        throw new InvalidOperationException("Parameterless constructor exists for unit tests only");

    public TagsOperation(IHttpConnection httpConnection)
    {
        _httpConnection = httpConnection;
    }

    public async Task<Response<Tag>> Create(CreateTag createTag, CancellationToken cancellationToken) =>
        await _httpConnection.PostRequest<Tag, CreateTagRequest, TagResponse>($"{BasePath}",
            createTag.ToRequest(), cancellationToken).ConfigureAwait(false);

    public async Task<Response<IReadOnlyList<Tag>>> List(CancellationToken cancellationToken) =>
        await _httpConnection.GetPagedResult<Tag, TagResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);

    public async Task<Response<IReadOnlyList<TaggedObject>>> ListTaggedObjects(string tagLabel,
        CancellationToken cancellationToken) =>
        await _httpConnection.GetPagedResult<TaggedObject, TaggedObjectResponse>(BasePath, cancellationToken)
            .ConfigureAwait(false);

    public async Task<Response> Delete(string tagLabel, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tagLabel);

        using var response = await _httpConnection.HttpClient.DeleteAsync($"{BasePath}/{tagLabel}", cancellationToken)
            .ConfigureAwait(false);

        var httpResponseError = await _httpConnection.CheckForHttpResponseErrors<Tag>(response, cancellationToken)
            .ConfigureAwait(false);

        return httpResponseError.HasError ? httpResponseError.ErrorResponse : Response.Success();
    }
}
