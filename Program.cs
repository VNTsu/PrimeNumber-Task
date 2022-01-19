using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rookie3
{
    class Process
    {
        static async Task Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Task<List<int>> t1 = GetPrimeNumbers(2, 1250000);
            Task<List<int>> t2 = GetPrimeNumbers(1250001, 2500000);
            Task<List<int>> t3 = GetPrimeNumbers(2500001, 5000000);
            Task<List<int>> t4 = GetPrimeNumbers(5000001, 6500000);
            Task<List<int>> t5 = GetPrimeNumbers(6500001, 8500000);
            Task<List<int>> t6 = GetPrimeNumbers(8500001, 10000000);
            var results = await Task.WhenAll (t1,t2,t3,t4,t5,t6);
            Console.WriteLine("Total prime numbers: {0}\nProcess time: {1}", results.Sum(p => p.Count), sw.ElapsedMilliseconds);

            sw.Stop();
        }
        private static async Task<List<int>> GetPrimeNumbers(int minimum, int maximum)
        {
            List<int> result = new List<int>();

            return await Task.Factory.StartNew(() =>
            {
                for (int i = minimum; i <= maximum; i++)
                {
                    if (IsPrimeNumber(i))
                    {
                        result.Add(i);
                    }
                }
                return result;
            });
        }

        static bool IsPrimeNumber(int number)
        {
            if (number % 2 == 0)
            {
                return number == 2;
            }
            else
            {
                var topLimit = (int)Math.Sqrt(number);

                for (int i = 3; i <= topLimit; i += 2)
                {
                    if (number % i == 0) return false;
                }
                return true;
            }
        }
    }
}

