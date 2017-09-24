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
            bool isPointer = false;
            if (varName.IndexOf('*') == 0) { isPointer = true; varName = varName.Substring(1); returnType = ValueType.Cint; }
            

            //ASTValue newVariable = new ASTValue(varName, varType);
            //newVariable.isPointer = isPointer;

            //ASTTree.variables.Add(newVariable);
            //MISC.pushVariable(ASTTree.variables.Count - 1);

            //ASTValue newToken = new ASTValue(varName, varType);
            //newToken.isPointer = isPointer;
            //newToken.index = ASTTree.tokens.Count;
            //ASTTree.tokens.Add(newToken);
            //a = newToken;
            if (!isPointer)
            {
                ASTvariable newVar = new ASTvariable(varType, varName);
                ASTTree.variables.Add(newVar);
                MISC.pushVariable(ASTTree.variables.Count - 1);

                ASTTree.tokens.Add(newVar);
                a = newVar;
            }
            
            
            
        }
        public static ValueType detectType(string s)
        {
            if (s == "double") return ValueType.Cdouble;
            if (s == "int") return ValueType.Cint;
            if (s == "string") return ValueType.Cstring;
            if (s == "char") return ValueType.Cchar;
            if (s == "bool") return  ValueType.Cboolean;
            if (s == "void") return ValueType.Cvoid;
            return ValueType.Unknown;
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
    class Adrs : MonoOperation
    {
        public Adrs(IOperation val)
        {
            operationString = "Get adress";
            a = val;
            returnType = ValueType.Cint;
        }
    }
    class GetValByAdress : MonoOperation
    {
        int adrs;
        public GetValByAdress(int adress, ValueType retType)
        {
            operationString = "Get value by adress";
            adrs = adress;
            returnType = retType;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}{1} ({2})", MISC.tabs(depth), ASTTree.variables[adrs].name, adrs));
            //Console.WriteLine(String.Format("{0}{1} [{2}]", MISC.tabs(depth), operationString, adrs));
        }
        
    }
}
