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
                process.StartInfo.WorkingDirectory = playProjectFolder;
                process.StartInfo.FileName = "wsl";
                process.StartInfo.Arguments = $"./{scriptName}";
            } else {
                process.StartInfo.WorkingDirectory = playProjectFolder.Replace(@"C:\", @"/mnt/c/").Replace(@"\", @"/");
                process.StartInfo.FileName = scriptName;
            }

            process.Start();

            var output = process.StandardOutput.ReadToEnd();

            process.Kill();
            Console.WriteLine($"RAN hello.sh and it returned: '{output}'");
        }
    }
}
