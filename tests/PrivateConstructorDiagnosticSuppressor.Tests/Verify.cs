using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Pozitron.Roslyn.Analyzers;

namespace PrivateConstructorDiagnosticSuppressor.Tests;

public static class Verify
{
    public static Task NotSuppressed(
        string testCode,
        NullableContextOptions nullableContextOptions)
        => AssertSuppression(false, testCode, nullableContextOptions);

    public static Task Suppressed(
        string testCode,
        NullableContextOptions nullableContextOptions)
        => AssertSuppression(true, testCode, nullableContextOptions);

    public static void NoWarning(
        string testCode,
        NullableContextOptions nullableContextOptions)
    {
        var suppressor = new PrivateConstructorNullabilitySuppressor();
        var suppressedDiagnosticIds = suppressor.SupportedSuppressions.Select(x => x.SuppressedDiagnosticId).ToList();

        var compilation = CreateCompilation(testCode, nullableContextOptions);
        var compilationErrors = compilation.GetDiagnostics();
        var compilationErrorIds = compilationErrors.Select(x => x.Id).ToList();

        compilationErrorIds.Should().NotContain(suppressedDiagnosticIds);

    }
    public static async Task AssertSuppression(
        bool verifySuppressed,
        string testCode,
        NullableContextOptions nullableContextOptions)
    {
        var suppressor = new PrivateConstructorNullabilitySuppressor();
        var suppressedDiagnosticIds = suppressor.SupportedSuppressions.Select(x => x.SuppressedDiagnosticId).ToList();

        var compilation = CreateCompilation(testCode, nullableContextOptions);
        var compilationErrors = compilation.GetDiagnostics();
        var compilationErrorIds = compilationErrors.Select(x => x.Id).ToList();

        compilationErrorIds.Should().Contain(suppressedDiagnosticIds);

        var suppressedCompilation = compilation.WithAnalyzers([suppressor]);
        var suppressedCompilationErrors = await suppressedCompilation.GetAllDiagnosticsAsync();
        var suppressedCompilationErrorIds = suppressedCompilationErrors.Select(x => x.Id).ToList();

        if (verifySuppressed)
        {
            suppressedCompilationErrorIds.Should().NotContain(suppressedDiagnosticIds);
        }
        else
        {
            suppressedCompilationErrorIds.Should().Contain(suppressedDiagnosticIds);
        }
    }

    private static Compilation CreateCompilation(
        string? code = null,
        NullableContextOptions nullableContextOptions = NullableContextOptions.Disable)
    {
        var syntaxTrees = code is null
            ? null
            : new[] { CSharpSyntaxTree.ParseText(code) };

        return CSharpCompilation.Create(
            Guid.NewGuid().ToString("N"),
            syntaxTrees,
            references: [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)],
            options: new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                reportSuppressedDiagnostics: true,
                nullableContextOptions: nullableContextOptions));
    }
}
