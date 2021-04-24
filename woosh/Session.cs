using System;
using System.Text;
using System.Linq;

namespace Woosh {
    public class Session {
        public Session() { }
        public Response Evaluate(string expression) {
            var exitCode = 0;
            var stdout = new StringBuilder();
            var stderr = new StringBuilder();

            string currentCommand;

            foreach (var line in expression.Split("\n")) {
                stdout.AppendLine($"LINE: {line}");

                foreach (var part in line.Split()) {
                    stdout.AppendLine($"PART: {part}");
                }

                // foreach (var character in line.ToCharArray()) {
                //     stdout.AppendLine($"CHAR: {character}");
                // }
                // if (currentCommand == null) {
                //     ;
                // } else {
                //     ;
                // }
            }

            return new (stdout.ToString(), stderr.ToString(), exitCode);
        }
    }
}
