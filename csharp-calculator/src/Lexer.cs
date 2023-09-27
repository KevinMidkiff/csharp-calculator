

using System.Reflection.PortableExecutable;
using System.Text;


/**
 * Lexer for artimatic calculations.
 */
namespace Calculator {
    public class Lexer {
        private string expr;
        private int pos;
        private char currentChar;
        private readonly int eofPos;

        /** 
         * Constructor that takes in the expression for the lexer to tokenize.
         */
        public Lexer(string expression) {
            this.expr = expression;
            this.eofPos = this.expr.Length;
            this.pos = 0;
            this.currentChar = this.expr[this.pos];
        }

        /**
         * Return whether or the Lexer has reached the EOF already.
         * 
         * @return boolean, true if at the EOF, false if not 
         */ 
        private bool EOF() {
            return this.pos == this.eofPos;
        }

        /**
         * Advance to the next chatacter in the expression.
         */
        private void Advance() {
            // Already reached the EOF of the expression during an earlier call to 
            // this method.
            if (this.EOF()) {
                // TODO(kmidkiff): Throw an exception?
                return;
            }

            // Attempt to advance the position to the next character
            this.pos += 1;

            // If the current position is not at the end of the string, then
            // set the current char to the next character in the string.
            if (this.pos != this.eofPos) {
                this.currentChar = this.expr[this.pos];
            }

            // If this is reached 
        }

        /**
         * Peek to next character without advancing.
         */
        private char Peek() {
            int peekPos = this.pos + 1;
            if (peekPos == this.eofPos) {
                return ' ';
            }
            return this.expr[peekPos];
        }

        /**
         * Get the next token in the expression.
         */
        public Token GetNextToken() {
            // State variables for constructing floating point / integer value
            StringBuilder sb = new();
            TokenType tempTokenType = TokenType.EOF;

            // While we are not at the EOF of the expression, attempt to grab the next 
            // token from the expression.
            while (!this.EOF()) {
                switch (this.currentChar) {
                    case ' ': {
                        this.Advance();
                        continue;
                    }
                    case '+': {
                        this.Advance();
                        return new Token(TokenType.PLUS, "+");
                    }
                    case '-': {
                        this.Advance();
                        return new Token(TokenType.MINUS, "-");
                    }
                    case '*': {
                        this.Advance();
                        return new Token(TokenType.MUL, "*");
                    }
                    case '/': {
                        this.Advance();
                        return new Token(TokenType.DIV, "/");
                    }
                    case '(': {
                        this.Advance();
                        return new Token(TokenType.LPAREN, "(");
                    } 
                    case ')': {
                        this.Advance();
                        return new Token(TokenType.RPAREN, ")");
                    }
                    default: {
                        if (this.currentChar == '.') {
                            // Check invalid floating point syntax
                            if (sb.Length == 0) {
                                throw new SyntaxError("Invalid float format - must have preceding integer");
                            }

                            sb.Append(this.currentChar);
                            tempTokenType = TokenType.FLOAT;
                        } else if (Char.IsDigit(this.currentChar)) {
                            sb.Append(this.currentChar);
                            if (tempTokenType == TokenType.EOF) {
                                tempTokenType = TokenType.INTEGER;
                            }
                        } else {
                            // Unknown character, invalid syntax
                            throw new SyntaxError(String.Format("Unknown character: {0}", this.currentChar));
                        }

                        // Peek logic to see if we've reached the end of an integer or floating point number token
                        char peek = this.Peek();

                        // Advance to the next character
                        this.Advance();

                        if (!Char.IsDigit(peek) && peek != '.') {
                            // We've reached the end of a float or integer token, return the token
                            return new Token(tempTokenType, sb.ToString());
                        } else {
                            continue;
                        }
                    }
                }
            }

            return new Token(TokenType.EOF, "<EOF>");
        }
    }
}