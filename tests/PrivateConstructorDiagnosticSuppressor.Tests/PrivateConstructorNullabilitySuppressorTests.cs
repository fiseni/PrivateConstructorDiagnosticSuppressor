using Microsoft.CodeAnalysis;

namespace PrivateConstructorDiagnosticSuppressor.Tests;

public class PrivateConstructorNullabilitySuppressorTests
{
    [Fact]
    public async Task NotSuppress_GivenPublicConstructor()
    {
        var code = """
            public class Foo
            {
                private string _value;
                public Foo()
                {
                }
            }
            """;

        Verify.NoWarning(code, NullableContextOptions.Disable);
        await Verify.NotSuppressed(code, NullableContextOptions.Enable);
    }

    [Fact]
    public async Task NotSuppress_GivenInternalConstructor()
    {
        var code = """
            public class Foo
            {
                private string _value;
                internal Foo()
                {
                }
            }
            """;

        Verify.NoWarning(code, NullableContextOptions.Disable);
        await Verify.NotSuppressed(code, NullableContextOptions.Enable);
    }

    [Fact]
    public async Task NotSuppress_GivenProtectedConstructor()
    {
        var code = """
            public class Foo
            {
                private string _value;
                protected Foo()
                {
                }
            }
            """;

        Verify.NoWarning(code, NullableContextOptions.Disable);
        await Verify.NotSuppressed(code, NullableContextOptions.Enable);
    }

    [Fact]
    public async Task NotSuppress_GivenProtectedInternalConstructor()
    {
        var code = """
            public class Foo
            {
                private string _value;
                protected internal Foo()
                {
                }
            }
            """;

        Verify.NoWarning(code, NullableContextOptions.Disable);
        await Verify.NotSuppressed(code, NullableContextOptions.Enable);
    }

    [Fact]
    public async Task Suppress_GivenPrivateConstructor()
    {
        var code = """
            public class Foo
            {
                private string _value;
                private Foo()
                {
                }
            }
            """;

        Verify.NoWarning(code, NullableContextOptions.Disable);
        await Verify.Suppressed(code, NullableContextOptions.Enable);
    }

    [Fact]
    public async Task Suppress_GivenPrivateProtectedConstructor()
    {
        var code = """
            public class Foo
            {
                private string _value;
                private protected Foo()
                {
                }
            }
            """;

        Verify.NoWarning(code, NullableContextOptions.Disable);
        await Verify.Suppressed(code, NullableContextOptions.Enable);
    }
}
