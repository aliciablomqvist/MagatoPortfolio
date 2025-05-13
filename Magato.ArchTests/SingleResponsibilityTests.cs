using System.Linq;
using System.Reflection;

using FluentAssertions;

using Xunit;

namespace Magato.ArchTests;

public sealed class SingleResponsibilityTests
{
    private const int MaxPublicMethods = 15;

    [Fact]
    public void Service_Classes_Should_Not_Have_Too_Many_Public_Methods()
    {
        var services = typeof(Magato.Api.ApiAssemblyMarker).Assembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") &&
                        !t.IsInterface &&
                        !t.IsAbstract);

        foreach (var svc in services)
        {
            var count = svc.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                           .Count(m => !m.IsSpecialName);

            count.Should()
                 .BeLessThanOrEqualTo(
                     MaxPublicMethods,
                     $"{svc.Name} appears to carry multiple responsibilities (> {MaxPublicMethods} methods)");
        }
    }
}
