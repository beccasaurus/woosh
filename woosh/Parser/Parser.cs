/*
 * 🛑 STOP 🛑
 *
 * Dear Reader, this code parses code by hand with if conditionals, switch statements, and string parsing.
 *
 * I wrote the code this way because I used this project to refreshen my C# skills and I wanted to write lots of C#.
 *
 * This should probably use an existing parsing system such as ANTLR if you were to do it for realsies.
 *
 * Just thought I'd let you know 💖
 *
 */

using System;
using System.Text;
using System.Collections.Generic;

namespace Woosh {

    public static class Parser {

        public static List<Statement> GetStatements(string code) {
            var statements = new List<Statement>();
            string command = null;
            List<string> commandArguments = new();
            
            StringBuilder currentlyParsingText = new();
            char? currentQuoteBlock = null;
            char? previousChar = null;

            foreach (var line in code.Split("\n")) {
                foreach (var character in line.ToCharArray()) {
                    switch (character) {
                        case ' ':
                            if (currentQuoteBlock == null && previousChar != '\\') {
                                if (command == null) {
                                    command = currentlyParsingText.ToString();
                                    currentlyParsingText.Clear();
                                    break;
                                } else if (currentlyParsingText.Length > 0) {
                                    commandArguments.Add(currentlyParsingText.ToString());
                                    currentlyParsingText.Clear();
                                    break;
                                }
                            }
                            goto default;
                        case '"':
                            if (previousChar != '\\') {
                                switch (currentQuoteBlock) {
                                    case '"':
                                        currentQuoteBlock=null;
                                        commandArguments.Add(currentlyParsingText.ToString());
                                        currentlyParsingText.Clear();
                                        break;
                                    case null:
                                        currentQuoteBlock='"';
                                        break;
                                }
                            } else {
                                goto default;
                            }
                            break;
                        case '\'':
                            if (previousChar != '\\') {
                                switch (currentQuoteBlock) {
                                    case '\'':
                                        currentQuoteBlock=null;
                                        commandArguments.Add(currentlyParsingText.ToString());
                                        currentlyParsingText.Clear();
                                        break;
                                    case null:
                                        currentQuoteBlock='\'';
                                        break;
                                }
                            } else {
                                goto default;
                            }
                            break;
                        default:
                            currentlyParsingText.Append(character);
                            break;
                    }
                    previousChar = character;
                }
                if (currentlyParsingText.Length > 0 && ! string.IsNullOrWhiteSpace(currentlyParsingText.ToString())) {
                    if (command == null) {
                        command = currentlyParsingText.ToString();
                        currentlyParsingText.Clear();
                    } else {
                        commandArguments.Add(currentlyParsingText.ToString());
                    }
                }
            }

            Console.WriteLine($"PARSED COMMAND {command} with arguments [{string.Join(",", commandArguments)}]");
            statements.Add(new() { Command = new() { Name = command, Arguments = commandArguments }});

            return statements;
        }
    }
}
