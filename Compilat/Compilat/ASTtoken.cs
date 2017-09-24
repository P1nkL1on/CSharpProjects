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

        public ASTvalue(ValueType vt, Object data)
        {
            this.valType = vt;
            this.data = data;
            ASTTree.tokens.Add(this);
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
                if (numPoints == 0) { this.valType = ValueType.Cint; this.data = (object)(int.Parse(s)); }
                else { this.valType = ValueType.Cdouble; this.data = (object)(double.Parse(s.Replace('.', ','))); }
            }
            else
            {
                // detect char
                if (s.IndexOf('\'') == 0 && s.LastIndexOf('\'') == s.Length - 1)
                {
                    if (s.Length == 3)
                    { this.valType = ValueType.Cchar; this.data = (object)(s[1]); }
                    else
                    { throw new Exception("Char can not be more than 1 symbol"); }
                }
                else
                {
                    // detect string
                    if (s.IndexOf('\"') == 0 && s.LastIndexOf('\"') == s.Length - 1)
                    { this.valType = ValueType.Cstring; this.data = (object)(s.Substring(1, s.Length - 2)); }
                    else
                    {
                        if (s.ToLower() == "true" || s.ToLower() == "false")
                        {
                            this.valType = ValueType.Cboolean; this.data = (object)((s.ToLower() == "true"));
                        }
                        else
                        {
                            // finally trying to find variable
                            string varName = s;
                            int found = -1;
                            ASTvariable foundedVar = new ASTvariable(ValueType.Unknown, "NONE");

                            for (int i = 0; i < ASTTree.variables.Count; i++)
                                if (ASTTree.variables[i].name ==varName && MISC.isVariableAvailable(i))
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
                Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth), "null"));
            else
                Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth), (br + data.ToString() + br)));
        }
        public void TraceMore(int depth)
        {
            string br = "";
            if (this.getValueType == ValueType.Cstring) br = "\"";
            if (this.getValueType == ValueType.Cchar) br = "\'";
            if (data == null)
                Console.WriteLine(String.Format("null\t\t[{0}]", valType.ToString()));
            else
                Console.WriteLine(String.Format("{0}\t\t[{1}]", (br + data.ToString() + br), valType.ToString()));
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

        public ASTvariable()
        {
            this.valType = ValueType.Unknown;
            this.name = "-";
            this.adress = -1;
        }
        public ASTvariable(ValueType vt, string name)
        {
            this.valType = vt;
            this.name = name;
            this.adress = ASTTree.variables.Count;
        }

        public virtual void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}${1}   [{2}]", MISC.tabs(depth), name, valType.ToString()));
        }
        public virtual void TraceMore(int depth)
        {
            Console.WriteLine(String.Format("{0}\t\t[{1}]", name, valType.ToString()));
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
            Console.WriteLine(String.Format("{0}*{1}   [{2} -> {3}]", MISC.tabs(depth), name, returnType.ToString(), valType.ToString()));
            //MISC.finish = true;
            //adress.Trace(depth + 1);
        }
        public override void TraceMore(int depth)
        {
            Console.WriteLine(String.Format("*{0}\t\t[{1} -> {2}]", name, returnType.ToString(), valType.ToString()));
        }

        public override ValueType returnTypes()
        { return valType; }

        public override ValueType getValueType { get { return returnType; } }


        public override bool IsPointer { get { return true; } }
    }
}
