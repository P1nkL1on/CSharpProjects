using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    class ASTFunction : IOperation
    {
        // int func (double a, double y, double z, bool XX){....with only those variables.... return int();}
        string name;
        TypeConvertion IO;
        CommandOrder input;
        CommandOrder actions;
        //
        public ASTFunction(string S)
        {
            string s = S.Substring(0, S.IndexOf('('));

            int varType = Math.Max((s.IndexOf("int") >= 0) ? 2 : -1, Math.Max((s.IndexOf("double") >= 0) ? 5 : -1, Math.Max((s.IndexOf("char") >= 0) ? 3 : -1,
                Math.Max((s.IndexOf("string") >= 0) ? 5 : -1, (s.IndexOf("bool") >= 0) ? 3 : -1))));
            if (varType >= 0)
            {
                string[] type_name = s.Split(s[varType + 1]);
                name = s[varType + 1] + type_name[1];
                IO.to = new ValueType[] { Define.detectType(type_name[0]) };
                // try to parse signature and actions
                input = new CommandOrder (MISC.getIn(S, S.IndexOf('(')), ',');
                actions = new CommandOrder(MISC.getIn(S, S.IndexOf('{')), ';');
                return;
            }

            throw new Exception("Can not parse a function");
        }
        //
        public ValueType returnTypes()
        {
            return IO.to[0];
        }
        //
        public void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}FUNCTION", MISC.tabs(depth)));
            Console.WriteLine(String.Format("{0}<<", MISC.tabs(depth + 1)));
            MISC.finish = true;
            input.Trace(depth + 2);
            Console.WriteLine(String.Format("{0}>>", MISC.tabs(depth + 1)));
            MISC.finish = true;
            Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth + 2), returnTypes().ToString()));
        }
    }
}
