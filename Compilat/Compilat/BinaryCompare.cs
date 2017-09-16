using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class LsEq : BinaryOperation
    {
        public LsEq(IOperation left, IOperation right)
        {
            a = left; b = right;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "<=");
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
    class Less : BinaryOperation
    {
        public Less(IOperation left, IOperation right)
        {
            a = left; b = right;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "<");
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
    class MrEq : BinaryOperation
    {
        public MrEq(IOperation left, IOperation right)
        {
            a = left; b = right;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + ">=");
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
    class More : BinaryOperation
    {
        public More(IOperation left, IOperation right)
        {
            a = left; b = right;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + ">");
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
}
