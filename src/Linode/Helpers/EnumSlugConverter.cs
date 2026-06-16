using System.Text.Json;
using System.Text.Json.Serialization;

namespace Linode.Helpers;

internal sealed class EnumSlugConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString() ?? throw new JsonException("Expected string value for enum");

        var normalized = s
            .Replace("/", string.Empty)
            .Replace("_", string.Empty)
            .Replace(":", string.Empty);

        if (Enum.TryParse(normalized, true, out TEnum result))
        {
            return result;
        }

        throw new JsonException($"Unknown enum value '{s}' (normalized to '{normalized})");
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}
