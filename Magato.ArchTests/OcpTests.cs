using System;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Xunit;

namespace Magato.ArchTests;

public sealed class OcpTests
{
    [Fact]
    public void No_Enum_Switch_In_Api_Layer()
    {
        var offenders = typeof(Magato.Api.ApiAssemblyMarker).Assembly
            .GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                          BindingFlags.Instance | BindingFlags.Static))
            .Where(m => m.ToString()!.Contains("switch") &&
                        m.ToString()!.Contains("enum", StringComparison.OrdinalIgnoreCase))
            .Select(m => $"{m.DeclaringType!.Name}.{m.Name}")
            .ToList();

        offenders.Should().BeEmpty(
            "switch/enum logic hampers extension without modification (violates OCP)");
    }

    [Fact]
    public void No_Type_Switch_In_Application_Layer()
    {
        var offenders = typeof(Magato.Api.ApiAssemblyMarker).Assembly
            .GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                          BindingFlags.Instance | BindingFlags.Static))
            .Where(m => m.GetMethodBody() != null)
            .Where(m =>
                m.ToString()!.Contains("switch") ||
                m.ToString()!.Contains(" is ", StringComparison.Ordinal))
            .Select(m => $"{m.DeclaringType!.Name}.{m.Name}")
            .ToList();

        offenders.Should().BeEmpty(
            "polymorphism (Strategy/Policy) should replace switch/if chains (OCP)");
    }
}
