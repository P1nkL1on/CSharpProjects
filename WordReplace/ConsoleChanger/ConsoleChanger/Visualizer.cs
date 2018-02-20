using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleChanger
{
    struct TEXT
    {
        static
        ConsoleColor foundBackColor = ConsoleColor.Yellow;

        public String text;
        public String fileName;
        public TEXT(String fileName)
        {
            this.fileName = fileName;
            text = "look at my lol horse, my horse is amazing. qwqwqw";
        }
        public List<int> placesOfWords(List<String> words)
        {
            List<int> res = new List<int>();
            for (int i = 0, prevPlace = 0; i < words.Count; i++)
            {
                do
                {
                    int foundPlace = prevPlace = text.IndexOf(words[i], prevPlace + 1);
                    if (foundPlace >= 0)
                    {
                        res.Add(foundPlace); res.Add(words[i].Length);
                    }
                } while (prevPlace != -1);
            }
            return res;
        }
        public void DrawFounds(List<int> founds)
        {
            int eps = 10;
            for (int i = 0, start = 0, finish = 0; i < founds.Count; i+= 2)
            {
                start = Math.Max(0, founds[i] - eps);
                finish = Math.Min(text.Length - 1, founds[i] + founds[i+1] + eps);
                string subS = text.Substring(start, finish - start);
                subS = subS.Replace('\n', ' ');

                Console.Write((fileName+" :").PadRight(20, ' ') + "...");

                Console.Write(subS.Substring(0,founds[i] - start));
                Console.BackgroundColor = foundBackColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(subS.Substring(founds[i] - start, founds[i+1]));
                Console.ResetColor();
                Console.WriteLine(subS.Substring(founds[i+1] + founds[i] - starts)+"...");
            }
        }
    }

    class FOUNDER
    {

        List<TEXT> files;
        List<String> wordsRequest;


        public FOUNDER(List<TEXT> text, List<String> words)
        {
            this.files = text;
            wordsRequest = words;
        }

        
        public void FoundAll()
        {
            foreach (TEXT t in files)
            {
                t.DrawFounds(t.placesOfWords(wordsRequest));
            }
        }
    }
}
