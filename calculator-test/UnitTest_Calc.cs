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

    [Test]
    public void InvalidExprDivideByZero() {
        try {
            double result = Calc.Compute("5 / 0");
            Assert.Fail("Somehow a divide by zero was allowed?");
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }

    [Test]
    public void InvalidExprDoubleOverflow() {
        try {
            // Use integer value that would overflow a double 
            double result = Calc.Compute(
                "279769313486231570814527423731704356798070567525844996598917476803157260780028538760589558" +
                "632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245" +
                "490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168" +
                "738177180919299881250404026184124858368.0 + 1");
            Assert.Fail("Should have overflowed double memory space");
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }

    [Test]
    public void InvalidExprResultOverflowDoubles() {
        try {
            // Attempt too large of a computation that would result in a double's INF
            // make sure a PANIC happens.
            double result = Calc.Compute(
                String.Format("{0} * {0}", 
                "179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558" + 
                "632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245" + 
                "490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168" + 
                "738177180919299881250404026184124858368.0"));
            Assert.Fail("Somehow computed infinity?");
        } catch (Panic) {
            Assert.Pass();
        }
    }

    [Test]
    public void InvalidExprResultOverflowInts() {
        try {
            // Attempt too large of a computation that would result in a double's INF
            // make sure a PANIC happens.
            // Doubles are used for all values (even ints), so theoretically this should allow
            // the computation of integers that are the max value that a double allows.
            double result = Calc.Compute(
                String.Format("{0} * {0}", 
                "179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558" + 
                "632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245" + 
                "490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168" + 
                "738177180919299881250404026184124858368"));
            Assert.Fail("Somehow computed infinity?");
        } catch (Panic) {
            Assert.Pass();
        }
    }
}