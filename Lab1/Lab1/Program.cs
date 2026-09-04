using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program
    {

        public static int Factorial(long i)
        {
            int res = 1;
            for (int j = 1; j <= i; j++)
            {
                res *= j;
            }
            return res;
        }

        public static double A(double x)
        {
            double first_s = Math.Sqrt(Math.Log(4.0 / 3.0));
            double second_s = x + (9.0 / 7.0);
            double third_s = Math.Exp(Math.Sin(1.3 * x - 0.7));
            double result = first_s + second_s - third_s;
            return result;
        }

        static double SinTaylor(double x, double epsilon, out int termsCount)
        {
            double sum = 0.0;
            double term = x;
            int n = 0;

            termsCount = 0;

            // Суммируем члены, пока очередной член по модулю больше ε
            while (Math.Abs(term) > epsilon)
            {
                sum += term;
                termsCount++;

                // Переход к следующему члену:
                // следующий член = текущий * (-x²) / ((2n + 2)(2n + 3))
                term *= -x * x / ((2 * n + 2) * (2 * n + 3));

                n++;
            }

            return sum;
        }
        
        public static void FirstTask()
        {
            // Задание 1. Вычислить факториал числа n, введённого пользователем. Предусмотреть проверку ввода.
            try
            {
                Console.WriteLine("Введите число для возведения его в факториал (число не должно быть отрицательным или больше 19)");
                long user_num = int.Parse(Console.ReadLine());
                if (user_num < 0 || user_num > 19)
                {
                    throw new Exception("Заданное пользователем число не подходит под условие.");
                }
                int result = Factorial(user_num);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine("При вводите числа возникло исключение: " + e);
                }
            }
        }

        public static void SecondTask()
        {
            // Задание 2. Вычислить последовательность чисел Фибоначчи от 0 до n и вывести её в одну строку через запятую.
            try
            {
                Console.WriteLine("Введите, сколько чисел из последовательности фиббоначи вывести? (число не должно быть отрицательным или больше 70)");
                long user_num = long.Parse(Console.ReadLine());
                if (user_num < 0 || user_num > 70)
                {
                    throw new Exception("Заданное пользователем число не подходит под условие.");
                }
                long first = 0;
                long second = 1;
                for (int i = 0; i < user_num; i++)
                {

                    Console.Write(first + ", ");
                    long next = first + second;
                    first = second;
                    second = next;
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine("При вводите числа возникло исключение: " + e);
                }
            }
        }

        public static void ThirdTask()
        {
            // Задание 3. Вычислить значение функции согласно варианту (значение x задаёт пользователь).
            // Если при данном x функция не определена (отрицательное число под корнем или логарифмом, деление на ноль)
            // — вывести понятное сообщение об ошибке.

            try
            {
                Console.WriteLine("Введите число для подсчёта результата функции.");
                double user_num = double.Parse(Console.ReadLine());
                double result = A(user_num);
                Console.WriteLine("Результат функции: " + result);
            }
            catch (Exception e)
            {
                Console.WriteLine("При вводите числа возникло исключение: " + e);
            }
        }

        public static void FourthTask()
        {
            //Задание 4. Вычислить сумму ряда Тейлора согласно варианту с точностью ε = 10⁻⁶
            //(суммировать члены ряда, пока очередной член по модулю больше ε).
            //Сравнить результат с библиотечной функцией Math, вывести оба значения и количество просуммированных членов.
            try
            {
                Console.Write("Введите x: ");
                double x = double.Parse(Console.ReadLine());

                const double epsilon = 1e-6;

                double taylorResult = SinTaylor(x, epsilon, out int termsCount);
                double mathResult = Math.Sin(x);

                Console.WriteLine($"Сумма ряда Тейлора: {taylorResult:F10}");
                Console.WriteLine($"Math.Sin(x):         {mathResult:F10}");
                Console.WriteLine($"Количество членов:   {termsCount}");
                Console.WriteLine($"Погрешность:         {Math.Abs(taylorResult - mathResult):E}");
            }
            catch (Exception e)
            {
                Console.WriteLine("При вводите числа возникло исключение: " + e);
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Вариант 1.");
            FirstTask();
            SecondTask();
            ThirdTask();
            FourthTask();

        }
    }
}