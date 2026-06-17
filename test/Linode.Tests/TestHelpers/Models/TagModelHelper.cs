using Linode.Models.Tags;

namespace Linode.Tests.TestHelpers.Models;

public static class TagModelHelper
{
    public const string DefaultTagsJsonResponse = """
                                                  {
                                                    "label": "example tag"
                                                  }
                                                  """;

    public static readonly Tag DefaultTag = new()
    {
        Label = "example tag"
    };
}
