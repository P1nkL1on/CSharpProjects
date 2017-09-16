using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilat
{
    public class MISC
    {
        public static string tabs (int depth){
            string res = "";
            for (int i = 0; i < depth; i++)
                res += "  ";
            return res + ((depth == 0) ? "  " : "├─");
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
    }
}
