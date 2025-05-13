using Magato.Api;

using NetArchTest.Rules;

using Xunit;

namespace Magato.ArchTests;

public sealed class DependencyInversionTests
{
    [Fact]
    public void Controllers_should_not_depend_on_Repositories_or_Data()
    {
        var result = Types.InAssembly(typeof(ApiAssemblyMarker).Assembly)
                          .That().ResideInNamespace("Magato.Api.Controllers..")
                          .ShouldNot().HaveDependencyOnAny(
                              "Magato.Api.Repositories",
                              "Magato.Api.Data")
                          .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Services_should_not_depend_on_DbContext()
    {
        var result = Types.InAssembly(typeof(ApiAssemblyMarker).Assembly)
                          .That().ResideInNamespace("Magato.Api.Services..")
                          .ShouldNot().HaveDependencyOn("Magato.Api.Data.ApplicationDbContext")
                          .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
