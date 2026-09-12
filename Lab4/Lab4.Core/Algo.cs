namespace Lab4.Core;

public class Algo
{
    public static int Factorial(long i)
    {
        if (i < 0 || i > 20) throw new ArgumentOutOfRangeException();
        int res = 1;
        for (int j = 1; j <= i; j++)
        {
            res *= j;
        }
        return res;
    }
    public static long[] Fibonacci(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException(nameof(n));

        long[] result = new long[n];

        if (n > 0)
            result[0] = 0;

        if (n > 1)
            result[1] = 1;

        for (int i = 2; i < n; i++)
        {
            result[i] = result[i - 1] + result[i - 2];
        }

        return result;
    }


    public static double A(double x)
    {
        double first_s = Math.Sqrt(Math.Log(4.0 / 3.0));
        double second_s = x + (9.0 / 7.0);
        double third_s = Math.Exp(Math.Sin(1.3 * x - 0.7));
        double result = first_s + second_s - third_s;
        return result;
    }

    public static double SinTaylor(double x)
    {
        double epsilon = 1e-6;
        double sum = 0.0;
        double term = x;
        int n = 0;


        // Суммируем члены, пока очередной член по модулю больше ε
        while (Math.Abs(term) > epsilon)
        {
            sum += term;

            // Переход к следующему члену:
            // следующий член = текущий * (-x²) / ((2n + 2)(2n + 3))
            term *= -x * x / ((2 * n + 2) * (2 * n + 3));

            n++;
        }

        return sum;
    }
}
