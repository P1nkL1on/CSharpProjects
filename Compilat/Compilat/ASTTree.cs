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
        public static List<IASTtoken> tokens = new List<IASTtoken>();
        public static List<ASTvariable> variables = new List<ASTvariable>();

        public static ConsoleColor clr = ConsoleColor.Black;

        public void Trace()
        {

            if (funcs.Count <= 0)
                return;
            for (int i = 0; i < funcs.Count; i++)
                if (funcs[i] != null)
                {
                    clr = (funcs[i].getName.ToLower() == "main") ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
                    funcs[i].Trace(0);
                }
            clr = ConsoleColor.Black;

            Console.WriteLine("\nTokens:");
            for (int i = 0; i < tokens.Count; i++)
            {
                tokens[i].TraceMore(0);
            }
            Console.WriteLine("\nVariables");
            for (int i = 0; i < variables.Count; i++)
            {
                Console.Write((i + 0) + ". ");
                variables[i].TraceMore(0);
            }

            Console.WriteLine("\nFunctions:");
            for (int i = 0; i < funcs.Count; i++)
            {
                if (funcs[i] != null)
                    Console.WriteLine(String.Format("  {0}: {1} => {2}", funcs[i].getName, funcs[i].getInputType, funcs[i].returnTypes().ToString()));
                else
                    Console.WriteLine(String.Format("  {0}:", "null"));
            }
        }
        static List<char> brStack = new List<char>();
        public ASTTree(string s)
        {
            string sTrim = "";

            funcs = new List<ASTFunction>();
            tokens = new List<IASTtoken>();
            variables = new List<ASTvariable>();
            MISC.ClearStack();

            sTrim = FuncTrimmer(s); // remove last ^
            original = s;
            string[] funcParseMaterial = sTrim.Split('^');
            try
            {
                for (int i = 0; i < funcParseMaterial.Length; i++)
                    funcs.Add(new ASTFunction(funcParseMaterial[i]));
                // after function declaration we have int foo(); int foo(){return 0;}; need to make them a one function
                for (int i = 0; i < funcs.Count; i++)
                    for (int j = i + 1; j < funcs.Count; j++)
                        if (funcs[i] != null && funcs[j] != null)
                            if (MISC.CompareFunctionSignature(funcs[i], funcs[j]))
                            { funcs[i] = funcs[j]; funcs[j] = null; }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        static string FuncTrimmer(string s)
        {
            string res = "";
            int brL = 0, isStr = 0, isCmt = 0, isChar = 0;

            for (int i = 0; i < s.Length; i++)
            {
                string add = s[i] + "";
                if (isStr == 0 && isChar == 0 && isCmt == 0)
                {
                    if (s[i] == ' ' || s[i] == '\t' || s[i] == '\n' || s[i] == '\r')
                        add = "";

                    if (s[i] == '{') brL++;
                    if (s[i] == '}')
                    {
                        brL--;
                        if (brL == 0) add += '^';   // function separator
                    }
                    if (s[i] == '\"')
                        isStr = 1;
                    if (s[i] == '\'')
                        isChar = 1;
                    if (s[i] == '/' && i < s.Length - 2 && s[i + 1] == '/')
                    {
                        add = ""; isCmt = 1;
                    }
                    if (s[i] == '/' && i < s.Length - 2 && s[i + 1] == '*')
                    {
                        add = ""; isCmt = 2;
                    }
                }
                else
                {
                    if (isCmt == 0)
                    {
                        if (isStr == 0 && isChar > 0 && i > 0 && s[i] == '\'' && (s[i - 1] != '\\' || (s[i - 1] == '\\' && i > 1 && s[i - 2] == '\\')))
                            isChar = 0;
                        if (isChar == 0 && isStr > 0 && i > 0 && s[i] == '\"' && (s[i - 1] != '\\'|| (s[i - 1] == '\\' && i > 1 && s[i - 2] == '\\')))
                            isStr = 0;
                    }
                    else
                    {
                        add = "";
                        if (isCmt == 1 && s[i] == '\n') isCmt = 0;
                        if (isCmt == 2 && s[i] == '*' && i < s.Length - 2 && s[i + 1] == '/') { isCmt = 0; i++; }
                    }
                    
                }
                res += add;
            }

            return res.
            Remove(res.Length - 1); ;
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
        Cadress = 8,
        Unknown = 9
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
                    case 'A':
                        vt = ValueType.Cadress; break;
                    case 'V':
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
