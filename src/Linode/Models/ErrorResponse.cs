namespace Linode.Models;

/// <summary>
/// An error response consists of several objects you can use to help with
/// troubleshooting.
/// </summary>
public sealed record ErrorResponse
{
    /// <summary>
    /// This is a human-readable explanation of the error.
    /// </summary>
    public required string Reason { get; init; }

    /// <summary>
    /// This pertains to an error based on a specific field in any JSON you've
    /// submitted. The API omits this from an error response if there isn't a
    /// relevant field.
    /// </summary>
    public string? Field { get; init; }
}
