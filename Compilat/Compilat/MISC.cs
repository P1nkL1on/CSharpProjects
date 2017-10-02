﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Compilat
{
    public class MISC
    {
        public static void ConsoleWrite(string S, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.Write(S);
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public static void ConsoleWriteLine(string S, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(S);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static List<Tuple<ValueType, ValueType>> availableConvertation = new List<Tuple<ValueType, ValueType>>();

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
            // we can found some kostils
            if (availableConvertation.Count == 0)
            {
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cint, ValueType.Cdouble));
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cint, ValueType.Cstring));
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cint, ValueType.Cboolean));
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cdouble, ValueType.Cstring));
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cdouble, ValueType.Cboolean));
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cchar, ValueType.Cstring));
                availableConvertation.Add(new Tuple<ValueType, ValueType>(ValueType.Cchar, ValueType.Cboolean));
            }


            // I -> D
            // checking in cyccle
            bool convertionFound = false;
            int[] convertion = new int[hadTypes.Length];
            for (int i = 0; i < convertion.Length; i++)
                convertion[i] = -1;                     // -1 -1 -1 -1 -1 -1 for each variable in signature

            for (int i = 0; i < accept.from.Length; i++)
            {
                bool foundAcceptance = true;
                for (int j = 0; j < hadTypes.Length; j++)
                {
                    bool geted = false;
                    if (hadTypes[j] != accept.from[i][j])
                        for (int k = 0; k < availableConvertation.Count; k++)
                        {
                            if (hadTypes[j] == availableConvertation[k].Item1
                                && accept.from[i][j] == availableConvertation[k].Item2)
                            { geted = true; convertion[j] = k; break; }
                        }
                    else
                        geted = true;
                    // if geted then we have conversion for a current parameter
                    if (!geted)
                        foundAcceptance = false;
                }
                if (foundAcceptance)
                {
                    int n = 0;
                }
            }

            //
            return ValueType.Unknown;
            throw new Exception("DID NOT FOUND");
        }

        static List<string> nowParsing = new List<string>();
        static List<List<int>> levelVariables = new List<List<int>>();
        public static void GoDeep(string parseFolder)
        {
            nowParsing.Add(parseFolder);
            levelVariables.Add(new List<int>());
            DrawIerch();
        }


        public static void GoBack()
        {
            string func = nowParsing[nowParsing.Count - 1];
            if (func.IndexOf("FUNCTION") == 0)
                if (func.IndexOf("$C") > 0 && func.IndexOf("$Cvoid") < 0 && func.IndexOf("R#") < 0)
                    throw new Exception("No return in non-void function \"" + func.Substring(9, func.IndexOf("$", 9) - 9) + "\"!");

            nowParsing.RemoveAt(nowParsing.Count - 1);
            levelVariables.RemoveAt(levelVariables.Count - 1);
            DrawIerch();
        }
        public static void pushVariable(int variableNumber)
        {
            if (levelVariables.Count == 0)
                levelVariables.Add(new List<int>());
            levelVariables[levelVariables.Count - 1].Add(variableNumber);
            DrawIerch();

            return;
        }
        public static bool isVariableAvailable(int variableNumber)
        {
            for (int i = 0; i < levelVariables.Count; i++)
                for (int j = 0; j < levelVariables[i].Count; j++)
                    if (levelVariables[i][j] == variableNumber)
                        return true;
            return false;
        }

        public static bool isLast(string ss)
        {
            return (isNowIn(ss) > 0/*== nowParsing.Count - 1*/);
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
                {
                    if (nowParsing[i].IndexOf("R#") == -1)
                        nowParsing[i] += "R#";
                    return;
                }
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
            {
                Console.Write("\n/" + nowParsing[i] + " :: ");
                for (int j = 0; j < levelVariables[i].Count; j++)
                    Console.Write(" " + levelVariables[i][j]);
            }
            Thread.Sleep(1000);
        }

        public static bool CompareFunctionSignature(ASTFunction f1, ASTFunction f2)
        {
            List<ValueType> lvt1 = f1.returnTypesList();
            List<ValueType> lvt2 = f2.returnTypesList();
            string nam1 = f1.getName, nam2 = f2.getName;
            if (nam1 == nam2 && lvt1.Count == lvt2.Count)
            {
                for (int i = 0; i < lvt1.Count; i++)
                    if (lvt1[i] != lvt2[i])
                        return false;
            }
            else
                return false;
            return true;
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
                        nowOpen2 += ((isSlide[i]) ? "│ " : "  ");

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
            if (c != '(' && c != '{' && c != '[')
                return S;
            if (c == '(') c2 = ')';
            if (c == '{') c2 = '}';
            if (c == '[') c2 = ']';

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
            for (int i = 0; i < S.Length; i++)
            {
                level += (S[i] == '(' || S[i] == '{') ? 1 : ((S[i] == ')' || S[i] == '}') ? -1 : 0);
                if (level == 0 && S.Substring(i).IndexOf(subS) == 0)
                    return i;
            }
            return -1; //(level == 0) ? pos :
        }
    }
}
