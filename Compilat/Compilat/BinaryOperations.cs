using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class Summ : BinaryOperation
    {
        public Summ(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIIDDDSSS", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "+" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }

    }
    class Diff : BinaryOperation
    {
        public Diff(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "-" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }

    }

    class Mult : BinaryOperation
    {
        public Mult(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "*" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }

    }

    class Qout : BinaryOperation
    {
        public Qout(IOperation left, IOperation right)
        {
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "/" + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }

    }
}
