using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predicate
{
    class Program
    {
        static bool IsUpperCase(string str)
        {
            return str.Equals(str.ToUpper());
        }

        static void Main(string[] args)
        {
            Predicate<string> isUpper = IsUpperCase;
            bool result = isUpper("hello world!!");
            Console.WriteLine(result);

            Predicate<string> isUpper2 = delegate (string s) { return s.Equals(s.ToUpper()); };
            result = isUpper2("HELLO WORLD!!");
            Console.WriteLine(result);

            Predicate<string> isUpper3 = s => s.Equals(s.ToUpper());
            result = isUpper3("hello world!!");
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
