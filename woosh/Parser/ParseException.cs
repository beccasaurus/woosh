using System;

namespace Woosh {
    public class ParseException : Exception {
        public ParseException(string message, string file, string line, int lineNumber, int exitCode) : base(message) {
            this.File = file;
            this.Line = line;
            this.LineNumber = lineNumber;
            this.ExitCode = exitCode;
        }
        public string File { get; init; }
        public string Line { get; init; }
        public int LineNumber { get; init; }
        public int ExitCode { get; init; }
        public string StandardError { get { return $"ParseError: {Message}\n{File}:{LineNumber} {Line}\n"; }}
    }
}
