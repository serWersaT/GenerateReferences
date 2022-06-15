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
            OldService oldservice = new OldService();

            Console.WriteLine("Введите команду:");
            //var reference = service.Commands(Console.ReadLine());
            var reference = oldservice.Commands(Console.ReadLine());

            Console.WriteLine(reference);

            Console.ReadLine();
        }

    }
}
