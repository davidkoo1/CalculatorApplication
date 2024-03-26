using Contracts;


[TestClass]
public class CalculatorTests
{
    private Calculator Calculator;

    [TestInitialize]
    public void TestSetup()
    {
        Calculator = new Calculator();
    }

    //Simple Exemple
    [TestMethod]
    public async Task TestAddition()
    {
        //Arrange(Подготовка к тесту)

        string testExpression = "3+4";

        //Act(Выполняем тест)

        double result = await Calculator.Evaluate(testExpression);

        //Assert(проверяем результат)

        Assert.AreEqual(7, result);
        //Assert.AreEqual(4, await Calculator.Evaluate("2+2"));
    }


    [TestMethod]
    public async Task TestMoreAddition()
    {
        //Arrange(Подготовка к тесту)

        string[] testExpressions = { "4+3", "100+2" };
        double[] corectResults = { 7, 102 };

        //Act(Выполняем тест)

        double[] results = new double[corectResults.Length];
        for (int i = 0; i < testExpressions.Length; i++)
        {
            results[i] = await Calculator.Evaluate(testExpressions[i]);
        }

        //Assert(проверяем результат)

        for (int i = 0; i < corectResults.Length; i++)
        {
            Assert.AreEqual(corectResults[i], results[i]);
        }
    }


    [TestMethod]
    public async Task TestSimpleMinus()
    {
        //Arrange
        string testExpression = "6-3";
        //Act
        double result = await Calculator.Evaluate(testExpression);
        //Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public async Task TestMoreMinus()
    {
        //Arrange
        string testExpression = "-6-3";
        //Act
        double result = await Calculator.Evaluate(testExpression);
        //Assert
        Assert.AreEqual(-9, result);
    }

    [TestMethod]
    public async Task TestSimpleMultiplication()
    {
        //Arrange
        string testExpression = "6*3*2";
        //Act
        double result = await Calculator.Evaluate(testExpression);
        //Assert
        Assert.AreEqual(36, result);
    }

    [TestMethod]
    public async Task TestSimpleDivision()
    {
        //Arrange
        string testExpression = "6/3";
        //Act
        double result = await Calculator.Evaluate(testExpression);
        //Assert
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public async Task TestWithDoubleResult_Division()
    {
        //Arrange
        string testExpression = "5/2";
        //Act
        double result = await Calculator.Evaluate(testExpression);
        //Assert
        Assert.AreEqual(2.5, result);
    }

    [TestMethod]
    public async Task TestMoreCombination()
    {
        //Arrange(Подготовка к тесту)

        string[] testExpressions = { "4+3", "100-2", "-10+5", "-18-2", "2+2*2", "6/1+2", "2*7/14", "-10-10", "6*2/4", "0-3", "-5+8", "2.5+2.5" };
        double[] corectResults = { 7, 98, -5, -20, 6, 8, 1, -20, 3, -3, 3, 5 };

        //Act(Выполняем тест)

        double[] results = new double[corectResults.Length];
        for (int i = 0; i < testExpressions.Length; i++)
        {
            results[i] = await Calculator.Evaluate(testExpressions[i]);
        }

        //Assert(проверяем результат)

        for (int i = 0; i < corectResults.Length; i++)
        {
            Assert.AreEqual(corectResults[i], results[i]);
        }
    }


    [TestMethod]
    public async Task TestSimpleParentheses()
    {
        //Arrange
        string testExpression = "2*(6-3)-1";
        //Act
        double result = await Calculator.Evaluate(testExpression);
        //Assert
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public async Task TestCombinationParentheses()
    {
        //Arrange(Подготовка к тесту)

        string[] testExpressions = {
            "10+(2*3)",
            "(1+2)*3",
            "100/(2+3)",
            "2-(-3)",
            "-2-(-5)",
            "2+(-(-(-5)))",
            "(2+2)*2",
            "2+(3*(4/2))",
            "(-3+5)-4",
            "(5*(3+2))-10",
            "-(-(-(-(-5))))",
            "4-(-3)-2+(-5)"
        };
        double[] corectResults = { 16, 9, 20, 5, 3, -3, 8, 8, -2, 15, -5, 0 };

        //Act(Выполняем тест)

        double[] results = new double[corectResults.Length];
        for (int i = 0; i < testExpressions.Length; i++)
        {
            results[i] = await Calculator.Evaluate(testExpressions[i]);
        }

        //Assert(проверяем результат)

        for (int i = 0; i < corectResults.Length; i++)
        {
            Assert.AreEqual(corectResults[i], results[i]);
        }
    }


    //Ошибки(неправильно скобки, оператор, деление на 0)
    [TestMethod]
    public async Task TestDivideByZero()
    {
        await Assert.ThrowsExceptionAsync<DivideByZeroException>(() => Calculator.Evaluate("4/0"));
    }

    [TestMethod]
    public async Task TestInvalidCharacters()
    {
        await Assert.ThrowsExceptionAsync<ArgumentException>(() => Calculator.Evaluate("2+3a"));
    }

    [TestMethod]
    public async Task TestUnmatchedParentheses()
    {
        await Assert.ThrowsExceptionAsync<Exception>(() => Calculator.Evaluate("(3+2"));
    }

    [TestMethod]
    public async Task TestWitnoutOperand()
    {
        await Assert.ThrowsExceptionAsync<ArgumentException>(() => Calculator.Evaluate("2(3+2)"));
    }

    [TestMethod]
    public async Task TestEmptyExpression()
    {
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Calculator.Evaluate(""));
    }

}
