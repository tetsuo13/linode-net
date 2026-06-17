using System.Text.Json;
using System.Text.Json.Serialization;

namespace Linode.Helpers;

/// <summary>
/// Converter to strip invalid characters from enum values. Invalid characters
/// are those that aren't allowed as an <see cref="Enum"/> value.
/// </summary>
internal sealed class EnumSlugConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(EnumSlugConverterInner<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private sealed class EnumSlugConverterInner<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString() ?? throw new JsonException("Expected string value for enum");

            var normalized = s
                .Replace("/", string.Empty)
                .Replace("_", string.Empty)
                .Replace(":", string.Empty);

            return Enum.TryParse(normalized, true, out TEnum result)
                ? result
                : throw new JsonException($"Unknown enum value '{s}' (normalized to '{normalized})");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
