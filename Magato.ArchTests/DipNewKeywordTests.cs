using System;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Xunit;

namespace Magato.ArchTests;

public sealed class DipNewKeywordTests
{
    [Fact]
    public void Application_Should_Not_Create_SqlConnection_With_New()
    {
        var offenders = typeof(Magato.Api.ApiAssemblyMarker).Assembly
            .GetTypes()
            .Where(t => t.Namespace?.Contains(".Services") == true)
            .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            .Where(m => m.ToString()!.Contains("new SqlConnection", StringComparison.Ordinal))
            .Select(m => $"{m.DeclaringType!.Name}.{m.Name}")
            .ToList();

        offenders.Should().BeEmpty(
            "high‑level code must rely on abstractions injected via DI and not instantiate SqlConnection directly (DIP)");
    }
}
