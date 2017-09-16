using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public abstract class ICycle : ICommand
    {
        protected IOperation condition;   // if (...){}
        protected CommandOrder actions;  // if (){...}

        public virtual void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + "Default cycle trace");
        }   
    }

    public class CycleFor : ICycle
    {
        public CycleFor(string parseCondition, CommandOrder actions)
        {
            condition = new Equal(BinaryOperation.ParseFrom(parseCondition), new ASTValue(ValueType.Cboolean, (object)true));
            this.actions = actions;
        }
        public override void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}WHILE (for)", MISC.tabs(depth)));
            condition.Trace(depth + 1);
            Console.WriteLine(String.Format("{0}DO", MISC.tabs(depth)));
            actions.Trace(depth + 1);
        }
    }
    public class CycleWhile : ICycle
    {
        bool doFirst;

        public CycleWhile(string parseCondition, string parseActions, bool doFirst)
        {
            condition = new Equal(BinaryOperation.ParseFrom(parseCondition), new ASTValue(ValueType.Cboolean, (object)true));
            actions = new CommandOrder(parseActions, ';');
            this.doFirst = doFirst;
        }

        public override void Trace(int depth)
        {
            if (!doFirst)
            {
                Console.WriteLine(String.Format("{0}WHILE", MISC.tabs(depth)));
                condition.Trace(depth + 1);
            }
            Console.WriteLine(String.Format("{0}DO", MISC.tabs(depth)));
            actions.Trace(depth + 1);

            if (doFirst)
            {
                Console.WriteLine(String.Format("{0}WHILE", MISC.tabs(depth)));
                condition.Trace(depth + 1);
            }
        }
    }
}
