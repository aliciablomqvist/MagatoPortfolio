using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Magato.ArchTests;

public sealed class InterfaceSegregationTests
{
    private const int MaxMembers = 8;

    [Fact]
    public void No_Interface_Should_Contain_Too_Many_Members()
    {
        var interfaces = typeof(Magato.Api.ApiAssemblyMarker).Assembly
            .GetTypes()
            .Where(t => t.IsInterface);

        foreach (var i in interfaces)
        {
            i.GetMembers().Length
             .Should()
             .BeLessThanOrEqualTo(
                 MaxMembers,
                 $"{i.Name} has more than {MaxMembers} members (violates ISP)");
        }
    }
}
