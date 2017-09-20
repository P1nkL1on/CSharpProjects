using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Compilat
{
    public class MISC
    {
        public static ValueType CheckTypeCorrect(TypeConvertion accept, params ValueType[] hadTypes)
        {
            // I D
            // IIB DDB CCB
            for (int i = 0; i < accept.from.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < accept.from[i].Count; j++)
                    if (hadTypes[j] != accept.from[i][j])
                        found = false;
                if (found)
                    return accept.to[i];
            }
            return ValueType.Unknown;
            throw new Exception("DID NOT FOUND");
        }

        static List<string> nowParsing = new List<string>();
        public static void GoDeep(string parseFolder){
            nowParsing.Add(parseFolder);
            DrawIerch();
        }
        public static void GoBack()
        {
            string func = nowParsing[nowParsing.Count - 1];
            if (func.IndexOf("FUNCTION") == 0)
                if (func.IndexOf("$C") > 0 && func.IndexOf("$Cvoid") < 0 && func.IndexOf("R$") < 0)
                    throw new Exception("No return in non-void function \"" + func.Substring(9, func.IndexOf("$",9) - 9) + "\"!");

            nowParsing.RemoveAt(nowParsing.Count - 1);
            DrawIerch();
        }
        public static bool isLast(string ss)
        {
            return (isNowIn(ss) == nowParsing.Count - 1);
        }
        public static string lastFunction()
        {
            for (int i = nowParsing.Count - 1; i >= 0; i--)
                if (nowParsing[i].IndexOf("FUNCTION") == 0)
                    return nowParsing[i];
            return "NONE";
        }
        public static void addReturnToLastFunction()
        {
            for (int i = nowParsing.Count - 1; i >= 0; i--)
                if (nowParsing[i].IndexOf("FUNCTION") == 0)
                    nowParsing[i]+="R$";
        }
        public static int isNowIn(string ss)
        {
            for (int i = nowParsing.Count - 1; i >= 0; i--)
                if (nowParsing[i].IndexOf(ss) >= 0)
                    return i;
            return -1;
        }
        static void DrawIerch()
        {
            return;
            Console.Clear();
            for (int i = 0; i < nowParsing.Count; i++)
                Console.Write("/" + nowParsing[i]);
            Thread.Sleep(50);
        }


        // correct tab problems
        static List<bool> isSlide = new List<bool>();
        static string nowOpen = "│ ";
        static int lastDepth = 0;
        public static bool finish = false;
        static bool rmColomn = false;

        public static string tabs(int depth)
        {
            bool lastChild = finish;
            if (depth != lastDepth)
            {
                if (depth > lastDepth)
                {
                    isSlide.Add(!rmColomn);
                    string nowOpen2 = "";
                    for (int i = 0; i < nowOpen.Length / 2; i++)
                        nowOpen2 += ((isSlide[i])?"│ ":"  ");

                    nowOpen = nowOpen2 + ((lastChild) ? "└ " : "├ ");
                }
                if (depth < lastDepth)
                    for (int i = 0; i < lastDepth - depth; i++)
                    { nowOpen = nowOpen.Remove(nowOpen.Length - 4) + ((lastChild) ? "└ " : "├ "); isSlide.RemoveAt(isSlide.Count - 1); }
                
                lastDepth = depth;
            }
            else
            {
                if (lastChild) nowOpen = nowOpen.Remove(nowOpen.Length - 2) + "└ ";
            }

            rmColomn = (nowOpen[nowOpen.Length - 2] == '└');
            finish = false;
            return nowOpen;
        }
        public static void separate(string S, string separator, ref string leftpart, ref string rightpart, int separatorIndex)
        {
            leftpart = ""; rightpart = "";
            int pos = separatorIndex;
            if (pos == -1)
            { leftpart = S; return; }

            for (int i = 0; i < S.Length; i++)
            {
                if (i < pos)
                    leftpart += S[i];
                if (i >= pos + separator.Length)
                    rightpart += S[i];
            }

            return;
        }
        public static string breakBrackets(string s)
        {
            if (s[0] == '(' && s[s.Length - 1] == ')')
                return s.Substring(1, s.Length - 2);
            return s;
        }

        public static List<string> splitBy(string S, params char[] seps)
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
        public static string getIn(string S, int pos)
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
                if (S[i] == c2) { nowLevel--; if (nowLevel == 0) return res; }
                if (nowLevel >= 1) res += S[i];
                if (S[i] == c) nowLevel++;
            }
            return res; // get operand or commands
        }

        public static int IndexOfOnLevel0(string S, string subS, int from)
        {
            int pos = S.IndexOf(subS, from);
            if (pos < 0)
                return -1;

            int level = 0;
            for (int i = 0; i <= pos; i++)
                level += (S[i] == '(' || S[i] == '{') ? 1 : ((S[i] == ')' || S[i] == '}') ? -1 : 0);
            return (level == 0) ? pos : -1;
        }
    }
}
