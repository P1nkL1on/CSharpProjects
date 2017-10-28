using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    // 

    class Summ : BinaryOperation
    {
        public Summ(IOperation left, IOperation right)
        {
            operationString = "+";
            TypeConvertion tpcv = new TypeConvertion("IIIDDDSSSAAAAIAIAA", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }
    class Diff : BinaryOperation
    {
        public Diff(IOperation left, IOperation right)
        {
            operationString = "-";
            TypeConvertion tpcv = new TypeConvertion("IIIDDDAAAAIAIAA", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }

    class Mult : BinaryOperation
    {
        public Mult(IOperation left, IOperation right)
        {
            operationString = "*";
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }

    class Qout : BinaryOperation
    {
        public Qout(IOperation left, IOperation right)
        {
            operationString = "/";
            TypeConvertion tpcv = new TypeConvertion("IIIDDD", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }
}
