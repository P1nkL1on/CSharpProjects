using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WawkiClasses
{
    static class FileSys
    {

        //public FileSystem(Game game)
        //{

        //}
        public static void SaveCurrGame(List<Figure> figs, sbyte currentPlayer, string path)
        {
            //string ans = currentPlayer.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(currentPlayer.ToString());
            //sb.AppendFormat("|);
            foreach (Figure f in figs)
            {
                sb.AppendFormat("|{0}/{1}/{2}/{3}", f.Team, f.State, f.Position.X, f.Position.Y);
            }
            FileStream aFile = new FileStream(path, System.IO.FileMode.OpenOrCreate);
            StreamWriter w = new StreamWriter(aFile);
            w.Write(sb.ToString());
            w.Close();
            aFile.Close();
            //currPlayer|player/isDamka(pointX,pointY)|player/isDamka(pointX,pointY)|...

        }
        public static List<Figure> LoadGame(string Path, out sbyte CurrentPlayer)
        {
            CurrentPlayer = 0;
            List<Figure> figs = new List<Figure>();

            FileStream aFile = new FileStream(Path, System.IO.FileMode.Open);
            StreamReader r = new StreamReader(aFile);
            string ans = r.ReadToEnd();
            r.Close();
            aFile.Close();

            string[] arr = ans.Split('|');
            CurrentPlayer = sbyte.Parse(arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                string[] figArr = arr[i].Split('/');
                Wawka wawka = new Wawka(sbyte.Parse(figArr[0]),
                    new System.Drawing.Point(int.Parse(figArr[2]), int.Parse(figArr[3])),
                    (FigureState)Enum.Parse(typeof(FigureState), figArr[1]));
                figs.Add(wawka);
            }
            return figs;
        }
    }
}
