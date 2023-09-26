namespace calculator_test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Calculator.Lexer lex = new("1 + 2");
        Assert.Pass();
    }
}