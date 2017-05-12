using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public static class Help
    {
        public static string[] statsName = new string[9] { "VIT", "AGL", "VLC", "STB", "MGT", "INT", "ERD", "CHR", "RTC" };
        public static string[] statsWide = new string[9] { 
            "vitality- increases maximum health and well-being in general", 
            "aglity- incereases your attack speed, dithriness and sneakiness",
            "velocity- increases your movement speed, step length and jumping quality",
            "stability- increases maximum load and improves ability to stand straight",
            "might- increases your damage and threateningness", 
            "intelligence- increases queality of your spells and mind bindings",
            "erudition- increases maximum mana pool and breadth of views on the world",
            "charisma- increases your ability to stay in the community and negotiate with bandits", 
            "reticence- increases your ability to stay in the shade and not make noise" };


        public static string AnswerQuestion(string question)
        {
            if (question.Length <= 0) return "Type a question, traveller.";
            if (question[question.Length - 1] != '?') return "There is no question, traveller.";
            for (int i = 0; i < statsName.Length; i++)
                if (question.ToUpper().Equals(statsName[i]+"?")) return string.Format("<{0}>\n  -{1};",statsName[i],statsWide[i]);
            if (question.ToUpper().Equals("STATS?"))
            { string res = ""; for (int i = 0; i < statsName.Length; i++)res += String.Format("\n<{0}>-{1};", statsName[i], statsWide[i]); return res; }

            return "???";
        }
    }
}
