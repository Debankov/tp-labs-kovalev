using Taylor.Core;

namespace Taylor.Tests;

internal sealed class TestResultStorage : IResultStorage
{
    public string? LastResult { get; private set; }

    public void Save(string result)
    {
        LastResult = result;
    }
}

public class UnitTest1
{
    
    private readonly TestResultStorage _storage;
    private readonly TaylorTriginometry _calculator;

    public UnitTest1()
    {
        _storage = new TestResultStorage();
        _calculator = new TaylorTriginometry(_storage);
    }

    [Fact]
    public void CalculateSin_Zero_ReturnsZero()
    {
        // Act
        _calculator.CalculateSin(0);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(0.0, result, 12);
    }

    [Fact]
    public void CalculateSin_PiOverTwo_ReturnsOne()
    {
        // Act
        _calculator.CalculateSin(Math.PI / 2);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(1.0, result, 12);
    }

    [Fact]
    public void CalculateCos_Zero_ReturnsOne()
    {
        // Act
        _calculator.CalculateCos(0);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(1.0, result, 12);
    }

    [Fact]
    public void CalculateCos_PiOverTwo_ReturnsZero()
    {
        // Act
        _calculator.CalculateCos(Math.PI / 2);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(0.0, result, 12);
    }

    [Fact]
    public void CalculateTan_PiOverFour_ReturnsOne()
    {
        // Act
        _calculator.CalculateTan(Math.PI / 4);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(1.0, result, 12);
    }

    [Fact]
    public void CalculateCot_PiOverFour_ReturnsOne()
    {
        // Act
        _calculator.CalculateCot(Math.PI / 4);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(1.0, result, 12);
    }

    [Fact]
    public void CalculateTan_PiOverTwo_ThrowsException()
    {
        // Act and Assert
        Assert.Throws<ArgumentException>(
            () => _calculator.CalculateTan(Math.PI / 2));
    }

    [Fact]
    public void CalculateCot_Zero_ThrowsException()
    {
        // Act and Assert
        Assert.Throws<ArgumentException>(
            () => _calculator.CalculateCot(0));
    }

    [Fact]
    public void DegreesToRadians_180Degrees_ReturnsPi()
    {
        // Act
        double result = TaylorTriginometry.DegreesToRadians(180);

        // Assert
        Assert.Equal(Math.PI, result, 12);
    }

    [Fact]
    public void DegreesToRadians_90Degrees_ReturnsPiOverTwo()
    {
        // Act
        double result = TaylorTriginometry.DegreesToRadians(90);

        // Assert
        Assert.Equal(Math.PI / 2, result, 12);
    }

    [Fact]
    public void CalculateSin_AngleGreaterThanTwoPi_IsNormalized()
    {
        // sin(2π + π/6) должен быть равен sin(π/6), то есть 0.5
        double angle = 2 * Math.PI + Math.PI / 6;

        // Act
        _calculator.CalculateSin(angle);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(0.5, result, 12);
    }

    [Fact]
    public void CalculateCos_NegativeAngle_ReturnsCorrectResult()
    {
        // cos(-π/3) = 0.5
        double angle = -Math.PI / 3;

        // Act
        _calculator.CalculateCos(angle);

        // Assert
        double result = ExtractResult(_storage.LastResult);

        Assert.Equal(0.5, result, 12);
    }

    private static double ExtractResult(string? message)
    {
        Assert.NotNull(message);

        // Сообщение имеет вид:
        // "sin(1.570796326794897) = 1.000000000000000"
        int separatorIndex = message.LastIndexOf('=');

        Assert.True(
            separatorIndex >= 0,
            "Результат не содержит символ '='.");

        string valueText = message[(separatorIndex + 1)..].Trim();

        return double.Parse(
            valueText,
            System.Globalization.CultureInfo.InvariantCulture);
    }
}
