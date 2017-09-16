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
            a = left; b = right;
            //if (a.GetType().FullName.IndexOf("Value") < 0)
            //    throw new Exception("Only values can be in part left of assuming!");
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "=");
            
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
    }

    class Equal : BinaryOperation
    {
        public Equal(IOperation left, IOperation right)
        {
            a = left; b = right;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "==");
            a.Trace(depth + 1);
            b.Trace(depth + 1);
        }
        /*
        public override IOperation ParseFrom(string s)
        {
            string left = "", right = "";
            MISC.separate(s, "==", ref left, ref right);
            return new Summ(ParseFrom(left), ParseFrom(right));
        }
        
        public override ASTValue calculateOperation()
        {
            ASTValue resultA = a.calculateOperation(),
                     resultB = b.calculateOperation();
            if (resultA.getValueType != resultB.getValueType)
                throw new Exception("Can not compare different types");

            switch (resultA.getValueType)
            {
                case ValueType.Cint:
                    return new ASTValue(ValueType.Cboolean, (int)resultA.getValue == (int)resultB.getValue);
                case ValueType.Cdouble:
                    return new ASTValue(ValueType.Cboolean, (double)resultA.getValue == (double)resultB.getValue);
                case ValueType.Cchar:
                    return new ASTValue(ValueType.Cboolean, (char)resultA.getValue == (char)resultB.getValue);
                case ValueType.Cstring:
                    return new ASTValue(ValueType.Cboolean, (string)resultA.getValue == (string)resultB.getValue);
                case ValueType.Cboolean:
                    return new ASTValue(ValueType.Cboolean, (bool)resultA.getValue == (bool)resultB.getValue);
                default:
                    break;
            }
            return new ASTValue(ValueType.Cboolean, (object)false);
        }*/
    }
}
