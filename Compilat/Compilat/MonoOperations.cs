using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class Define : BinaryOperation
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
                throw new Exception("Can not define variable with name \"" + ss[1] + "\"");

            ss[0].ToLower();
            varType = detectType(ss[0]);

            TypeConvertion tpcv = new TypeConvertion("IIDDBBCCSS$$", 1);
            returnType = MISC.CheckTypeCorrect(tpcv, varType);

            defineType = varType;
            //for (int i =0; i < varName.Length; i++){
            bool isPointer = false;
            if (varName.IndexOf('*') == 0) { isPointer = true; varName = varName.Substring(1); returnType = ValueType.Cint; }
            if (varName.LastIndexOf("]") == varName.Length - 1 && varName.IndexOf("[") > 0)
            {
                IOperation arrayLength = BinaryOperation.ParseFrom(MISC.getIn(varName, varName.IndexOf('[')));
                if (arrayLength as ASTvalue == null || arrayLength.returnTypes() != ValueType.Cint)
                    throw new Exception("Incorrect array length parameters!");
                isPointer = true; varName = varName.Substring(0, varName.IndexOf('['));
                // push an array
                int length = (int)(arrayLength as ASTvalue).getValue;
                if (length < 1)
                    throw new Exception("Array length should be 1 and more!");
                for (int i = 0; i < length; i++)
                {
                    // as default variable
                    ASTvariable newVar = new ASTvariable(varType, varName + "#" + i);
                    ASTTree.variables.Add(newVar);
                    MISC.pushVariable(ASTTree.variables.Count - 1);
                    ASTTree.tokens.Add(newVar);
                }
            }

            if (!isPointer)
            {
                ASTvariable newVar = new ASTvariable(varType, varName);
                ASTTree.variables.Add(newVar);
                MISC.pushVariable(ASTTree.variables.Count - 1);

                ASTTree.tokens.Add(newVar);
                a = newVar;
            }
            else
            {
                ASTpointer newPointer = new ASTpointer(varType, varName);
                defineType = ValueType.Cadress;
                ASTTree.variables.Add(newPointer);
                MISC.pushVariable(ASTTree.variables.Count - 1);

                ASTTree.tokens.Add(newPointer);
                a = newPointer;
            }
            returnType = defineType;
            b = new ASTvalue(ValueType.Cadress, (object)(ASTTree.variables.Count - 1));
        }
        public static ValueType detectType(string s)
        {
            if (s == "double") return ValueType.Cdouble;
            if (s == "int") return ValueType.Cint;
            if (s == "string") return ValueType.Cstring;
            if (s == "char") return ValueType.Cchar;
            if (s == "bool") return ValueType.Cboolean;
            if (s == "void") return ValueType.Cvoid;
            return ValueType.Unknown;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "DEFINE [" + defineType.ToString() + "]");
            a.Trace(depth + 1);
            MISC.finish = true;
            b.Trace(depth + 1);
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
        //public ValueType wantType;
        public Adrs(IOperation val)
        {
            if (val as GetValByAdress == null)
                throw new Exception("Can not get adress of non-variable token!");
            operationString = "Get adress";
            a = val;
            returnType = ValueType.Cadress;
        }

    }
    class GetValByAdress : MonoOperation
    {
        public ValueType pointerType;

        public GetValByAdress(IOperation adress, ValueType retType)
        {
            operationString = "Get value by adress";
            a = adress;
            if (a.returnTypes() != ValueType.Cadress)
                throw new Exception("You can get value only by number of memory slot!");

            returnType = retType;
            
            if (retType == ValueType.Cadress)
            {
                IOperation dep = a; int res = -1;
                while ((a as GetValByAdress) != null)
                {
                    a.Trace(0);
                    res = (a as GetValByAdress).GetAdress();
                    a = (a as GetValByAdress).a;
                }
                if (res >= 0)
                    returnType = ASTTree.variables[res].returnTypes();
                //Console.WriteLine(a.returnTypes() + " /// " + res + " //// " + returnType.ToString());
                //Console.ReadKey();
                a = dep;
            }
                
        }

        public int GetAdress (){
            if (a as ASTvalue != null)
            {
                int variableNumber = (int)((a as ASTvalue).getValue);
                if (variableNumber >= ASTTree.variables.Count)
                    return -1;
                return variableNumber;
            }
            return -1;
        }
    }
}
