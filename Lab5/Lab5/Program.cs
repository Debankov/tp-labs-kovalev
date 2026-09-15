using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Program
    {
          static void Main()
          {
              Console.Write("Введите количество членов ряда N: ");
              long n = long.Parse(Console.ReadLine());

              Console.Write("Введите количество потоков: ");
              int threadCount = int.Parse(Console.ReadLine());

              if (n <= 0)
              {
                  Console.WriteLine("N должно быть положительным.");
                  return;
              }

              if (threadCount <= 0)
              {
                  Console.WriteLine("Количество потоков должно быть положительным.");
                  return;
              }

              if (threadCount > n)
                  threadCount = (int)Math.Min(n, int.MaxValue);

              Console.WriteLine();
              Console.WriteLine($"Количество членов: {n:N0}");
              Console.WriteLine($"Количество потоков: {threadCount}");
              Console.WriteLine();

              // Последовательное вычисление
              Stopwatch stopwatch = Stopwatch.StartNew();

              double piSequential = CalculateSequential(n);

              stopwatch.Stop();
              long sequentialTime = stopwatch.ElapsedMilliseconds;

              // Параллельное вычисление
              stopwatch.Restart();

              double piParallel = CalculateParallel(n, threadCount);

              stopwatch.Stop();
              long parallelTime = stopwatch.ElapsedMilliseconds;

              double speedup;

              if (parallelTime == 0)
                  speedup = double.PositiveInfinity;
              else
                  speedup = (double)sequentialTime / parallelTime;

              double difference = Math.Abs(piSequential - piParallel);

              Console.WriteLine("Результаты:");
              Console.WriteLine($"Последовательный результат: {piSequential:R}");
              Console.WriteLine($"Параллельный результат: {piParallel:R}");
              Console.WriteLine($"Эталонное значение Math.PI:{Math.PI:R}");
              Console.WriteLine();

              Console.WriteLine($"Время последовательной версии: {sequentialTime} мс");
              Console.WriteLine($"Время параллельной версии: {parallelTime} мс");

              if (double.IsInfinity(speedup))
                  Console.WriteLine("Ускорение: слишком маленькое время измерения");
              else
                  Console.WriteLine($"Ускорение: {speedup:F2}");

              Console.WriteLine();
              Console.WriteLine($"Разница результатов: {difference:E}");

              const double tolerance = 1e-12;

              if (difference <= tolerance)
                  Console.WriteLine("Результаты совпадают с заданной точностью.");
              else
                  Console.WriteLine("Результаты отличаются больше заданной точности.");

              Console.WriteLine();
              Console.WriteLine("Ошибка последовательного результата: " + $"{Math.Abs(Math.PI - piSequential):E}");
              Console.WriteLine("Ошибка параллельного результата: " + $"{Math.Abs(Math.PI - piParallel):E}");
          }

         
          static double CalculateSequential(long n)
          {
              double sum = 0.0;

              for (long i = 0; i < n; i++)
              {
                  double term = 1.0 / (2.0 * i + 1.0);

                  if ((i & 1) == 0)
                      sum += term;
                  else
                      sum -= term;
              }

              return 4.0 * sum;
          }

        
          static double CalculateParallel(long n, int threadCount)
          {
              double[] partialSums = new double[threadCount];
              Task[] tasks = new Task[threadCount];

              long blockSize = n / threadCount;
              long remainder = n % threadCount;

              for (int threadId = 0; threadId < threadCount; threadId++)
              {
                  int localThreadId = threadId;

                  long start = localThreadId * blockSize +
                               Math.Min(localThreadId, remainder);

                  long count = blockSize +
                               (localThreadId < remainder ? 1 : 0);

                  long end = start + count;

                  tasks[localThreadId] = Task.Run(() =>
                  {
                      double localSum = 0.0;

                      for (long i = start; i < end; i++)
                      {
                          double term = 1.0 / (2.0 * i + 1.0);

                          if ((i & 1) == 0)
                              localSum += term;
                          else
                              localSum -= term;
                      }

                      partialSums[localThreadId] = localSum;
                  });
              }

              Task.WaitAll(tasks);

             
              double totalSum = 0.0;

              for (int i = 0; i < threadCount; i++)
                  totalSum += partialSums[i];

              return 4.0 * totalSum;
          }
        

    
    }
}
