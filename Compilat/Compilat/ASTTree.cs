using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public class ASTTree
    {
        public static List<ASTFunction> funcs = new List<ASTFunction>();
        string original;
        public static List<ASTValue> tokens = new List<ASTValue>();
        public static List<ASTValue> variables = new List<ASTValue>();


        public void Trace()
        {
            Console.WriteLine(original);
            if (funcs.Count <= 0)
                return;
            for (int i = 0; i < funcs.Count; i++)
                funcs[i].Trace(0);

            Console.WriteLine("\n__TOKENS__");
            for (int i = 0; i < tokens.Count; i++)
                Console.WriteLine(String.Format("  {0}.\t{3}\t{1}\t{2}",
                    tokens[i].index, (tokens[i].getValue == null) ? "-" : tokens[i].getValue.ToString(),
                    tokens[i].getValueType, tokens[i].name));
            Console.WriteLine("\n__VARIABLES__");
            for (int i = 0; i < variables.Count; i++)
                Console.WriteLine(String.Format("  {0}.\t{3}\t{1}\t{2}",
                    variables[i].index, (variables[i].getValue == null) ? "-" : variables[i].getValue.ToString(),
                    variables[i].getValueType, variables[i].name));
            Console.WriteLine("\n___FUNCTIONS___");
            for (int i = 0; i < funcs.Count; i++)
            {
                string s = "";
                Console.WriteLine(String.Format("  {0}: {1} => {2}", funcs[i].getName, funcs[i].getInputType, funcs[i].returnTypes().ToString()));

            }
        }

        public ASTTree(string s)
        {
            string sTrim = "";
            int bracketLevel = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ') sTrim += s[i];
                if (bracketLevel == 1 && i > 0 && s[i] == '}')
                    sTrim += "^";
                bracketLevel += (s[i] == '{') ? 1 : ((s[i] == '}') ? -1 : 0);
            }
            sTrim = sTrim.Remove(sTrim.Length - 1);
            original = s;
            string[] funcParseMaterial = sTrim.Split('^');
            try
            {
                for (int i = 0; i < funcParseMaterial.Length; i++)
                    funcs.Add(new ASTFunction(funcParseMaterial[i]));
                //new CommandOrder(sTrim, ';');
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
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
        /// <summary>
        /// Create a empty variable of certain type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ASTValue(String name, ValueType type)
        {
            this.type = type; this.value = null;
            this.index = ASTTree.variables.Count;
            this.name = name;
        }
        /// <summary>
        /// Create a value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public ASTValue(ValueType type, Object value)
        {
            this.type = type; this.value = value;
            this.index = ASTTree.tokens.Count;
            this.name = "-";
            ASTTree.tokens.Add(this);
        }
        /// <summary>
        /// Parse a variable/value of unknown type
        /// </summary>
        /// <param name="s"></param>
        public ASTValue(string s)
        {
            this.name = "-";
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
                if (numPoints == 0) { this.type = ValueType.Cint; this.value = (object)(int.Parse(s)); }
                else { this.type = ValueType.Cdouble; this.value = (object)(double.Parse(s.Replace('.', ','))); }
            }
            else
            {
                // detect char
                if (s.IndexOf('\'') == 0 && s.LastIndexOf('\'') == s.Length - 1)
                {
                    if (s.Length == 3)
                    { this.type = ValueType.Cchar; this.value = (object)(s[1]); }
                    else
                    { throw new Exception("Char can not be more than 1 symbol"); }
                }
                else
                {
                    // detect string
                    if (s.IndexOf('\"') == 0 && s.LastIndexOf('\"') == s.Length - 1)
                    { this.type = ValueType.Cstring; this.value = (object)(s.Substring(1, s.Length - 2)); }
                    else
                    {
                        if (s.ToLower() == "true" || s.ToLower() == "false")
                        {
                            this.type = ValueType.Cboolean; this.value = (object)((s.ToLower() == "true"));
                        }
                        else
                        {
                            // finally variable
                            this.name = s;
                            ASTValue getedVar = new ASTValue(); bool found = false;
                            for (int i = 0; i < ASTTree.variables.Count; i++)
                                if (ASTTree.variables[i].name == this.name && MISC.isVariableAvailable(i))
                                    { getedVar = ASTTree.variables[i]; found = true; }

                            if (!found)
                            {
                                throw new Exception("Used a variable \"" + this.name + "\", that was never defined in this context!");
                            }
                            else
                            {
                                this.type = getedVar.type;
                                this.value = (object)getedVar.value;
                            }
                        }
                    }
                }
            }


            this.index = ASTTree.tokens.Count;
            ASTTree.tokens.Add(this);
        }


        public void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth), (this.name == "-") ? value.ToString() : name, this.index)
                /*+ "\t" + returnTypes().ToString()*/);
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

        public ValueType returnTypes()
        {
            return type;
        }
    }

    public enum ValueType
    {
        Cint = 0,
        Cdouble = 1,
        Cchar = 2,
        Cstring = 3,
        Cboolean = 4,
        Carray = 5,
        Cvariable = 6,
        Cvoid = 7,
        Unknown = 8
    }

    public struct TypeConvertion
    {
        public List<ValueType>[] from;
        public ValueType[] to;


        public TypeConvertion(string s, int IOcount)
        {
            IOcount++;
            List<List<ValueType>> flist = new List<List<ValueType>>();
            List<ValueType> tlist = new List<ValueType>();
            List<ValueType> inputVals = new List<ValueType>();
            for (int i = 0; i < s.Length; i++)
            {
                ValueType vt;
                switch (s[i])
                {
                    case '$':
                        vt = ValueType.Cvariable; break;
                    case 'I':
                        vt = ValueType.Cint; break;
                    case 'D':
                        vt = ValueType.Cdouble; break;
                    case 'B':
                        vt = ValueType.Cboolean; break;
                    case 'C':
                        vt = ValueType.Cchar; break;
                    case 'S':
                        vt = ValueType.Cstring; break;
                    case '_':
                        vt = ValueType.Cvoid; break;
                    default:
                        vt = ValueType.Cvariable; break;
                }
                if (i % IOcount == IOcount - 1)
                    tlist.Add(vt);
                else
                {
                    if (i % IOcount < IOcount - 2)
                        inputVals.Add(vt);
                    else
                    {
                        inputVals.Add(vt);
                        flist.Add(inputVals.ToArray().ToList());
                        inputVals.Clear();
                    }
                }


            }
            from = flist.ToArray<List<ValueType>>();
            to = tlist.ToArray();
        }
    }
}
