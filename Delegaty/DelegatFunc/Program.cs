using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegatFunc
{
    //public delegate TResult Func<in T, out TResult>(T arg);

    class Program
    {
        static int Sum(int x, int y)
        {
            return x + y;
        }

        static void Main(string[] args)
        {
            Func<int, int, int> add = Sum;
            int result = add(10, 10);
            Console.WriteLine(result);

            Func<int> getRandomNumber = delegate ()
            {
                Random rnd = new Random();
                return rnd.Next(1, 100);
            };

            Console.WriteLine(getRandomNumber());
            Thread.Sleep(100);

            Func<int> getRandomNumber2 = () => new Random().Next(1, 100);

            Console.WriteLine(getRandomNumber2()); 

            Func<int, int, int> Sum2 = (x, y) => x + y;

            Console.WriteLine(Sum2(12,18));

            Console.ReadKey();
        }
    }
}
