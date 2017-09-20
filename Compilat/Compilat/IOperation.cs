using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public interface ICommand : IAstNode
    {

    }


    public interface IOperation : ICommand
    {
        ValueType returnTypes();
    }

    public abstract class MonoOperation : IOperation
    {
        protected string operationString = "???";
        protected ValueType returnType;
        protected TypeConvertion atArg;
        protected IOperation a;   // pointer
        public virtual void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + operationString);
            MISC.finish = true;
            a.Trace(depth + 1);
        }
        public static IOperation ParseFrom(string s)
        {
            if (s.IndexOf('(') == 0 && s.LastIndexOf(')') == s.Length - 1)
                return BinaryOperation.ParseFrom(MISC.breakBrackets(s));

            if (s.Length > 2 && s.IndexOf("--") == s.Length - 2)
                return new Dscr(ParseFrom(s.Substring(0, s.Length - 2)));

            if (s.Length > 2 && s.IndexOf("++") == s.Length - 2)
                return new Incr(ParseFrom(s.Substring(0, s.Length - 2)));

            if (s.IndexOf("-") == 0)
                return new Mins(ParseFrom(s.Substring(1, s.Length - 1)));

            if (s.IndexOf("!") == 0)
                return new Nega(ParseFrom(s.Substring(1, s.Length - 1)));
            try
            {
                return new ASTFunctionCall(s);
            }
            catch (Exception e) { }

            int varType = Math.Max((s.IndexOf("int")>=0)? 2 : -1, Math.Max((s.IndexOf("double")>=0)? 5 : -1, Math.Max((s.IndexOf("char")>=0)? 3 : -1,
                Math.Max((s.IndexOf("string")>=0)? 5 : -1, (s.IndexOf("bool")>=0)? 3 : -1))));

            if (varType >= 0)
                return new Define(s.Insert(varType + 1, "$"));

            return new ASTValue(s);
        }
        
        public ValueType returnTypes()
        {
            return returnType;
        }
    }

    public abstract class BinaryOperation : IOperation
    {
        protected string operationString = "???";
        protected ValueType returnType;
        // acceptable left and right types
        static int lastIndex = -1;
        protected IOperation a;   // pointer
        protected IOperation b;   // pointer
        public virtual void Trace(int depth)
        {
            Console.WriteLine(MISC.tabs(depth) + operationString + "\t" + returnType.ToString());
            a.Trace(depth + 1);
            MISC.finish = true;
            b.Trace(depth + 1);
        }

        static bool onLevel(string s, string symbols, int level)
        {
            lastIndex = onLevel(s, symbols, level, 0);
            return (lastIndex > -1);
        }

        static int onLevel(string s, string symbols, int level, int from)
        {
            if (s.IndexOf(symbols) < 0)
                return -1;
            if (symbols == "*")
            { int n = 0; }
            char leftBracket = '(', rightBracket = ')';
            int nowLevel = 0,
                pos = s.IndexOf(symbols, from);
            if (pos == -1)
                return -1;

            for (int i = 0, ll = 0; i < s.Length; i++)
            {
                if (s[i] == leftBracket)
                    ll++;
                if (s[i] == rightBracket)
                    ll--;

                if (i > from && i + symbols.Length <= s.Length && ll == 0 && s.Substring(i, symbols.Length) == symbols)
                { pos = i; break; }
            }


            if (pos == -1 || pos >= s.Length - 1)
                return -1;
            // if it is a part of complex symbol then try further
            if (symbols.Length == 1 && pos < s.Length - 1 && pos > 0)
            {
                // kost for <= >=
                if (symbols == "=" && s[pos - 1] == '<' || s[pos - 1] == '>')
                    return onLevel(s, symbols, level, pos + 1);

                switch (s[pos + 1])
                {
                    // == += -= *= /= <= >= in case of =
                    case '=':
                        if (symbols == "=" || symbols == "+" || symbols == "-" || symbols == "*" || symbols == "/" || symbols == "<" || symbols == ">")
                            return onLevel(s, symbols, level, pos + 1);
                        break;
                    // ++
                    case '+':
                        if (symbols == "+")
                            return onLevel(s, symbols, level, pos + 1);
                        break;
                    // --
                    case '-':
                        if (symbols == "-")
                            return onLevel(s, symbols, level, pos + 1);
                        break;
                    default:
                        break;
                }
                if ((s[pos - 1] == s[pos] || s[pos + 1] == s[pos]) && (s[pos] == '=' || s[pos] == '-' || s[pos] == '+'))
                    return onLevel(s, symbols, level, pos + 1);
            }

            for (int i = 0; i < pos; i++)
            {
                if (s[i] == leftBracket)
                    nowLevel++;
                if (s[i] == rightBracket)
                    nowLevel--;
            }
            return (nowLevel == level)? pos : -1;
        }

        /*for (int i = 0; i < s.Length + 1 - symbols.Length; i++)
            {
                if (s[i] == leftBracket)
                    nowLevel++;
                if (s[i] == rightBracket)
                    nowLevel--;
                if (nowLevel == 0 && s.Substring(i, symbols.Length) == symbols)
                    pos = i;
            }*/

        public static IOperation ParseFrom(string s)
        {
            string left = "", right = "";

            if (onLevel(s, "=", 0))
            {
                MISC.separate(s, "=", ref left, ref right, lastIndex);
                if (left.Length > 0)
                    switch (left[left.Length - 1])
                    {
                        case '+':
                            return new Assum(ParseFrom(left.Remove(left.Length - 1)), new Summ(ParseFrom(left.Remove(left.Length - 1)), ParseFrom(right)));
                        case '-':
                            return new Assum(ParseFrom(left.Remove(left.Length - 1)), new Diff(ParseFrom(left.Remove(left.Length - 1)), ParseFrom(right)));
                        case '*':
                            return new Assum(ParseFrom(left.Remove(left.Length - 1)), new Mult(ParseFrom(left.Remove(left.Length - 1)), ParseFrom(right)));
                        case '/':
                            return new Assum(ParseFrom(left.Remove(left.Length - 1)), new Qout(ParseFrom(left.Remove(left.Length - 1)), ParseFrom(right)));
                        default:
                            break;
                    }
                return new Assum(ParseFrom(left), ParseFrom(right));
            }

            if (onLevel(s, "==", 0))
            {
                MISC.separate(s, "==", ref left, ref right, lastIndex);
                return new Equal(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, "<=", 0))
            {
                MISC.separate(s, "<=", ref left, ref right, lastIndex);
                return new LsEq(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, ">=", 0))
            {
                MISC.separate(s, ">=", ref left, ref right, lastIndex);
                return new MrEq(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, "<", 0))
            {
                MISC.separate(s, "<", ref left, ref right, lastIndex);
                return new Less(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, ">", 0))
            {
                MISC.separate(s, ">", ref left, ref right, lastIndex);
                return new More(ParseFrom(left), ParseFrom(right));
            }

            if (onLevel(s, "+", 0))
            {
                MISC.separate(s, "+", ref left, ref right, lastIndex);
                return new Summ(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, "-", 0))
            {
                MISC.separate(s, "-", ref left, ref right, lastIndex);
                if (left.Length == 0)
                    return MonoOperation.ParseFrom("-" + right);
                return new Diff(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, "*", 0))
            {
                MISC.separate(s, "*", ref left, ref right, lastIndex);
                return new Mult(ParseFrom(left), ParseFrom(right));
            }
            if (onLevel(s, "/", 0))
            {
                MISC.separate(s, "/", ref left, ref right, lastIndex);
                return new Qout(ParseFrom(left), ParseFrom(right));
            }
            if (s.IndexOf('(') == 0 && s.LastIndexOf(')') == s.Length - 1)
                return ParseFrom(MISC.breakBrackets(s));
            try
            {
                return UniqOperation.ParseFrom(s);
            }
            catch (Exception e)
            {
                return MonoOperation.ParseFrom(s);
            }

           
        }
        public ValueType returnTypes()
        {
            return returnType;
        }
    }
}
/*
           if (symbols == "=" || symbols == "+" || symbols == "-" || symbols == "<" || symbols == ">")
           {
               for (int i = 1; i < s.Length - 1; i++)
                   if (((s[i] == '=' || s[i] == '<' || s[i] == '>') && s[i - 1] != '=' && s[i + 1] != '=')
                       || ((s[i] == '+' || s[i] == '-') && s[i + 1] != '=' && s[i - 1] != '+' && s[i - 1] != '-' && s[i + 1] != '+' && s[i + 1] != '-'))
                   { pos = i; break; }
           }
           else
           {
               pos = s.IndexOf(symbols);   // if it is double symbol or somthing else
           }*/