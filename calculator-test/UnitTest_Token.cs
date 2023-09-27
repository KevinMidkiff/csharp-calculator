namespace calculator_test;

public class TokenTests {
    [Test]
    public void BasicTokenTest() {
        Token token = new(Calculator.TokenType.MINUS, "-");
        Assert.Pass();
    }
}