using Lab4.Core;
using static Lab4.Core.Algo;

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

    // Тесты через Theory 
    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void A_ReturnsFiniteValue(double x)
    {
        double result = Algo.A(x);

        Assert.False(double.IsNaN(result));
        Assert.False(double.IsInfinity(result));
    }


    [Theory]
    [InlineData(12)]
    [InlineData(1000)]
    [InlineData(20000)]
    [InlineData(123890)]
    public void BusPassLoad_Returns_ArgumentOutOfRangeException(int pass)
    {
        Algo.Autopark.Bus bus = new Algo.Autopark.Bus("testBus", 14.0, 120000, 10);

        Assert.Throws<ArgumentOutOfRangeException>(() => bus.PassagiereLoad(pass));

    }

    [Theory]
    [InlineData(-100, 0)]
    [InlineData(100, 0)]
    [InlineData(120, -10)]
    [InlineData(-100, -100)]

    public void BusRoadCost_Returns_ArgumetnOutOfRangeException(int distance, int fuelcost)
    {
        Algo.Autopark.Bus bus = new Algo.Autopark.Bus("testBus", 14.0, 120000, 10);
        Assert.Throws<ArgumentOutOfRangeException>(() => bus.RoadTripCost(distance, fuelcost));

    }



    // Граничные значения
    [Theory]
    [InlineData(-1)]
    [InlineData(16)]

    public void VehicleConsctuctor_FuelBorderArgument_ArgumetnOutOfRangeException(double fuel)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new Algo.Autopark.Vehicle("testVehicle", fuel, 0);
        });

    }

    [Theory]
    [InlineData(-1)]
    [InlineData(21)]


    public void Factorial_BorderArgument_Returns_ArgumetnOutOfRangeException(int n)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Algo.Factorial(n));
    }

    [Fact]
    public void Fibonacci_BorderArgument_Returns_ArgumetnOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Algo.Fibonacci(-1));

    }

    // Тесты через Throw (хотя у меня и так почти все через Throw)

    [Fact]
    public void Vehicle_WithFuelConsumptionGreaterThan15_ThrowsException()
    {
        // fuelComsumption = 16 — недопустимое значение
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new Autopark.Vehicle(
                "Test vehicle",
                16.0,
                100.0);
        });
    }

    [Fact]
    public void Truck_WithNegativeMaxCargoCapacity_ThrowsException()
    {
        // maxCargoCapacity = -1 — недопустимое значение
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new Autopark.Truck(
                "Test truck",
                10.0,
                100.0,
                -1.0);
        });
    }

    [Fact]
    public void LightCar_WithZeroPassengerSeats_ThrowsException()
    {
        // passengerSeats = 0 — количество мест должно быть от 1 до 9
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new Autopark.LightCar(
                "Test light car",
                8.0,
                100.0,
                0,
                300.0);
        });
    }

    [Fact]
    public void Bus_AfterCreation_HasZeroPassengers()
    {
        // Arrange
        var bus = new Autopark.Bus(
            "Mercedes",
            10.0,
            100.0,
            50);

        // Assert
        Assert.Equal(0, bus.PassagiereCapacity);
        Assert.Equal(50, bus.MaxPassagiereCapacity);
    }

    [Fact]
    public void PassagiereLoad_IncreasesPassengersCount()
    {
        // Arrange
        var bus = new Autopark.Bus(
            "Mercedes",
            10.0,
            100.0,
            50);

        // Act
        bus.PassagiereLoad(20);

        // Assert
        Assert.Equal(20, bus.PassagiereCapacity);
    }

    [Fact]
    public void PassagiereUnload_DecreasesPassengersCount()
    {
        // Arrange
        var bus = new Autopark.Bus(
            "Mercedes",
            10.0,
            100.0,
            50);

        bus.PassagiereLoad(30);

        // Act
        bus.PassagiereUnload(10);

        // Assert
        Assert.Equal(20, bus.PassagiereCapacity);
    }

    [Fact]
    public void PassagiereLoad_MoreThanCapacity_ThrowsException()
    {
        // Arrange
        var bus = new Autopark.Bus(
            "Mercedes",
            10.0,
            100.0,
            50);

        // Act и Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            bus.PassagiereLoad(51);
        });

        // Проверяем, что состояние объекта не изменилось
        Assert.Equal(0, bus.PassagiereCapacity);
    }

    [Fact]
    public void PassagiereLoad_NegativeValue_ThrowsException()
    {
        // Arrange
        var bus = new Autopark.Bus(
            "Mercedes",
            10.0,
            100.0,
            50);

        // Act и Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            bus.PassagiereLoad(-5);
        });

        // Инвариант: количество пассажиров не стало отрицательным
        Assert.Equal(0, bus.PassagiereCapacity);
    }

}
