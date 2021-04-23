using Xunit;
using Xunit.Abstractions;
using System.IO;

public class HelloWooshLibrary : WooshSpec {
    public HelloWooshLibrary(ITestOutputHelper log) : base (log) {}

    [Fact]
    public void CanRunSimpleWooshCommands() {
        var session = new Woosh.Session();
        var result = session.Evaluate(@"echo ""Hello, woosh!""");
        Assert.Contains("Hello, woosh!", result.stdout);
        Assert.Empty(result.stderr);
        Assert.Equal(0, result.code)
    }
}
