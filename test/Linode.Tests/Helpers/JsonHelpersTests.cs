using Linode.Helpers;

namespace Linode.Tests.Helpers;

public class JsonHelpersTests
{
    [Fact]
    public async Task GetChildObjectFromJson_TopLevelElementExists_IsRemoved()
    {
        // lang=json
        const string json = """
                            {
                              "errors": [{ "reason": "soa_email required when type=master", "field": "soa_email" }]
                            }
                            """;

        // lang=json
        const string expected = """
                                [{ "reason": "soa_email required when type=master", "field": "soa_email" }]
                                """;
        using var content = new StringContent(json);
        var actual = await JsonHelpers.GetChildObjectFromJson(content, "errors", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }
}
