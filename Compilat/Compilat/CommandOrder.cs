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

            List<string> commandArr = MISC.splitBy(S, separators);

            for (int i = 0; i < commandArr.Count; i++)
            {
                ICommand[] parsedCommands = ParseCommand(commandArr[i]);
                for (int c = 0; c < parsedCommands.Length; c++)
                    commands.Add(parsedCommands[c]);
            }

        }


        public ICommand[] ParseCommand(String S)
        {
            // here we get a 1 string between ;...;
            // it can be cycle or simple operation
            int p1 = MISC.IndexOfOnLevel0(S, "{", 0),
                p2 = MISC.IndexOfOnLevel0(S, "}", 0);
            if (p1 == 0 && p2 == S.Length - 1)
            {
                return new ICommand[] { new OperatorZone(MISC.getIn(S, S.IndexOf('{'))) };
            }
            if ((S.IndexOf("{") < 0 || (MISC.IndexOfOnLevel0(S, "=", 0) > 0))
                && S.ToLower().IndexOf("if") != 0/* && S.ToLower().IndexOf("else") != 0*/)
            {
                IOperation newBO = BinaryOperation.ParseFrom(S);
                if ((newBO as Assum) != null && (newBO as Assum).requiredUpdate != "none")
                {
                    string needUpdate = (newBO as Assum).requiredUpdate;
                    if (needUpdate.IndexOf("structdefineinfor") == 0)
                    {
                        string nam = (newBO as Assum).GetAssumableName;
                        if (nam == "-")
                            throw new Exception("What are you doing!?");
                        List<IOperation> values = (newBO as Assum).GetStructDefine();
                        List<ICommand> res = new List<ICommand>();
                        for (int i = 0; i < values.Count; i++)
                            res.Add(new Assum(BinaryOperation.ParseFrom(nam + "[" + i + "]"), values[i]));

                        return res.ToArray();
                    }
                }
                else
                    return new ICommand[] { newBO };
            }
            #region Cycles
            //try to parse while cycles
            if (S.ToLower().IndexOf("for") == 0)
            {
                string parseCondition = MISC.getIn(S, S.IndexOf('(')),
                       parseAction = MISC.getIn(S, S.IndexOf('{'));

                string[] conditionParts = parseCondition.Split(';');
                if (conditionParts.Length != 3)
                    throw new Exception("Invalid count of FOR-cycle condition parts");
                // first one - is simple commands of initialization

                MISC.GoDeep("FOR");
                if (conditionParts[0].Length > 0)
                    this.MergeWith(new CommandOrder(conditionParts[0], ','));    // included
                if (conditionParts[1].Length <= 0)
                    conditionParts[1] = "true";
                // parse commands
                
                CommandOrder actions = new CommandOrder(parseAction, ';');
                if (conditionParts[2].Length > 0)
                    actions.MergeWith(new CommandOrder(conditionParts[2], ','));
                
                ICommand[] res = new ICommand[] { new CycleFor(conditionParts[1], actions) };
                MISC.GoBack();
                return res;
            }
            if (S.ToLower().IndexOf("while") == 0)
                return new ICommand[] { new CycleWhile(MISC.getIn(S, S.IndexOf('(')), MISC.getIn(S, S.IndexOf('{')), false) };
            if (S.ToLower().IndexOf("do") == 0)
                return new ICommand[] { new CycleWhile(MISC.getIn(S, S.IndexOf('(')), MISC.getIn(S, S.IndexOf('{')), true) };
            #endregion
            #region Operators
            if (S.ToLower().IndexOf("if") == 0)
            {
                int indexOfConditionRightBrakket = MISC.IndexOfOnLevel0(S, ")", 0);
                if (S.IndexOf("{") - 1 == indexOfConditionRightBrakket)
                {
                    int pos1 = MISC.IndexOfOnLevel0(S, "}", 0),
                        pos2 = MISC.IndexOfOnLevel0(S, "}", pos1 + 1),
                        posElse = MISC.IndexOfOnLevel0(S, "}else{", 0);
                    if (pos2 < pos1)
                        return new ICommand[] { new OperatorIf(MISC.getIn(S, S.IndexOf('(')), MISC.getIn(S, S.IndexOf('{')), "") };
                    else
                        return new ICommand[] { new OperatorIf(MISC.getIn(S, S.IndexOf('(')), MISC.getIn(S, S.IndexOf('{')), MISC.getIn(S, S.LastIndexOf("{"))) };
                }
                else
                {
                    int indexElse = MISC.IndexOfOnLevel0(S, "else", 0);
                    if (indexElse < 0)
                        return new ICommand[] { new OperatorIf(MISC.getIn(S, S.IndexOf('(')), S.Substring(indexOfConditionRightBrakket + 1), "") };
                    else
                        return new ICommand[]{new OperatorIf(MISC.getIn(S, S.IndexOf('(')),
                            S.Substring(indexOfConditionRightBrakket + 1, indexElse - indexOfConditionRightBrakket - 1),
                            S.Substring(indexElse + 4))};
                }
            }
            #endregion
            throw new Exception("Can not parse a command");
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
            Console.WriteLine(String.Format("{0}" + ((commands.Count > 0) ? "C" : "Empty c") + "ommand order <{1}> :", MISC.tabs(depth), commands.Count));
            for (int i = 0; i < commands.Count; i++)
            {
                //if (commands.Count > 1)
                //    Console.WriteLine(String.Format("{0}#{1}", MISC.tabs(depth+1), i + 1));
                if (i == commands.Count - 1)
                    MISC.finish = true;
                commands[i].Trace(depth + 1);
            }
        }

        public int CommandCount
        {
            get { return commands.Count(); }
        }
    }
}
