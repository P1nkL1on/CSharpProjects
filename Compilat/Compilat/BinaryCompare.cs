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
            operationString = "<=";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
    class Less : BinaryOperation
    {
        public Less(IOperation left, IOperation right)
        {
            operationString = "<";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
    class MrEq : BinaryOperation
    {
        public MrEq(IOperation left, IOperation right)
        {
            operationString = ">=";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
    class More : BinaryOperation
    {
        public More(IOperation left, IOperation right)
        {
            operationString = ">";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
}
