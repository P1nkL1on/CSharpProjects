using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    struct ASTFunctionCall : IOperation
    {

        int functionCallNumber;
        List<IOperation> arguments;

        public ASTFunctionCall(string s)
        {
            string approximateFuncName = s.Substring(0, s.IndexOf("("));
            bool foundAnalog = false; int i = 0;
            while (!foundAnalog && i < ASTTree.funcs.Count)
            {
                bool nameSame = (ASTTree.funcs[i].getName == approximateFuncName);
                // if same name then check correct of all types including
                if (nameSame)
                {
                    foundAnalog = true;
                }
                i++;
            }
            // declare
            functionCallNumber = i - 1;
            arguments = new List<IOperation>();

            //make bug
            if (!foundAnalog)
                throw new Exception("Function with this name/arguments was never declared!");
        }

        public void Trace(int depth)
        {
            Console.WriteLine(String.Format("{0}{1}  [{2}]", MISC.tabs(depth), ASTTree.funcs[functionCallNumber].getName,
                              ASTTree.funcs[functionCallNumber].returnTypes().ToString()));
            for (int i = 0; i < arguments.Count; i++)
            {
                arguments[i].Trace(depth + 1);
                if (i == arguments.Count - 1)
                    MISC.finish = true;
            }
        }
        public ValueType returnTypes()
        {
            return ASTTree.funcs[functionCallNumber].returnTypes();
        }

    }
}
