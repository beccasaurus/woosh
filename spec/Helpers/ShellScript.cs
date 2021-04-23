using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>Respresents a shell script which can be executed, e.g. a whoosh script!</summary>
public class ShellScript {

    /// <summary>Respresents the output from a shell script including Standard Output, Standard Error, and Exit Code.</summary>
    public struct Result {
        public Result(string stdout, string stderr, int exitCode) {
            this.stdout = stdout;
            this.stderr = stderr;
            this.exitCode = exitCode;
        }
        public string stdout { get; init; }
        public string stderr { get; init; }
        public int exitCode { get; init; }
    }

    public static Result Run(string command, params string[] arguments) {
        return new ShellScript(command, arguments).Run();
    }

    public static string DefaultWorkingDirectory { get; set; }
    public static readonly Dictionary<string, string> DefaultEnvironmentVariables = new();
    public static string WindowsExecutable = "wsl";
    public static readonly List<string> DefaultAdditionalPaths = new();

    string path;
    public string Path {
        get { return path; }
        set {
            // if (! File.Exists(System.IO.Path.Combine(WorkingDirectory ?? DefaultWorkingDirectory, value))) throw new ArgumentException($"Invalid ShellScript path, file not found: {value}");
            this.path = value;
        }
    }
    public List<string> Arguments { get; init; }

    public string WorkingDirectory { get; set; }
    public List<string> AdditionalPaths { get; set; }
    public Dictionary<string, string> EnvironmentVariables { get; set; }

    public ShellScript() { this.Arguments = new(); this.EnvironmentVariables = new(); this.AdditionalPaths = new(); }
    public ShellScript(string path, params string[] arguments) : this() { this.Path = path; this.Arguments.AddRange(arguments); }

    public Result Run() {
        using var process = new Process {
            StartInfo = {
                FileName = Path,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = WorkingDirectory ?? DefaultWorkingDirectory
            }
        };

        foreach (var argument in Arguments) process.StartInfo.ArgumentList.Add(argument);
        foreach (var variable in DefaultEnvironmentVariables) process.StartInfo.EnvironmentVariables[variable.Key] = variable.Value;
        foreach (var variable in EnvironmentVariables) process.StartInfo.EnvironmentVariables[variable.Key] = variable.Value;
        foreach (var path in DefaultAdditionalPaths) process.StartInfo.EnvironmentVariables["PATH"] = $"{process.StartInfo.EnvironmentVariables["PATH"]}:{path}";
        foreach (var path in AdditionalPaths) process.StartInfo.EnvironmentVariables["PATH"] = $"{process.StartInfo.EnvironmentVariables["PATH"]}:{path}";

        if (RuntimeInformation.OSDescription.ToLower().Contains("windows") && ! string.IsNullOrWhiteSpace(WindowsExecutable)) {
            process.StartInfo.FileName = WindowsExecutable;
            process.StartInfo.ArgumentList.Insert(0, Path);
        } else {
            process.StartInfo.WorkingDirectory = process.StartInfo.WorkingDirectory.Replace(@"C:\", @"/mnt/c/").Replace(@"\", @"/");
        }

        process.Start();

        // Naive for now:
        process.WaitForExit();
        var exitCode = process.ExitCode;
        var stdout = process.StandardOutput.ReadToEnd();
        var stderr = process.StandardError.ReadToEnd();

        process.Kill();

        return new(stdout, stdout, exitCode);
    }
}
