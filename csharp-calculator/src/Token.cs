

using System.Security.Cryptography.X509Certificates;


/**
 * Basic token representation for parsing the arithmatic expressions.
 */
namespace Calculator {
    /**
     * Definition of types of tokens in an expression.
     */
    public enum TokenType {
        NUMBER,
        PLUS,
        MINUS,
        MUL,
        DIV,
        LPAREN,
        RPAREN,
        EOF
    }

    public class Token {
        private readonly TokenType tokenType;
        private readonly string value;

        /**
         * Constructor.
         */
        public Token(TokenType tokenType, string value) {
            this.tokenType = tokenType;
            this.value = value;
        } 

        /**
         * Retrieve the token's value represented as a string.
         */
        public string GetValue() {
            return this.value;
        }

        /**
         * Retrieve the token type.
         */
        public TokenType GetTokenType() {
            return this.tokenType;
        }

        /**
         * Simple helper for check if EOF.
         */
        public bool IsEOF() {
            return this.tokenType == TokenType.EOF;
        }
    }
 }