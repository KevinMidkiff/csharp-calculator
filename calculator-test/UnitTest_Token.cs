namespace calculator_test;

public class TokenTests {
    [Test]
    public void Test2() {
        Token token = new(Calculator.TokenType.MINUS, "-");
        Assert.Pass();
    }
}