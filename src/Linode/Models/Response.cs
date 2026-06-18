namespace Linode.Models;

/// <summary>
/// The basic response object used for all responses from HTTP requests.
/// </summary>
public record Response
{
    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    public bool Successful { get; private init; }

    /// <summary>
    /// This array lists the things that went wrong with your request. It
    /// includes as many of the problems in the response as possible.
    /// </summary>
    public IReadOnlyList<ErrorResponse>? Errors { get; private init; }

    internal static Response Success() => new()
    {
        Successful = true
    };

    internal static Response<T> Success<T>(T data) => new()
    {
        Successful = true,
        Data = data
    };

    internal static Response Failure(List<ErrorResponse> errorResponse) => new()
    {
        Successful = false,
        Errors = errorResponse
    };

    internal static Response<T> Failure<T>(List<ErrorResponse>? errorResponse) => new()
    {
        Successful = false,
        Errors = errorResponse,
        Data = default
    };
}

/// <summary>
/// The basic response object used for all responses from HTTP requests that
/// contains a typed data response object.
/// </summary>
/// <typeparam name="T">The type matching the data.</typeparam>
public sealed record Response<T> : Response
{
    /// <summary>
    /// The response data.
    /// </summary>
    public required T? Data { get; init; }
}
