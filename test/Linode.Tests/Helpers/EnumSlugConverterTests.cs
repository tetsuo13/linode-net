using System.Text.Json;
using Linode.Helpers;

namespace Linode.Tests.Helpers;

public class EnumSlugConverterTests
{
    public enum MyEnum
    {
        LinodeMigrate,
        LinodePowerOffOn,
        AntiAffinityLocal
    }

    private readonly JsonSerializerOptions _options;

    public EnumSlugConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new EnumSlugConverterFactory());
    }

    [Theory]
    [InlineData("linode/migrate", MyEnum.LinodeMigrate)]
    [InlineData("linode/power_off_on", MyEnum.LinodePowerOffOn)]
    [InlineData("anti_affinity:local", MyEnum.AntiAffinityLocal)]
    public void ContainsInvalidSlugs(string slug, MyEnum expected)
    {
        var actual = JsonSerializer.Deserialize<MyEnum>($"\"{slug}\"", _options);

        Assert.Equal(expected, actual);
    }
}
