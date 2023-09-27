/**
 * Syntax tree parser for calculator.
 */
namespace Calculator {
    /**
     * Expression parser to create the abstract syntax tree for the 
     * expression being tokenized by the provided lexer.
     */
    public class Parser {
        private Lexer lex;
        private Token currentToken;

        /**
         * Constructor.
         *
         * @param lex - Lexer to tokenize the expression
         */
        public Parser(Lexer lex) {
            this.lex = lex;
            this.currentToken = this.lex.GetNextToken();
        }

        /**
         * Eat and advance to the next token verifying that the current token is 
         * what was expected.
         *
         * @param tokenType - Expected token to eat.
         */
        private void Eat(TokenType tokenType) {
            if (this.currentToken.GetTokenType() == tokenType) {
                // Consume the token
                this.currentToken = this.lex.GetNextToken();
            } else {
                // The next token is not the expected token.
                throw new SyntaxError(
                    String.Format("Current token mismatch - (actual) {0} != (expected) {1}", 
                    this.currentToken.GetValue(), tokenType));
            }
        }

        /**
         * Process a 'factor' in the grammar of the calculator.
         * 
         * factor : INTEGER | DOUBLE | LPAREN expr RPAREN
         *
         * @return Top most node of the AST associated with the factor
         */
        private Ast.Node Factor() {
            Ast.Node node;
            switch(this.currentToken.GetTokenType()) {
                case TokenType.INTEGER: {
                    node = new Ast.IntNode(Int32.Parse(this.currentToken.GetValue()));
                    this.Eat(TokenType.INTEGER);
                    break;
                }
                case TokenType.DOUBLE: {
                    node = new Ast.DoubleNode(Double.Parse(this.currentToken.GetValue()));
                    this.Eat(TokenType.DOUBLE);
                    break;
                }
                case TokenType.LPAREN: {
                    this.Eat(TokenType.LPAREN);
                    node = this.Expr();
                    this.Eat(TokenType.RPAREN);
                    break; 
                }
                default:
                    throw new SyntaxError(
                        String.Format("Invalid 'factor' syntax - should not have reached '{0}'",
                        this.currentToken.GetValue()));
            }

            return node;
        }

        /**
         * Process a term in the grammar of the calculator.
         *
         * term : factor ((MUL | DIV) factor)*
         *
         * @return Top most node of the AST for the term
         */
        private Ast.Node Term() {
            Ast.Node node = this.Factor();

            while (this.currentToken.GetTokenType() == TokenType.MUL || this.currentToken.GetTokenType() == TokenType.DIV) {
                Token token = this.currentToken;
                if (token.GetTokenType() == TokenType.MUL) {
                    this.Eat(TokenType.MUL);
                } else if (token.GetTokenType() == TokenType.DIV) {
                    this.Eat(TokenType.DIV);
                }

                node = new Ast.OperationNode(node, token, this.Factor());
            }

            return node;
        } 

        /**
         * Process an 'expr' in the grammar of the calculator.
         *
         * expr : term ((PLUS | MINUX) term)
         *
         * @return Top most node of the AST for the expr.
         */
        private Ast.Node Expr() {
            Ast.Node node = this.Term(); 

            while (this.currentToken.GetTokenType() == TokenType.PLUS || this.currentToken.GetTokenType() == TokenType.MINUS) {
                Token token = this.currentToken;

                if (token.GetTokenType() == TokenType.PLUS) {
                    this.Eat(TokenType.PLUS);
                } else if (token.GetTokenType() == TokenType.MINUS) {
                    this.Eat(TokenType.MINUS);
                }

                node = new Ast.OperationNode(node, token, this.Term());
            }

            return node;
        }

        /**
         * Process the provided lexer tokens according to the following grammar:
         *
         * expr   : term ((PLUS | MINUX) term)*
         * term   : factor ((MUL | DIV) factor)*
         * factor : INTEGER | DOUBLE | LPAREN expr RPAREN
         *
         * @return Top most node of the AST for the calculator's expression.
         */
        public Ast.Node Parse() {
            Ast.Node node = this.Expr();
            // Verify that we reached the EOF of the expression, otherwise, there is a
            // syntax error.
            if (!this.currentToken.IsEOF()) {
                throw new SyntaxError("Premature end of equation, syntax error");
            }
            return node;
        }
    }
}