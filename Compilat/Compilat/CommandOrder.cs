using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public class CommandOrder : IAstNode
    {
        // there can be any ASI doing one by one
        protected List<ICommand> commands;

        public CommandOrder(params ICommand[] cmnds)
        {
            commands = cmnds.ToList<ICommand>();
        }

        public CommandOrder(String S, params char[] separators)
        {
            commands = new List<ICommand>();

            // calculate a line enters
            string S0 = S; S = "";
            for (int i = 0; i < S0.Length; i++)
                if (S0[i] != '\n')
                    S += S0[i];

            List<string> commandArr = splitBy(S, separators);

            for (int i = 0; i < commandArr.Count; i++)
                commands.Add(ParseCommand(commandArr[i]));

        }
        static List<string> splitBy(string S, params char[] seps)
        {
            List<string> res = new List<string>();
            res.Add("");
            int founded = 0, level = 0;
            for (int i = 0; i < S.Length;
                i++, level += (i < S.Length) ? ((S[i] == '(' || S[i] == '{') ? 1 : (S[i] == ')' || S[i] == '}') ? -1 : 0) : 0)

                for (int j = 0; j < seps.Length; j++)   // count separate
                    if (S[i] == seps[j] && level == 0)
                    { founded++; res.Add(""); }
                    else
                        res[founded] += S[i];

            List<string> res2 = new List<string>(); // delete zero long
            for (int i = 0; i < res.Count; i++)
                if (res[i].Length > 0)
                    res2.Add(res[i]);

            return res2;
        }
        static string getIn(string S, int pos)
        {

            char c = S[pos], c2 = ' ';
            if (c != '(' && c != '{')
                return S;
            if (c == '(') c2 = ')';
            if (c == '{') c2 = '}';

            int nowLevel = 0;
            string res = "";
            for (int i = pos; i < S.Length; i++)
            {
                if (S[i] == c2) { nowLevel--; if (nowLevel == 0) return res;}
                if (nowLevel >= 1) res += S[i];
                if (S[i] == c) nowLevel++;
            }
            return res; // get operand or commands
        }

        static int IndexOfOnLevel0( string S, string subS, int from )
        {
            int pos = S.IndexOf(subS, from);
            if (pos < 0)
                return -1;

            int level = 0;
            for (int i = 0; i <= pos; i++)
                level += (S[i] == '(' || S[i] == '{') ? 1 : ((S[i] == ')' || S[i] == '}') ? -1 : 0);
            return (level == 0)? pos : -1;            
        }

        public ICommand ParseCommand(String S)
        {
            // here we get a 1 string between ;...;
            // it can be cycle or simple operation
            if (S.IndexOf("{") < 0)
                // it can not be cycle
                return BinaryOperation.ParseFrom(S);

            #region Cycles
            //try to parse while cycles
            if (S.ToLower().IndexOf("for") == 0)
            {
                string parseCondition = getIn(S, S.IndexOf('(')),
                       parseAction = getIn(S, S.IndexOf('{'));

                string[] conditionParts = parseCondition.Split(';');
                if (conditionParts.Length != 3)
                    throw new Exception("Invalid count of FOR-cycle condition parts");
                // first one - is simple commands of initialization
                if (conditionParts[0].Length > 0)
                    this.MergeWith(new CommandOrder(conditionParts[0], ','));    // included
                if (conditionParts[1].Length <= 0)
                    conditionParts[1] = "true";
                // parse commands
                CommandOrder actions = new CommandOrder(parseAction, ';');
                if (conditionParts[2].Length > 0)
                    actions.MergeWith(new CommandOrder(conditionParts[2], ','));
                return new CycleFor(conditionParts[1], actions);

            }
            if (S.ToLower().IndexOf("while") == 0)
                return new CycleWhile(getIn(S, S.IndexOf('(')), getIn(S, S.IndexOf('{')), false);
            if (S.ToLower().IndexOf("do") == 0)
                return new CycleWhile(getIn(S, S.IndexOf('(')), getIn(S, S.IndexOf('{')), true);
            #endregion
            #region Operators
            if (S.ToLower().IndexOf("if") == 0)
            {
                int pos1 = IndexOfOnLevel0(S, "}", 0),
                    pos2 = IndexOfOnLevel0(S, "}", pos1 + 1),
                    posElse = IndexOfOnLevel0(S, "}else{", 0);
                if (pos2 < pos1)
                    return new OperatorIf(getIn(S, S.IndexOf('(')), getIn(S, S.IndexOf('{')), "");
                else
                    return new OperatorIf(getIn(S, S.IndexOf('(')), getIn(S, S.IndexOf('{')), getIn(S, S.LastIndexOf("{")));
            }
            #endregion
            return null;
        }

        public void MergeWith(CommandOrder another)
        {
            for (int i = 0; i < another.commands.Count; i++)
                commands.Add(another.commands[i]);
            // merge with            
        }

        public void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}"+((commands.Count>0)? "C" : "Empty c")+"ommand order <{1}> :", MISC.tabs(depth), commands.Count));
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands.Count > 1)
                    Console.WriteLine(String.Format("{0}#{1}", MISC.tabs(depth), i+1));
                if (commands[i] != null)
                    commands[i].Trace(depth + 1);
            }
        }

        public int CommandCount
        {
            get { return commands.Count(); }
        }
    }
}
