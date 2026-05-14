using Microsoft.Extensions.DependencyInjection;

namespace Linode.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public async Task RegisterLinodeClient_ValidPat_ReturnsClient()
    {
        await using var serviceProvider = new ServiceCollection()
            .AddLinodeApi("pat")
            .BuildServiceProvider();

        var client = serviceProvider.GetRequiredService<ILinodeClient>();

        Assert.NotNull(client);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void RegisterLinodeClient_EmptyPat_ThrowsException(string pat)
    {
        var serviceProvider = new ServiceCollection();
        Assert.Throws<ArgumentException>(() => serviceProvider.AddLinodeApi(pat));
    }

    [Fact]
    public void RegisterLinodeClient_NullPat_ThrowsException()
    {
        var serviceProvider = new ServiceCollection();
        Assert.Throws<ArgumentNullException>(() => serviceProvider.AddLinodeApi(null!));
    }
}
