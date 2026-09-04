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
        static void Main(string[] args)
        {
            Console.WriteLine("Вариант 1.");
            // Задание 1. Вычислить факториал числа n, введённого пользователем. Предусмотреть проверку ввода.
            try
            {
                Console.WriteLine("Введите число для возведения его в факториал (число не должно быть отрицательным или больше 19)");
                long user_num = int.Parse(Console.ReadLine());
                if (user_num < 0  || user_num > 19)
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

            // Задание 3. Вычислить значение функции согласно варианту (значение x задаёт пользователь).
            // Если при данном x функция не определена (отрицательное число под корнем или логарифмом, деление на ноль)
            // — вывести понятное сообщение об ошибке.

            try
            {
                Console.WriteLine("Введите число для подсчёта результата функции.");
                double user_num = double.Parse(Console.ReadLine());
                double result = A(user_num);
                Console.WriteLine("Результат функции: " + result);
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine("При вводите числа возникло исключение: " + e);
            }
        }
    }
}