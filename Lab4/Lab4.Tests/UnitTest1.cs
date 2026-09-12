using Lab4.Core;

namespace Lab4.Tests;

public class UnitTest1
{

    /*
     * Примеры тестов из методичнки
     */
    [Fact]
    public void Factorial_OfZero_ReturnsOne()
    {
        // Arrange + Act
        long result = Algo.Factorial(0);
        // Assert
        Assert.Equal(1, result);
    }
    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 120)]
    [InlineData(10, 3628800)]
    public void Factorial_ReturnsExpected(int n, long expected)
        => Assert.Equal(expected, Algo.Factorial(n));

    [Theory]
    [InlineData(-1)]
    [InlineData(21)]
    public void Factorial_OutOfRange_Throws(int n)
        => Assert.Throws<ArgumentOutOfRangeException>(() => Algo.Factorial(n));

    [Fact]
    public void Fibonacci_First6_AreCorrect()
    {
        var seq = Algo.Fibonacci(6);
        Assert.Equal(new long[] { 0, 1, 1, 2, 3, 5 }, seq);
    }

    [Fact]
    public void Fibonacci_ZeroCount_ReturnsEmpty()
        => Assert.Empty(Algo.Fibonacci(0));

    [Theory]
    [InlineData(0)]
    [InlineData(0.5)]
    [InlineData(Math.PI / 2)]
    [InlineData(-1.2)]
    public void SinTaylor_MatchesMathSin(double x)
        => Assert.Equal(Math.Sin(x), Algo.SinTaylor(x), 1e-5);



    /*
     *  Мои примеры 
     */
    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void A_ReturnsFiniteValue(double x)
    {
        // Act
        double result = Algo.A(x);

        // Assert
        Assert.False(double.IsNaN(result));
        Assert.False(double.IsInfinity(result));
    }


}
