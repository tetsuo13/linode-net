namespace Linode.Models.Tags;

/// <summary>
/// A tag you've created to apply to objects on your account. Tags are for
/// organizational purposes only.
/// </summary>
public record Tag
{
    /// <summary>
    /// The name of the tag used for organization of objects on your account.
    /// </summary>
    public required string Label { get; init; }
}
