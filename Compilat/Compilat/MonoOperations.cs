using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class Mins : BinaryOperation
    {
        public Mins(IOperation val)
        {
            a = val;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "-");
            a.Trace(depth+1);
        }
    }
    class Nega : BinaryOperation
    {
        public Nega(IOperation val)
        {
            a = val;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "!");
            a.Trace(depth+1);
        }
    }

    class Incr : BinaryOperation
    {
        public Incr(IOperation val)
        {
            a = val;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "++");
            a.Trace(depth + 1);
        }
    }
    class Dscr : BinaryOperation
    {
        public Dscr(IOperation val)
        {
            a = val;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "--");
            a.Trace(depth + 1);
        }
    }
}
