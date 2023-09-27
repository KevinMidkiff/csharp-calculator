/**
 * Main entrypoint into the program
 */
namespace Calculator {
    public class Program {
        private static void Usage() {
            string name = System.AppDomain.CurrentDomain.FriendlyName;
            Console.WriteLine($"usage: {name} [-h | --help] <expr>");
            Console.WriteLine("\t-h | --help - Show this help");
            Console.WriteLine("\texpr        - Simple mathematical expression to calculate");
            Console.WriteLine("");
            Console.WriteLine("Examples:");
            Console.WriteLine($"$ ./{name} \"1 * (3 + 4) / 55\"");
            Console.WriteLine($"$ ./{name} \"55 + 2\"");
            Console.WriteLine($"$ ./{name} \"3 / 6\"");
        }
        public static int Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("calc> Must provide an expression to solve (-h | --help to see usage)");
                return -1;
            }

            if (args[0] == "-h" || args[0] == "--help") {
                Usage();
                return 0;
            }

            try {
                string expr = String.Join(" ", args);
                
                Console.WriteLine($"calc> Attempting to solve expression '{expr}'");
                double result = Calc.Compute(expr);
                Console.WriteLine($"calc> Result: {result}");
            } catch (SyntaxError e) {
                Console.WriteLine($"calc> {e.Message}");
            } catch (Panic e) {
                Console.WriteLine($"calc> Unable to compute expression: {e.Message}");
            } catch (Exception e) {
                Console.WriteLine($"calc> Unknown Issue: {e.Message}");
            }

            return 0;
        }
    }
}