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
        static void Main(string[] args)
        {
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
                long user_num = int.Parse(Console.ReadLine());
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
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine("При вводите числа возникло исключение: " + e);
                }
            }
        }
    }
}