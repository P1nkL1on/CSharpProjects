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
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }
    class Less : BinaryOperation
    {
        public Less(IOperation left, IOperation right)
        {
            operationString = "<";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }
    class MrEq : BinaryOperation
    {
        public MrEq(IOperation left, IOperation right)
        {
            operationString = ">=";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }
    class More : BinaryOperation
    {
        public More(IOperation left, IOperation right)
        {
            operationString = ">";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBDIBIDBCCB", 2);
            IOperation[] children = new IOperation[2] { left, right };
            returnType = MISC.CheckTypeCorrect(this, tpcv, ref children);
            a = children[0]; b = children[1];
        }
    }
}
