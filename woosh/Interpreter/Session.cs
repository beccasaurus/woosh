using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Woosh {

    public class EvaluationException : Exception {
        public EvaluationException(string message) : base(message) {}
    }

    public class Session {
        public bool ExitOnError { get; set; }

        public Session() { }
        public Response Evaluate(string code, bool verbose = false) {
            var exitCode = 0;
            var stdout = new StringBuilder();
            var stderr = new StringBuilder();
            List<Statement> statements;
            
            try { statements = Parser.GetStatements(code); }
            catch (ParseException e) {
                return new("", e.StandardError, e.ExitCode);
            } catch (Exception e) {
                if (verbose) return new("", $"Unexpected Exception: {e.Message}", 1);
                else         return new("", $"Unexpected Exception", 1);
            }

            foreach (var statement in statements) {
                try { EvaluateStatement(statement, stdout, stderr, out exitCode); }
                catch (Exception e) {
                    if (verbose) return new("", $"Unexpected Exception: {e.Message}", 1);
                    else         return new("", $"Unexpected Exception", 1);
                }
                if (ExitOnError && exitCode != 0) return new (stdout.ToString(), stderr.ToString(), exitCode);
            }

            return new (stdout.ToString(), stderr.ToString(), exitCode);
        }

        void EvaluateStatement(Statement statement, StringBuilder stdout, StringBuilder stderr, out int exitCode) {
            if (statement.RightHandSide != null) throw new EvaluationException("Don't know how to do right-hand sides yet!");

            // Check to see if it's a builtin or what :)
            // Note a statement could be commandless or I guess, nah, implicit. Think "x=5"
            // Right now, just testing println so do that!
            switch (statement.Command.Name) {
                case "println":
                    stdout.AppendLine(string.Join(" ", statement.Command.Arguments ?? new List<string>()));
                    exitCode = 0;
                    break;
                    ;;
                default:
                    throw new EvaluationException($"Don't know how to do this command yet! [{statement.Command.Name}]");
            }
            exitCode = 0;
        }
    }
}
