using System.Text.Json;
using Linode.Helpers;

namespace Linode.Tests.Helpers;

public class EnumSlugConverterTests
{
    public enum TargetType
    {
        LinodeMigrate,
        LinodePowerOffOn,
        AntiAffinityLocal
    }

    private readonly JsonSerializerOptions _options;

    public EnumSlugConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new EnumSlugConverter());
    }

    [Theory]
    [InlineData("linode/migrate", TargetType.LinodeMigrate)]
    [InlineData("linode/power_off_on", TargetType.LinodePowerOffOn)]
    [InlineData("anti_affinity:local", TargetType.AntiAffinityLocal)]
    public void InvalidCharacters_ConvertedToEnum(string slug, TargetType expected)
    {
        var actual = JsonSerializer.Deserialize<TargetType>($"\"{slug}\"", _options);

        Assert.Equal(expected, actual);
    }
}
