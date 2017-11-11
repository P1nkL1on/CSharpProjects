using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Tower
{
    class Viewer
    {

        List<IViewable> items;

        public Viewer()
        {
            items = new List<IViewable>();
        }
        public void AddItem(IViewable it)
        {
            items.Add(it);
        }

        public void Draw(Panel pn)
        {
            Bitmap B = new Bitmap(pn.Width, pn.Height);

            using (Graphics G = Graphics.FromImage(B))
            {
                G.Clear(Color.Black);
                for (int i = 0; i < items.Count; i++)
                    items[i].View(G, pn.Width / 2, pn.Height - 25);
            }

            using (Graphics G = pn.CreateGraphics())
                G.DrawImage(B, 0, 0);
        }

        public void Update()
        {
                for (int i = 0; i < items.Count; i++)
                    items[i].Update();
        }
    }
}
