using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Magato.Api;
using Xunit;

namespace Magato.ArchTests;

public sealed class DiResolutionTests
{
    [Fact]
    public void All_Registered_Services_Should_Resolve()
    {
        var services = new ServiceCollection();
        var cfg = new ConfigurationBuilder().AddInMemoryCollection().Build();

        var envMock = new Mock<IWebHostEnvironment>();
        envMock.SetupGet(e => e.EnvironmentName).Returns("Testing");
        envMock.SetupGet(e => e.ApplicationName).Returns("Magato.Api");

        services.AddMagatoServices(cfg, envMock.Object);

        var provider = services.BuildServiceProvider(validateScopes: true);

        foreach (var descriptor in services)
            _ = provider.GetRequiredService(descriptor.ServiceType);
    }
}
