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
        List<Define> input;
        CommandOrder actions;
        //
        public ASTFunction(string S)
        {
            string s = S.Substring(0, S.IndexOf('('));

            int varType = Math.Max((s.IndexOf("int") >= 0) ? 2 : -1, Math.Max((s.IndexOf("double") >= 0) ? 5 : -1, Math.Max((s.IndexOf("char") >= 0) ? 3 : -1,
                Math.Max((s.IndexOf("string") >= 0) ? 5 : -1, Math.Max((s.IndexOf("bool") >= 0) ? 3 : -1, (s.IndexOf("void") >= 0) ? 3 : -1)))));
            if (varType >= 0)
            {
                string[] type_name = s.Split(s[varType + 1]);
                name = s[varType + 1] + type_name[1];
                IO.to = new ValueType[] { Define.detectType(type_name[0]) };
                // try to parse signature and actions
                List<string> vars = MISC.splitBy(MISC.getIn(S, S.IndexOf('(')), ',');
                input = new List<Define>();
                for (int i = 0; i < vars.Count; i++)
                    input.Add((Define)MonoOperation.ParseFrom(vars[i]));

                MISC.GoDeep("FUNCTION$"+name);
                actions = new CommandOrder(MISC.getIn(S, S.IndexOf('{')), ';');
                MISC.GoBack();
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
            Console.WriteLine(String.Format("{0}FUNCTION \"{1}\"", MISC.tabs(depth), this.name));
            Console.WriteLine(String.Format("{0}<<", MISC.tabs(depth + 1)));

            for (int i = 0; i < input.Count; i++) { if (i == input.Count - 1)MISC.finish = true; input[i].Trace(depth + 2); }

            Console.WriteLine(String.Format("{0}>>", MISC.tabs(depth + 1)));
            MISC.finish = true;
            Console.WriteLine(String.Format("{0}{1}", MISC.tabs(depth + 2), returnTypes().ToString()));
            MISC.finish = true;
            actions.Trace(depth + 1);
        }
    }
}
