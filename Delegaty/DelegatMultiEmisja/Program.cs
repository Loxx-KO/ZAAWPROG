using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatMultiEmisja
{
    public delegate void MyDelegate2(string msg);

    class Multicast_delegate
    {
        static void Main(string[] args)
        {
            MyDelegate2 del1 = ClassA.MethodA;
            MyDelegate2 del2 = ClassB.MethodB;

            MyDelegate2 del = del1 + del2; // combines del1 + del2
            del("Hello World");

            MyDelegate2 del3 = (string msg) => Console.WriteLine("Called lambda expression: " + msg);
            del += del3; // combines del1 + del2 + del3
            del("Hello World");

            del = del - del2; // removes del2
            del("Hello World");

            del -= del1; // removes del1
            del("Hello World");

            Console.ReadKey();
        }
    }

    class ClassA
    {
        internal static void MethodA(string message)
        {
            Console.WriteLine("Called ClassA.MethodA() with parameter: " + message);
        }
    }

    class ClassB
    {
        internal static void MethodB(string message)
        {
            Console.WriteLine("Called ClassB.MethodB() with parameter: " + message);
        }
    }
}
