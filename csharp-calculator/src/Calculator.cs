/**
 * Calculator implementation.
 */
namespace Calculator {
    /**
     * Calculator Class.
     */
    public class Calc {
        /**
         * Compute the provided mathematical expression. An expression must be like the 
         * following:
         *     5 + 2 * 9
         *
         * This method will parse and provide the result for valid expressions as a double.
         *
         * @param expression - Mathematical expression to compute.
         * @return Computed value as a double.
         * @throws SyntaxError - If the expression is invalid
         */
        public static double Compute(string expression) {
            // Load the expression into the lexer for tokenization
            Lexer lex = new(expression);
            // Load the lexer into the parser to create the AST
            Parser parser = new(lex);
            // Generate the AST for the expression
            Ast.Node node = parser.Parse();
            // Process and return the expression's result
            return node.Visit();
        } 
    } 
}