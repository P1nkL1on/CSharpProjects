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
            TypeConvertion tpcv = new TypeConvertion("IIBDDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "<=" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
    class Less : BinaryOperation
    {
        public Less(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIBDDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "<" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
    class MrEq : BinaryOperation
    {
        public MrEq(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIBDDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + ">=" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
    class More : BinaryOperation
    {
        public More(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIBDDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + ">" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }
}
