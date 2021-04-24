using System;

namespace Woosh {

    public class EvaluationException : Exception {
        public EvaluationException(string message) : base(message) {}
    }
}