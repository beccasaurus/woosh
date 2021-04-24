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

    [Fact]
    public void CanRunSimpleWooshCommands() {
        var session = new Woosh.Session();
        var result = session.Evaluate(@"println ""Hello, woosh!""");
        Log(result.stdout);
        Assert.Equal(0, result.exitCode);
        Assert.Empty(result.stderr);
        Assert.Equal("Hello, woosh!\n", result.stdout);
    }
}
