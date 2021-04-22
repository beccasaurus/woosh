using System;
using System.Linq;
using System.Diagnostics;

namespace Woosh.Playground
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine($"Hello World! {string.Join(", ", args)}");
      var processStartInfo = new ProcessStartInfo
      {
        UseShellExecute = true,
        RedirectStandardOutput = true,
        WorkingDirectory = "/home/bex/Code/beccasaurus/woosh/play",
        FileName = "hello.sh"
      };

      var process = new Process()
    }
  }
}
