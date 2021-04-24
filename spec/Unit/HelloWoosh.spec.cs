using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

public class HelloWooshLibrary : WooshSpec {
    public HelloWooshLibrary(ITestOutputHelper log) : base (log) {}

    [Fact]
    public void CanExtractSimpleWooshCommandStatements() {
        var statements = Woosh.Parser.GetStatements(@"echo ""Hello, woosh!""");
        Assert.Single(statements);
        Assert.Equal("echo", statements.First().Command.Name);
        Assert.Equal("Hello, woosh!", statements.First().Command.Arguments.First());
    }

    [Theory]
    [InlineData("print Hello", 0, "Hello", "")]
    [InlineData("println Hello", 0, "Hello\n", "")]
    public void CanRunSimpleWooshCommands(string code, int expectedExitCode, string expectedStdout, string expectedStderr) {
        var session = new Woosh.Session();
        var actualResponse = session.Evaluate(code, verbose: true);
        Assert.Equal<object>(expectedStderr, actualResponse.stderr);
        Assert.Equal<object>(expectedStdout, actualResponse.stdout);
        Assert.Equal<object>(expectedExitCode, actualResponse.exitCode);
    }
}
