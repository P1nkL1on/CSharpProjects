using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public class ASTTree
    {
        IAstNode rootNode;
        string original;
        public static List<ASTValue> vars = new List<ASTValue>();

        public void Trace()
        {
            Console.WriteLine(original);
            rootNode.Trace(0);

            Console.WriteLine("\n__Var_table__");
            for (int i = 0; i < vars.Count; i++)
                Console.WriteLine(String.Format("  {0}.\t{3}\t{1}\t[{2}]", vars[i].index, vars[i].getValue.ToString(), vars[i].getValueType, vars[i].name));
        }

        public ASTTree(string s)
        {
            string sTrim = "";
            for (int i = 0; i < s.Length; i++)
                if (s[i] != ' ') sTrim += s[i];
            original = s;
            rootNode = new CommandOrder(sTrim, ';');
        }
    }

    public struct ASTValue : IOperation
    {
        ValueType type;
        
        Object value;
        public int index;
        public string name;

        public ASTValue(Object value)
        {
            this.type = ValueType.Cint;
            this.value = 0;
            this.index = -1;
            this.name = "value";
        }

        public ASTValue(ValueType type, Object value)
        {
            this.type = type; this.value = value;
            this.index = ASTTree.vars.Count;

            this.name = "value";

            ASTTree.vars.Add(this);
        }

        public void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}{3}", MISC.tabs(depth), index.ToString(), type, value.ToString()));
        }

        public ASTValue calculateOperation()
        {
            return this;
        }
        public ValueType getValueType
        {
            get { return type; }
        }
        public Object getValue
        {
            get { return value; }
        }

        public IOperation ParseFrom(string s)
        {
            return new ASTValue(ValueType.Cint, s);
        }
    }

    public enum ValueType
    {
        Cint = 0,
        Cdouble = 1,
        Cchar = 2,
        Cstring = 3,
        Cboolean = 4,
        Carray = 5
    }
}
