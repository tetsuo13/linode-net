using System.Text.Json.Serialization;

namespace Linode.Models.Internal;

/// <summary>
/// A paginated collection of <typeparamref name="T"/> objects.
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed record PagedData<T>
{
    /// <summary>
    /// An array of objects.
    /// </summary>
    [JsonPropertyName("data")]
    public required List<T> Data { get; init; }

    /// <summary>
    /// The current page.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; init; }

    /// <summary>
    /// The total number of pages.
    /// </summary>
    [JsonPropertyName("pages")]
    public int Pages { get; init; }

    /// <summary>
    /// The total number of results.
    /// </summary>
    [JsonPropertyName("results")]
    public int Results { get; init; }
}
