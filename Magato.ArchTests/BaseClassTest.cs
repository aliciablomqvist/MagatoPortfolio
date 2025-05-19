using Magato.Api;

using NetArchTest.Rules;

using Xunit;

public class BaseClassTest
{
    [Fact]
    public void Application_layer_must_not_have_Base_classes()
{
        var types = Types.InAssembly(typeof(ApiAssemblyMarker).Assembly)
                         .That().HaveNameStartingWith("Base")
                         .GetTypes();

        Assert.True(!types.Any(), "There are *Base* classes in the Application layer");
    }
}
