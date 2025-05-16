using Magato.Api;

using NetArchTest.Rules;

using Xunit;

namespace Magato.ArchTests;

public sealed class InterfaceUsageTests
{
    [Fact]
    public void Controllers_Should_Depend_Only_On_Slim_Interfaces()
{
        var result = Types.InAssembly(typeof(ApiAssemblyMarker).Assembly)
                          .That().HaveNameEndingWith("Controller")
                          .ShouldNot().HaveDependencyOn("Magato.Api.Services.BigFatInterface")
                          .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
