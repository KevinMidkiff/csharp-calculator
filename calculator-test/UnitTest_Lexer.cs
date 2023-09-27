namespace calculator_test;

public class LexerTests {
    [Test]
    public void BasicInit() {
        Lexer lex = new("2 + 5");
        Assert.Pass();
    }

    [Test]
    public void BasicTokenize() {
        Lexer lex = new("2 + 5");
        Token[] expectedTokens = {
            new(TokenType.INTEGER, "2"),
            new(TokenType.PLUS, "+"),
            new(TokenType.INTEGER, "5"),
            new(TokenType.EOF, "<EOF>")
        };
        int expectedPos = 0;

        while (true) {
            Token nextToken = lex.GetNextToken();
            Token expected = expectedTokens[expectedPos];
            if (nextToken.GetTokenType() != expected.GetTokenType()) {
                Assert.Fail("Got token: '{0}', Expected: '{1}'", nextToken.GetValue(), expected.GetValue());
            } else if (nextToken.GetValue() != expected.GetValue()) {
                Assert.Fail("Token values do not match: {0} != {1}", nextToken.GetValue(), expected.GetValue());
            }
            if (nextToken.IsEOF()) {
                break;
            } else {
                expectedPos++;
            }
        }

        Assert.Pass();
    }

    [Test]
    public void ComplexTokenize() {
        Lexer lex = new("2 + (5.123 / 22) - (55 * 10.0) + 12");
        Token[] expectedTokens = {
            new(TokenType.INTEGER, "2"),
            new(TokenType.PLUS, "+"),
            new(TokenType.LPAREN, "("),
            new(TokenType.DOUBLE, "5.123"),
            new(TokenType.DIV, "/"),
            new(TokenType.INTEGER, "22"),
            new(TokenType.RPAREN, ")"),
            new(TokenType.MINUS, "-"),
            new(TokenType.LPAREN, "("),
            new(TokenType.INTEGER, "55"),
            new(TokenType.MUL, "*"),
            new(TokenType.DOUBLE, "10.0"),
            new(TokenType.RPAREN, ")"),
            new(TokenType.PLUS, "+"),
            new(TokenType.INTEGER, "12"),
            new(TokenType.EOF, "<EOF>")
        };
        int expectedPos = 0;

        while (true) {
            Token nextToken = lex.GetNextToken();
            Token expected = expectedTokens[expectedPos];
            if (nextToken.GetTokenType() != expected.GetTokenType()) {
                Assert.Fail("Got token: '{0}', Expected: '{1}'", nextToken.GetValue(), expected.GetValue());
            } else if (nextToken.GetValue() != expected.GetValue()) {
                Assert.Fail("Token values do not match: {0} != {1}", nextToken.GetValue(), expected.GetValue());
            }
            if (nextToken.IsEOF()) {
                break;
            } else {
                expectedPos++;
            }
        }

        Assert.Pass();
    }

    [Test]
    public void InvalidFloat() {
        Lexer lex = new("2 + .55");
        Token[] expectedTokens = {
            new(TokenType.INTEGER, "2"),
            new(TokenType.PLUS, "+"),
            // NOTE: This is technically not a valid token, but the lexer should detect that
            new(TokenType.DOUBLE, ".55"),
            new(TokenType.EOF, "<EOF>")
        };
        int expectedPos = 0;

        try {
            while (true) {
                Token nextToken = lex.GetNextToken();
                Token expected = expectedTokens[expectedPos];
                if (nextToken.GetTokenType() != expected.GetTokenType()) {
                    Assert.Fail("Got token: '{0}', Expected: '{1}'", nextToken.GetValue(), expected.GetValue());
                } else if (nextToken.GetValue() != expected.GetValue()) {
                    Assert.Fail("Token values do not match: {0} != {1}", nextToken.GetValue(), expected.GetValue());
                }
                if (nextToken.IsEOF()) {
                    break;
                } else {
                    expectedPos++;
                }
            }

            // This should have caught the error
            Assert.Fail();
        } catch (SyntaxError) {
            Assert.Pass();
        }
    }

    [Test]
    public void NegativeNumber() {
        Lexer lex = new("2 - -55.4 * -2");
        Token[] expectedTokens = {
            new(TokenType.INTEGER, "2"),
            new(TokenType.MINUS, "-"),
            new(TokenType.DOUBLE, "-55.4"),
            new(TokenType.MUL, "*"),
            new(TokenType.INTEGER, "-2"),
            new(TokenType.EOF, "<EOF>")
        };
        int expectedPos = 0;

        while (true) {
            Token nextToken = lex.GetNextToken();
            Token expected = expectedTokens[expectedPos];
            if (nextToken.GetTokenType() != expected.GetTokenType()) {
                Assert.Fail("Got token: '{0}', Expected: '{1}'", nextToken.GetValue(), expected.GetValue());
            } else if (nextToken.GetValue() != expected.GetValue()) {
                Assert.Fail("Token values do not match: {0} != {1}", nextToken.GetValue(), expected.GetValue());
            }
            if (nextToken.IsEOF()) {
                break;
            } else {
                expectedPos++;
            }
        }

        Assert.Pass();
    }
}