using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        public const int count = 10;
        static void Main(string[] args)
        {
            FB.Start();
            Console.WriteLine(new string('-', 60));
            
            SF.Start();
            Console.WriteLine(new string('-', 60));

            RAND.Start();
            Console.WriteLine(new string('-', 60));
            
            Console.ReadKey();
        }
    }

}
