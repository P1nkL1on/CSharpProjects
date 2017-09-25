using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{

    class Assum : BinaryOperation
    {
        public string requiredUpdate;
        public Assum(IOperation left, IOperation right)
        {
            operationString = "=";
            TypeConvertion tpcv = new TypeConvertion("II_DD_BB_CC_SS_$$_AA_", 2);
            a = left; b = right;
            ValueType at = a.returnTypes(), bt = b.returnTypes();
            returnType = MISC.CheckTypeCorrect(tpcv, at, bt);
            requiredUpdate = "none";
        }
        public string GetAssumableName
        {
            get
            {
                if ((a as ASTvariable) != null) return (a as ASTvariable).name;
                if ((a as Define) != null) return (a as Define).varName;
                return "-";
            }
        }
        public List<IOperation> GetStructDefine()
        {
            if (b as StructureDefine == null)
                return new List<IOperation>();
            return (b as StructureDefine).values;
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
    class Uneq : BinaryOperation
    {
        public Uneq(IOperation left, IOperation right)
        {
            operationString = "!=";
            TypeConvertion tpcv = new TypeConvertion("IIBDDBBBBCCBSSB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }

    class AND : BinaryOperation
    {
        public AND(IOperation left, IOperation right)
        {
            operationString = "&";
            TypeConvertion tpcv = new TypeConvertion("BBB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }

    class OR : BinaryOperation
    {
        public OR(IOperation left, IOperation right)
        {
            operationString = "|";
            TypeConvertion tpcv = new TypeConvertion("BBB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }

    class ANDS : BinaryOperation
    {
        public ANDS(IOperation left, IOperation right)
        {
            operationString = "&&";
            TypeConvertion tpcv = new TypeConvertion("BBB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }

    class ORS : BinaryOperation
    {
        public ORS(IOperation left, IOperation right)
        {
            operationString = "||";
            TypeConvertion tpcv = new TypeConvertion("BBB", 2);
            a = left; b = right;
            returnType = MISC.CheckTypeCorrect(tpcv, a.returnTypes(), b.returnTypes());
        }
    }
}
