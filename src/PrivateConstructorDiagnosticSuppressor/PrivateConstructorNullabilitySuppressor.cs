using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace Pozitron.Roslyn.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PrivateConstructorNullabilitySuppressor : DiagnosticSuppressor
{
    public static SuppressionDescriptor SuppressionDescriptor { get; } = new(
        id: "POZ0001",
        suppressedDiagnosticId: "CS8618",
        justification: "The state is set externally by 3rd party libraries (EF Core, mappers, etc).");

    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions { get; } = ImmutableArray.Create(SuppressionDescriptor);

    public override void ReportSuppressions(SuppressionAnalysisContext context)
    {
        foreach (var diagnostic in context.ReportedDiagnostics)
        {
            if (SuppressionDescriptor.SuppressedDiagnosticId.Equals(diagnostic.Id))
            {
                var node = diagnostic.Location.SourceTree?
                    .GetRoot(context.CancellationToken)
                    .FindNode(diagnostic.Location.SourceSpan);

                if (node is ConstructorDeclarationSyntax ctorDeclarationSyntax
                    && ctorDeclarationSyntax.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PrivateKeyword)))
                {
                    context.ReportSuppression(Suppression.Create(SuppressionDescriptor, diagnostic));
                }
            }
        }
    }
}
