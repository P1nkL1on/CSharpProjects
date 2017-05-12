using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DungeonsLibrary;

namespace DungeonTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit a = new Unit(new KnightLoadout());
            Console.WriteLine(a.StatsToString());

            while (true)
            {
                //writeout
                string enter = Console.ReadLine(); Console.Clear(); Console.WriteLine(a.StatsToString());

                if (enter.Length > 0 && enter[0] == '+')
                {
                    Console.WriteLine(String.Format("{0} increased by 1", Help.statsName[int.Parse(enter[1] + "")]));
                    a.AddStat(int.Parse(enter[1] + ""), 5);
                }
                else
                    Console.WriteLine(Help.AnswerQuestion(enter));
            }
        }
    }
}
