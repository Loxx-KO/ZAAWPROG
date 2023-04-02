using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatAction
{
    class Program
    {
        static void ConsolePrint(int i)
        {
            Console.WriteLine(i);
        }

        static void Main(string[] args)
        {
            Action<int> printActionDel = ConsolePrint;
            printActionDel(10);

            Action<int> printActionDel2 = delegate (int i)
            {
                Console.WriteLine(i);
            };
            printActionDel2(20);

            Action<int> printActionDel3 = i => Console.WriteLine(i);

            printActionDel3(30);

            Console.ReadKey();
        }
    }
}
