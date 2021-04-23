using Xunit;
using Xunit.Abstractions;
using System.IO;

public class HelloWooshScript : WooshSpec {
    public HelloWooshScript(ITestOutputHelper log) : base (log) {}

    [Fact]
    public void CanCallWooshScript() {
        var result = RunWooshScript("hello.wsh", "arg1", "arg2");
        Assert.Contains("Hello, woosh!", result.stdout);
    }
}
