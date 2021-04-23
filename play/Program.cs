using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Woosh.Playground {
    class Program {
        static void Main(string[] args) {
            var scriptName = "hello.sh";
            var playProjectFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\.."));

            using var process = new Process();

            process.StartInfo.RedirectStandardOutput = true;

            if (RuntimeInformation.OSDescription.ToLower().Contains("windows")) {
                var solutionFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\.."));
                process.StartInfo.EnvironmentVariables["PATH"] = process.StartInfo.EnvironmentVariables["PATH"] + ":" + solutionFolder.Replace(@"C:\", @"/mnt/c/").Replace(@"\", @"/") + @"/bin";
                process.StartInfo.WorkingDirectory = playProjectFolder;
                process.StartInfo.FileName = "wsl";
                process.StartInfo.Arguments = $"./{scriptName} hello some arguments";
            } else {
                var solutionFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
                process.StartInfo.EnvironmentVariables["PATH"] = process.StartInfo.EnvironmentVariables["PATH"] + ":" + solutionFolder.Replace(@"C:\", @"/mnt/c/").Replace(@"\", @"/") + @"/bin";
                process.StartInfo.WorkingDirectory = playProjectFolder.Replace(@"C:\", @"/mnt/c/").Replace(@"\", @"/");
                //process.StartInfo.FileName = scriptName;
                process.StartInfo.FileName = scriptName;
                process.StartInfo.Arguments = "some arguments 1 2 3";
            }

            process.Start();

            var output = process.StandardOutput.ReadToEnd();

            process.Kill();
            Console.WriteLine($"RAN hello.sh and it returned: '{output}'");
        }
    }
}
