using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Woosh {

    public class Session {
        public bool ExitOnError { get; set; }

        public Session() { }
        
        public Response Evaluate(string code, bool verbose = false) {
            return Evaluator.Evaluate(this, code, verbose);
        }
    }
}
