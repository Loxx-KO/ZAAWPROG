using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatJakoParametrMetody
{
    internal class Program
    {
        public delegate void MyDelegate(string msg);

        class Delegate_as_a_parameter
        {
            static void Main(string[] args)
            {
                MyDelegate del = ClassA.MethodA;
                InvokeDelegate(del);

                del = ClassB.MethodB;
                InvokeDelegate(del);

                del = (string msg) => Console.WriteLine("Called lambda expression: " + msg);
                InvokeDelegate(del);

                Console.ReadKey();
            }

            static void InvokeDelegate(MyDelegate del)
            {
                del("Hello World");
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
}
