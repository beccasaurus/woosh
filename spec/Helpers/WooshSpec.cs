using System;
using System.IO;
using System.Linq;
using Xunit.Abstractions;

public class WooshSpec {
    readonly ITestOutputHelper log;

    public readonly string ProjectFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", ".."));
    public readonly string ScriptsFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Scripts"));
    public readonly string SolutionFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", ".."));
    public readonly string ExecutableWooshFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "bin"));

    public WooshSpec() {
        ShellScript.DefaultWorkingDirectory = ScriptsFolder;
        ShellScript.DefaultAdditionalPaths.Add(ExecutableWooshFolder);
    }

    public WooshSpec(ITestOutputHelper log) : this () {
        this.log = log;
    }

    public void Log(string message, params object[] objects) {
        if (objects.Any()) log.WriteLine(message, objects);
        else log.WriteLine(message);
    }

    public ShellScript.Result RunWooshScript(string script, params string[] arguments) {
        if (File.Exists(Path.Combine(ScriptsFolder, script)))
            return ShellScript.Run(Path.Combine(ScriptsFolder, script), arguments);
        else
            return ShellScript.Run(script, arguments);
    }
}