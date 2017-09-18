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

            varType = ValueType.Variable;
            if (ss[0] == "double") varType = ValueType.Cdouble;
            if (ss[0] == "int") varType = ValueType.Cint;
            if (ss[0] == "string") varType = ValueType.Cstring;
            if (ss[0] == "char") varType = ValueType.Cchar;
            if (ss[0] == "bool") varType = ValueType.Cboolean;

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
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "DEFINE [" + defineType.ToString() + "]");
            a.Trace(depth + 1);
        }
    }

    class Mins : MonoOperation
    {
        public Mins(IOperation val)
        {
            a = val;
            returnType = val.returnTypes();
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "-");
            a.Trace(depth + 1);
        }
    }
    class Nega : MonoOperation
    {
        public Nega(IOperation val)
        {
            a = val;
            returnType = val.returnTypes();
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "!");
            a.Trace(depth + 1);
        }
    }

    class Incr : MonoOperation
    {
        public Incr(IOperation val)
        {
            a = val;
            returnType = ValueType.Cboolean;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "++");
            a.Trace(depth + 1);
        }
    }
    class Dscr : MonoOperation
    {
        public Dscr(IOperation val)
        {
            a = val;
            returnType = ValueType.Cboolean;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "--");
            a.Trace(depth + 1);
        }
    }
}
