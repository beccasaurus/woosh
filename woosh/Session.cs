using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Woosh {
    public class Session {
        public Session() { }
        public Response Evaluate(string expression) {
            var exitCode = 0;
            var stdout = new StringBuilder();
            var stderr = new StringBuilder();

            // ...

            return new (stdout.ToString(), stderr.ToString(), exitCode);
        }
    }
}
