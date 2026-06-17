using System.Text.Json;
using System.Text.Json.Serialization;

namespace Linode.Helpers;

/// <summary>
/// Converter to convert enums to and from uppercase strings.
/// </summary>
/// <typeparam name="TEnum">The enum type that this converter targets.</typeparam>
internal sealed class AllCapsEnumJsonConverter<TEnum> : JsonStringEnumConverter<TEnum>
    where TEnum : struct, Enum
{
    public AllCapsEnumJsonConverter()
        : base(JsonNamingPolicy.KebabCaseUpper)
    {
    }
}
