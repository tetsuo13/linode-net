using System.Text.Json;

namespace Linode.Helpers;

internal static class JsonHelpers
{
    public static async Task<string> GetChildObjectFromJson(HttpContent content, string topLevelElement,
        CancellationToken cancellationToken)
    {
        var jsonResponse = await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        using var doc = JsonDocument.Parse(jsonResponse);
        return doc.RootElement.GetProperty(topLevelElement).GetRawText();
    }
}
