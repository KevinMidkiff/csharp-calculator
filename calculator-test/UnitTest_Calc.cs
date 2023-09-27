/**
 * Basic unit tests for the calculator.
 */
namespace calculator_test;

public class CalcTests {
    [Test]
    public void BasicExpr() {
        double result = Calc.Compute("2 + 5");
        Assert.That(result, Is.EqualTo(7.0));
    }

    [Test]
    public void ComplexExpr() {
        double result = Calc.Compute("2 + (5.123 / 22) - (55 * -10.0) + 12");
        Assert.That(result, Is.EqualTo(564.2328636363636));
    }

    [Test]
    public void InvalidExprUnknownToken() {
        try {
            double result = Calc.Compute("2 + asd");
            Assert.Fail("Should not have been able to parse the expression");
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }

    [Test]
    public void InvalidExprIncomplete() {
        try {
            double result = Calc.Compute("2 - ");
            Assert.Fail("This should not have succeeded");
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }

    [Test]
    public void InvalidExprMissingRParen() {
        try {
            // The below expression should be invalid because it is missing the closing ")"
            double result = Calc.Compute("2 * (3 + 5");
            Assert.Fail("Missed closing rparen error");
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }

    [Test]
    public void InvalidExprMissingLParen() {
        try {
            // The below expression should be invalid because it is missing the opening "("
            double result = Calc.Compute("2 * 3 + 5)");
            Assert.Fail("Missed missing lparen error in expression");
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }
}