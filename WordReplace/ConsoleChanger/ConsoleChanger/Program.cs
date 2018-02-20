using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            FOUNDER f = new FOUNDER(new List<TEXT>() { new TEXT("file1.txt"), new TEXT("smackmybitch.jpg")}, new List<string>() { "lol", "qw"});
            f.FoundAll();
            Console.ReadKey();
        }
    }
}
