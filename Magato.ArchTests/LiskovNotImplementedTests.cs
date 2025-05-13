using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Magato.ArchTests;

public sealed class LiskovNotImplementedTests
{
    [Fact]
    public void Concrete_Classes_Should_Not_Throw_NotImplementedException()
    {
        var offenders = typeof(Magato.Api.ApiAssemblyMarker).Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static))
            .Where(m => m.GetMethodBody() != null)
            .Where(m => m.ToString()!.Contains("NotImplementedException", StringComparison.Ordinal))
            .Select(m => $"{m.DeclaringType!.Name}.{m.Name}")
            .Distinct()
            .ToList();

        offenders.Should().BeEmpty(
            "concrete implementations must satisfy the base contract (LSP)");
    }
}
