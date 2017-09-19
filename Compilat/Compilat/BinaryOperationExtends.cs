using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{

    class Assum : BinaryOperation
    {
        public Assum(IOperation left, IOperation right)
        {
            operationString = "=";
            TypeConvertion tpcv = new TypeConvertion("II_DD_BB_CC_SS_$$_", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
            //a = left; b = right;
            //if (a.GetType().FullName.IndexOf("Value") < 0)
            //    throw new Exception("Only values can be in part left of assuming!");
        }
    }

    class Equal : BinaryOperation
    {
        public Equal(IOperation left, IOperation right)
        {
            operationString = "==";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBBBBCCBSSB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
}
