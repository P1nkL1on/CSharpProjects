using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace MatveevV2
{
    class MatGen
    {
        List<Word> text;
        List<Word> seps;

        //public List<string> success;
        List<string> words;
        string[] matvv;

        public List<int> usedIndexes;// = new List<int>();

        public MatGen()
        {
            words = new List<string>();
            matvv = new string[]{"...","ээ","ээээ","как бы","откуда","когда","где","почему","что","кто","из-за чего","это","как что","где может","может ли быть",
                                 "а у меня","я вот видел","вот","так","то есть","наверно","наверное","здесь","тут","насчет","от","до","вот так","ведь","точно","точно и, что","...эээ...",
                                 "и","а","но","всмысле","о","при","после"};
            List<int> usedIndexes = new List<int>();
        }

        public string Generate()
        {
            Random rnd = new Random(DateTime.Now.Millisecond + DateTime.Now.Second * 1000);
            usedIndexes = new List<int>();

            usedIndexes.Add(rnd.Next(matvv.Length));   // matveev == +, words == -

            string question = matvv[usedIndexes[0]] + " ";

            for (int i = 1, r = rnd.Next(5, 10); i < r; i++)
            {
                int cas = rnd.Next(5), prev = usedIndexes[usedIndexes.Count - 1];
                List<float> chances = new List<float>();

                //question += text[rnd.Next(text.Count)].Str;
                if (cas < 2)
                {
                    if (prev < 0)
                    {
                        chances.Add(text[0].nicewith[text.Count - 1 + prev]);
                        for (int j = 1; j < text.Count; j++)
                            chances.Add(chances[j - 1] + text[i].nicewith[text.Count - 1 + prev]);
                    }
                    else
                    {
                        chances.Add(seps[0].nicewith[text.Count - 1 + prev]);
                        for (int j = 1; j < text.Count; j++)
                            chances.Add(chances[j - 1] + seps[i].nicewith[text.Count - 1 + prev]);
                    }
                }
                else
                {
                    if (prev < 0)
                    {
                        chances.Add(seps[0].nicewith[text.Count - 1 + prev]);
                        for (int j = 1; j < seps.Count; j++)
                            chances.Add(chances[j - 1] + text[i].nicewith[text.Count - 1 + prev]);
                    }
                    else
                    {
                        chances.Add(seps[0].nicewith[text.Count - 1 + prev]);
                        for (int j = 1; j < seps.Count; j++)
                            chances.Add(chances[j - 1] + seps[i].nicewith[text.Count - 1 + prev]);
                    }
                }



                if (cas < 2)
                {
                    // we have somehing like 3 4 6 10
                    float randomedWord = rnd.Next(Math.Max((int)(chances[chances.Count - 1] * 10.0), 0)) / 10.0f;
                    //
                    int choosenWord = chances.Count - 1;
                    for (int j = 0; j < chances.Count; j++)
                        if (chances[j] > randomedWord)
                        { choosenWord = j; break; }
                    // choosen word
                    usedIndexes.Add(-choosenWord);
                    question += text[choosenWord].Str;
                }
                else
                {
                    // we have somehing like 3 4 6 10
                    float randomedWord = rnd.Next(Math.Max(0, (int)(chances[chances.Count - 1] * 10.0))) / 10.0f;
                    //
                    int choosenWord = chances.Count - 1;
                    for (int j = 0; j < chances.Count; j++)
                        if (chances[j] > randomedWord)
                        { choosenWord = j; break; }
                    // choosen word
                    usedIndexes.Add(choosenWord);
                    question += seps[choosenWord].Str;
                }

                while (question[question.Length - 1] == '\n' || question[question.Length - 1] == '\r')
                    question = question.Remove(question.Length - 2);
                if (i < r - 1)
                    question += " ";
            }

            //question += "?\r\n\r\n";
            //for (int i = 0; i < usedIndexes.Count; i++)
            //    question += "  |  " + usedIndexes[i];
            return question + "?";
        }

        public void AddText(string s)
        {

            Random rnd = new Random(DateTime.Now.Millisecond + DateTime.Now.Second * 1000);
            string[] wordsAdd = s.Split(' ', '.', ',', ';', '!', '?', '-', '\'', '\"');



            for (int i = 0; i < wordsAdd.Length; i++)
                if (wordsAdd[i].Length > 5 || (wordsAdd[i].Length > 1 && rnd.Next(2) == 0))
                    words.Add(wordsAdd[i].ToLower());

            text = new List<Word>();
            seps = new List<Word>();
            text.Add(new Word("...", words.Count + matvv.Length));
            for (int i = 0; i < words.Count; i++)
                text.Add(new Word(words[i], words.Count + matvv.Length));

            for (int i = 0; i < matvv.Length; i++)
                seps.Add(new Word(matvv[i], words.Count + matvv.Length));
        }

        public void DrawGraf(Panel pn)
        {
            using (Graphics g = pn.CreateGraphics())
            {
                g.Clear(Color.White);

                Pen p = new Pen(Color.Black);


                int l = words.Count + matvv.Length;
                for (int i = 0; i < l; i++)
                {
                    PointF pf = calculatePos(i, l);
                    g.DrawEllipse(p, pf.X, pf.Y, 2, 2);
                    for (int j = 0; j < l; j++)
                    {
                        if (i < text.Count && text[i].nicewith[j] != 100.0f)
                        {
                            PointF target = calculatePos(j, l);
                            Pen pnew = new Pen(Color.FromArgb(Math.Max(0, Math.Min(100-(int)text[i].nicewith[j], 255)), 
                                (text[i].nicewith[j] < 100)? 255 : 0, (text[i].nicewith[j] > 100)? 255 : 0, 0));
                            g.DrawLine(pnew, pf.X + 1, pf.Y + 1, target.X + 1, target.Y + 1);
                        }
                        if (i >= text.Count && seps[i - text.Count].nicewith[j] != 100.0f)
                        {
                            PointF target = calculatePos(j, l);
                            Pen pnew = new Pen(Color.FromArgb(Math.Max(0, Math.Min(-100 + (int)(seps[i - text.Count].nicewith[j]), 255)),
                                (seps[i - text.Count].nicewith[j] < 100) ? 255 : 0, (seps[i - text.Count].nicewith[j] > 100) ? 255 : 0, 0));
                            g.DrawLine(pnew, pf.X + 1, pf.Y + 1, target.X + 1, target.Y + 1);
                        }
                    }
                }
            }
        }

        static PointF calculatePos(int i, int l)
        {
            float x = (float)(255 + 250.0f * Math.Cos(i * 2.0 / l * Math.PI));
            float y = (float)(255 + 250.0f * Math.Sin(i * 2.0 / l * Math.PI));
            return new PointF(x, y);
        }

        public void Approve(float much)
        {
            Word now = new Word();
            for (int i = 1; i < usedIndexes.Count; i++)
            {
                    if (usedIndexes[i] > 0)
                    {
                        seps[usedIndexes[i]].nicewith[usedIndexes[i - 1] + text.Count - 1] += much;
                    }
                    else
                    {
                        text[-usedIndexes[i]].nicewith[usedIndexes[i - 1] + text.Count - 1] += much;
                    }
            }
        }
    }
}
