using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace DungStruct
{
    public class Manager
    {

        Field ff;
        public Manager()
        {
            ff = new Field(100, 100);
        }

        public void Draw(Panel panel)
        {
            int wid = panel.Width, hei = panel.Height;
            Bitmap bt = new Bitmap(wid, hei);

            using (Graphics G = Graphics.FromImage(bt))
                ff.Draw(G, new DrawMeta(new Point(wid / 2, hei / 2), 0.0f, .5f));

            using (Graphics G = panel.CreateGraphics())
                G.DrawImage(bt, 0, 0);
        }

    }
}
