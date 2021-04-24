using System;

namespace Woosh {
    public struct Response {
        public Response(string stdout, string stderr, int exitCode) {
            this.stdout = stdout;
            this.stderr = stderr;
            this.exitCode = exitCode;
        }
        public string stdout { get; init; }
        public string stderr { get; init; }
        public int exitCode { get; init; }
    }
}
