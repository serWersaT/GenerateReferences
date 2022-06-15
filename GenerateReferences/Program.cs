using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateReferences.Services;

namespace GenerateReferences
{
    class Program
    {        
        static void Main(string[] args)
        {
            Service service = new Service();

            Console.WriteLine("Введите команду:");
            service.Commands(Console.ReadLine());
            Console.ReadLine();
        }

    }
}
