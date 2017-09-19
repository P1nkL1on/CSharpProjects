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
            operationString = "+";
            TypeConvertion tpcv = new TypeConvertion("IIIDDDSSS", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
    class Diff : BinaryOperation
    {
        public Diff(IOperation left, IOperation right)
        {
            operationString = "-";
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }

    class Mult : BinaryOperation
    {
        public Mult(IOperation left, IOperation right)
        {
            operationString = "*";
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }

    class Qout : BinaryOperation
    {
        public Qout(IOperation left, IOperation right)
        {
            operationString = "/";
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
}
