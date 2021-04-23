using System;
using System.IO;
using System.Linq;

namespace Woosh {
    class Program {
        public static int Main(string[] args) {
            if (args.Any()) {
                var script = args.First();
                if (! File.Exists(script)) { Console.WriteLine($"Script not found: {script}"); return 1; }
                Console.WriteLine($"Found a script: {script}");
                Console.WriteLine(File.ReadAllText(script));
                return 0;
            } else {
                Console.WriteLine("woosh 1.0");
                return 1;
            }
        }
    }
}
