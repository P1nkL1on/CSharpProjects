using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class Define : MonoOperation
    {
        ValueType defineType;
        public Define(string s)
        {
            string[] ss = s.Split('$');
            string varName;
            ValueType varType;
            if (ss[1].Length > 0)
                varName = ss[1];
            else
                throw new Exception("Can not define variable with name \""+ss[1]+"\"");

            ss[0].ToLower();
            varType = detectType(ss[0]);

            TypeConvertion tpcv = new TypeConvertion("IIDDBBCCSS$$", 1);
            returnType = MISC.CheckTypeCorrect(tpcv, varType);

            defineType = varType;
            //for (int i =0; i < varName.Length; i++){
            ASTValue newVariable = new ASTValue(varName, varType);
            
            ASTTree.variables.Add(newVariable);
            ASTValue newToken = new ASTValue(varName, varType);
            newToken.index = ASTTree.tokens.Count;
            ASTTree.tokens.Add(newToken);
            a = newToken;
            
        }
        public static ValueType detectType(string s)
        {
            if (s == "double") return ValueType.Cdouble;
            if (s == "int") return ValueType.Cint;
            if (s == "string") return ValueType.Cstring;
            if (s == "char") return ValueType.Cchar;
            if (s == "bool") return  ValueType.Cboolean;
            if (s == "void") return ValueType.Cvoid;
            return ValueType.Cvariable;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "DEFINE [" + defineType.ToString() + "]");
            MISC.finish = true;
            a.Trace(depth + 1);
        }
    }

    class Mins : MonoOperation
    {
        public Mins(IOperation val)
        {
            operationString = "-";
            a = val;
            returnType = val.returnTypes();
        }
        
    }
    class Nega : MonoOperation
    {
        public Nega(IOperation val)
        {
            operationString = "!";
            a = val;
            returnType = val.returnTypes();
        }
        
    }

    class Incr : MonoOperation
    {
        public Incr(IOperation val)
        {
            operationString = "++";
            a = val;
            returnType = ValueType.Cboolean;
        }
    }
    class Dscr : MonoOperation
    {
        public Dscr(IOperation val)
        {
            operationString = "--";
            a = val;
            returnType = ValueType.Cboolean;
        }
    }
}
