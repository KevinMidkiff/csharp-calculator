/**
 * Abstract syntax tree for calculator.
 */
namespace Calculator {
    namespace Ast {
        /**
         * Basic node interface for the AST.
         */
        public interface Node {
            /**
             * Visit the node returning a double for the computed value
             * of the syntax tree.
             */
            double Visit();
        }

        /**
         * AST node containing a double value.
         */
        public class DoubleNode : Node {
            private readonly double value;

            public DoubleNode(double value) => this.value = value;

            public double Visit() {
                return this.value;
            }
        }

        /**
         * AST node containing an integer value.
         */
        public class IntNode : Node {
            private readonly int value;

            public IntNode(int value) => this.value = value;

            public double Visit() {
                return this.value;
            }
        }

        /**
         * AST node representing an mathematical operation.
         */
        public class OperationNode : Node {
            private Node left;
            private Token token;
            private Node right;

            public OperationNode(Node left, Token token, Node right) {
                this.left = left;
                this.right = right;
                this.token = token;
            }

            public double Visit() {
                switch (token.GetTokenType()) {
                    case TokenType.PLUS:  return left.Visit() + right.Visit();
                    case TokenType.MINUS: return left.Visit() - right.Visit();
                    case TokenType.MUL:   return left.Visit() * right.Visit();
                    case TokenType.DIV: {
                        double leftValue = left.Visit();
                        double rightValue = right.Visit();
                        // Check if dividing by zero
                        if (rightValue == 0.0) {
                            throw new SyntaxError("Attempted divide by zero");
                        }
                        return leftValue / rightValue; 
                    }
                    default:
                        throw new SyntaxError(String.Format("Not an operation: {0}", token.GetValue()));
                }
            }
        }
    }
}