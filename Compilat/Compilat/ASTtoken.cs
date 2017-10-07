using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public interface IASTtoken : IOperation
    {
        void Trace(int depth);
        void TraceMore(int depth);

        ValueType getValueType { get; }
        Object getValue { get; }
        ValueType returnTypes();

    }

    public class ASTvalue : IASTtoken
    {
        ValueType valType;
        Object data;
        ConsoleColor clr;

        public ASTvalue(ValueType vt, Object data)
        {
            this.valType = vt;
            this.data = data;
            ASTTree.tokens.Add(this);
            clr = ConsoleColor.White;
            if (vt == ValueType.Cchar) clr = ConsoleColor.Blue;
            if (vt == ValueType.Cstring) clr = ConsoleColor.DarkBlue;
            if (vt == ValueType.Cint) clr = ConsoleColor.Cyan;
            if (vt == ValueType.Cdouble) clr = ConsoleColor.DarkCyan;
            if (vt == ValueType.Cboolean) clr = ConsoleColor.Yellow;
            if (vt == ValueType.Cadress) clr = ConsoleColor.DarkGray;
        }
        public ASTvalue(string s)
        {
            string nums = "-1234567890.";
            bool isnum = true;
            int numPoints = 0;
            // cheking if is a nubmer
            for (int i = 0; i < s.Length; i++)
            {
                if (nums.IndexOf(s[i]) < 0) { isnum = false; break; }
                if (s[i] == '.') { numPoints++; if (numPoints > 1) { isnum = false; break; } }
                if (i > 0 && s[i] == '-') { isnum = false; break; }
            }
            // calculate a number
            if (isnum)
            {
                if (numPoints == 0) { this.valType = ValueType.Cint; this.data = (object)(int.Parse(s)); clr = ConsoleColor.Cyan; }
                else { this.valType = ValueType.Cdouble; this.data = (object)(double.Parse(s.Replace('.', ','))); clr = ConsoleColor.DarkCyan; }
            }
            else
            {
                // detect char
                if (s.IndexOf('\'') == 0 && s.LastIndexOf('\'') == s.Length - 1)
                {
                    if (s.Length == 3)
                    { this.valType = ValueType.Cchar; this.data = (object)(s[1]); clr = ConsoleColor.Blue; }
                    else
                    { throw new Exception("Char can not be more than 1 symbol"); }
                }
                else
                {
                    // detect string
                    if (s.IndexOf('\"') == 0 && s.LastIndexOf('\"') == s.Length - 1)
                    { this.valType = ValueType.Cstring; this.data = (object)(s.Substring(1, s.Length - 2)); clr = ConsoleColor.DarkBlue; }
                    else
                    {
                        if (s.ToLower() == "true" || s.ToLower() == "false")
                        {
                            this.valType = ValueType.Cboolean; this.data = (object)((s.ToLower() == "true"));
                            clr = ConsoleColor.Yellow;
                        }
                        else
                        {
                            // finally trying to find variable
                            string varName = s;
                            int found = -1;
                            ASTvariable foundedVar = new ASTvariable(ValueType.Unknown, "NONE");

                            for (int i = 0; i < ASTTree.variables.Count; i++)
                                if (ASTTree.variables[i].name == varName && MISC.isVariableAvailable(i))
                                { foundedVar = ASTTree.variables[i]; found = i; break; }

                            if (found < 0)
                                throw new Exception("Used a variable \"" + varName + "\", that was never defined in this context!");
                            else
                            {
                                this.valType = foundedVar.getValueType;
                                throw new Exception("GetAddr_" +found);
                            }
                        }
                    }
                }
            }
            ASTTree.tokens.Add(this);
        }

        public void Trace(int depth)
        {
            string br = "";
            if (this.getValueType == ValueType.Cstring) br = "\"";
            if (this.getValueType == ValueType.Cchar) br = "\'";
            if (this.getValueType == ValueType.Cadress) br = "#";
            if (data == null)
            {
                //Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth), "null"));
                Console.Write(MISC.tabs(depth));
                MISC.ConsoleWriteLine("null", ConsoleColor.Red);
            }
            else
            {
                //Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth), (br + data.ToString() + br)));
                Console.Write(MISC.tabs(depth));
                MISC.ConsoleWriteLine((br + data.ToString() + br), clr);
            }
        }
        public void TraceMore(int depth)
        {
            string br = "";
            if (this.getValueType == ValueType.Cstring) br = "\"";
            if (this.getValueType == ValueType.Cchar) br = "\'";
            if (data == null)
                MISC.ConsoleWriteLine(String.Format("\tnull\t\t[{0}]", valType.ToString()), clr);
            else
                MISC.ConsoleWriteLine(String.Format("\t{0}\t\t[{1}]", (br + data.ToString() + br), valType.ToString()), clr);
        }

        public ValueType getValueType { get { return valType; } }
        public Object getValue { get { return data; } }
        public ValueType returnTypes() { return valType; }
    }

    public class ASTvariable : IASTtoken
    {
        ValueType valType;
        public string name;
        int adress;
        string localSpace;

        public ASTvariable()
        {
            this.valType = ValueType.Unknown;
            this.name = "-";
            this.adress = -1;
            this.localSpace = string.Join("/", MISC.nowParsing.ToArray());
        }
        public ASTvariable(ValueType vt, string name)
        {
            this.valType = vt;
            this.name = name;
            this.adress = ASTTree.variables.Count;
            this.localSpace = string.Join("/", MISC.nowParsing.ToArray());
        }

        public virtual void Trace(int depth)
        {
            //Console.WriteLine(String.Format("{0}${1}   [{2}]", MISC.tabs(depth), name, valType.ToString()));
            Console.Write(MISC.tabs(depth));
            MISC.ConsoleWrite(name, ConsoleColor.Green);
            MISC.ConsoleWriteLine("\t[" +  valType.ToString() + "]", ConsoleColor.DarkGreen);
        }
        public virtual void TraceMore(int depth)
        {
            MISC.ConsoleWrite(String.Format("\t{0}\t\t{1}\t\t", name, valType.ToString().Substring(1)), ConsoleColor.DarkGreen);
            MISC.ConsoleWriteLine(localSpace, ConsoleColor.DarkMagenta);
        }

        public virtual ValueType getValueType 
        { get { return valType; } }
        public Object getValue
        { get { return null; } }
        public virtual ValueType returnTypes()
        { return getValueType; }
        public virtual bool IsPointer { get { return false; } }
    }

    public class ASTpointer : ASTvariable
    {
        ValueType valType;
        ValueType returnType;
        //int adress;

        public ASTpointer(ValueType vt, string name)
        {
            this.returnType = ValueType.Cadress;
            this.valType = vt;
            this.name = name;
        }

        public override void Trace(int depth)
        {
            //Console.WriteLine(String.Format("{0}*{1}   [{2} -> {3}]", MISC.tabs(depth), name, returnType.ToString(), valType.ToString()));
            Console.Write(MISC.tabs(depth)+"*");
            MISC.ConsoleWrite(name, ConsoleColor.Green);
            MISC.ConsoleWriteLine("\t"+returnType.ToString()+"->"+ valType.ToString()+"", ConsoleColor.DarkGreen);
            //MISC.finish = true;
            //adress.Trace(depth + 1);
        }
        public override void TraceMore(int depth)
        {
            MISC.ConsoleWriteLine(String.Format("\t*{0}\t\t[{1} -> {2}]", name, returnType.ToString().Substring(1), valType.ToString().Substring(1)), ConsoleColor.Green);
        }

        public override ValueType returnTypes()
        { 
            //if (valType == ValueType.Cadress) 
            //    return ASTTree.variables[(int) this.getValue].getValueType;
            return valType; 
        }

        public override ValueType getValueType { get { return returnType; } }


        public override bool IsPointer { get { return true; } }
    }
}
