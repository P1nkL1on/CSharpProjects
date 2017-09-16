using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public abstract class IOperator : ICommand
    {
        protected IOperation condition;   // if (...){}
        protected List<CommandOrder> actions;  // {....} {....} {....} {....} {....}
        // for IF there are only 2, for SWITCH is infinite + DEFAULT

        public virtual void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "Default operator trace");
        }   
    }

    public class OperatorIf : IOperator
    {
        public OperatorIf(string parseCondition, string parseActions, string parseElseAction)
        {
            condition = new Equal(BinaryOperation.ParseFrom(parseCondition), new ASTValue(ValueType.Cboolean, (object)true));
            actions = new List<CommandOrder>();
            actions.Add(new CommandOrder(""));
            actions.Add(new CommandOrder(""));

            actions[0].MergeWith(new CommandOrder(parseActions, ';'));
            actions[1].MergeWith(new CommandOrder(parseElseAction, ';'));
        }

        public override void Trace(int depth)
        {
            
            Console.WriteLine(String.Format("{0}IF", MISC.tabs(depth)));
            condition.Trace(depth + 1);
            
            Console.WriteLine(String.Format("{0}THEN", MISC.tabs(depth)));
            actions[0].Trace(depth + 1);

            if (actions[1].CommandCount > 0)
            {
                Console.WriteLine(String.Format("{0}ELSE", MISC.tabs(depth)));
                actions[1].Trace(depth + 1);
            }
        }
    }
    
}
