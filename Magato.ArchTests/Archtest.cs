using NetArchTest.Rules;

using Xunit;

public class ArchitectureTests
{
    [Fact]
    public void Controllers_should_not_DependOn_EnitityFramework()
{
        var result = Types.InAssembly(typeof(Magato.Api.ApiAssemblyMarker).Assembly)
                          .That()
                          .HaveNameEndingWith("Controller")
                          .ShouldNot()
                          .HaveDependencyOn("Microsoft.EntityFrameworkCore")
                          .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
