using System.Threading.Channels;
using System.Globalization;

namespace Taylor.Core;

public class TaylorTriginometry
{
    private readonly IResultStorage _resultStorage;

    public TaylorTriginometry(IResultStorage resultStorage)
    {
        _resultStorage = resultStorage;
    }

    private const double Epsilon = 1e-15;
    private const int MaxIterations = 100;

    public void CalculateTan(double angle)
    {
        double result = TaylorTan(angle);

        string message = string.Format(
            CultureInfo.InvariantCulture,
            "tan({0}) = {1:F15}",
            angle,
            result);

        _resultStorage.Save(message);
    }

    public void CalculateCot(double angle)
    {
        double result = TaylorCot(angle);

        string message = string.Format(
            CultureInfo.InvariantCulture,
            "cot({0}) = {1:F15}",
            angle,
            result);

        _resultStorage.Save(message);
    }

    public void CalculateSin(double angle)
    {
        double result = TaylorSin(angle);

        string message = string.Format(
            CultureInfo.InvariantCulture,
            "sin({0}) = {1:F15}",
            angle,
            result);

        _resultStorage.Save(message);
    }

    public void CalculateCos(double angle)
    {
        double result = TaylorCos(angle);

        string message = string.Format(
            CultureInfo.InvariantCulture,
            "cos({0}) = {1:F15}",
            angle,
            result);

        _resultStorage.Save(message);
    }


    private static double TaylorSin(double x)
    {
        x = NormalizeAngle(x);

        double term = x;
        double sum = term;

        for (int n = 1; n < MaxIterations; n++)
        {
            
            term *= -x * x / ((2 * n) * (2 * n + 1));
            sum += term;

            if (Math.Abs(term) < Epsilon)
                break;
        }

        return sum;
    }

    private static double TaylorCos(double x)
    {
        x = NormalizeAngle(x);

        double term = 1.0;
        double sum = term;

        for (int n = 1; n < MaxIterations; n++)
        {
           
            term *= -x * x / ((2 * n - 1) * (2 * n));
            sum += term;

            if (Math.Abs(term) < Epsilon)
                break;
        }

        return sum;
    }

    private static double TaylorTan(double x)
    {
        double sin = TaylorSin(x);
        double cos = TaylorCos(x);

        if (Math.Abs(cos) < 1e-12)
            throw new ArgumentException("Тангенс не определён для этого угла.");

        return sin / cos;
    }

    private static double TaylorCot(double x)
    {
        double sin = TaylorSin(x);
        double cos = TaylorCos(x);

        if (Math.Abs(sin) < 1e-12)
            throw new ArgumentException("Котангенс не определён для этого угла.");

        return cos / sin;
    }


    private static double NormalizeAngle(double x)
    {
        x %= 2 * Math.PI;

        if (x > Math.PI)
            x -= 2 * Math.PI;

        if (x < -Math.PI)
            x += 2 * Math.PI;

        return x;
    }

    public static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
