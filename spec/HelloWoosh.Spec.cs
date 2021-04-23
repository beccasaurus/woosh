using Xunit;
using Xunit.Abstractions;
using System.IO;

public class HelloWoosh : WooshSpec {
    public HelloWoosh(ITestOutputHelper log) : base (log) {}

    [Fact]
    public void TestWoosh() {
        var result = RunWooshScript("hello.wsh", "arg1", "arg2");

        Assert.Contains("Hello, woosh!", result.stdout);
    }
}
