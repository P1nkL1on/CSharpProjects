using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Tower
{
    class Block
    {
        public static void Draw(Graphics G, float X, float Y, Color clr, int size, bool cont)
        {
            Rectangle rt = new Rectangle((int)(X - size / 2),(int)( Y - size / 2), size, size);

            if (cont) clr = Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B);
            G.FillRectangle(new SolidBrush(clr), rt);
            
        }
    }


    class TowerLevel
    {
        int[,] field;
        


        public TowerLevel(int[,] field)
        {
            this.field = field;

        }

        public void Draw(Graphics G, float dX, float dY, float height, float angle, int size, int maxHei)
        {
            int iM = field.GetLength(0), jM = field.GetLength(1);
            for (int i = 0; i < iM; i++)
                for (int j = 0; j < jM; j++)
                    if (field[i, j] > 0)
                    {
                        float coordX, coordY;
                        coordX = dX + (i - iM / 2) * size- (j - jM / 2) * size * (float)Math.Cos(angle);
                        coordY = dY + (j - jM / 2) * size * (float)Math.Sin(angle) - height * size;

                        Color clr = Color.FromArgb(255, (int)(255.0 / iM * i), (int) (255.0 / jM * j) );
                        double k = (1.0 * height / maxHei);
                        clr = Color.FromArgb((int)(clr.R * k), (int)(clr.G * k), (int)(clr.B * k));

                        Block.Draw(G, coordX, coordY, clr, size, false);
                    }
        }
    }


    class Tower : IViewable
    {
        List<TowerLevel> tl;
        Field ff;
        

        public Tower(Field ff)
        {

            this.ff = ff;

            tl = new List<TowerLevel>();
            //tl.Add(new TowerLevel(new int[,] { { 1, 1, 1 }, { 0, 1, 1 }, { 1, 1, 1 } }, clrs));
            //tl.Add(new TowerLevel(new int[,] { { 1, 1, 0 }, { 1, 1, 0 }, { 1, 0, 1 } }, clrs));
            //tl.Add(new TowerLevel(new int[,] { { 1, 0, 1 }, { 0, 1, 1 }, { 0, 0, 1 } }, clrs));
            //tl.Add(new TowerLevel(new int[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } }, clrs));
        }

        void Draw(Graphics G, float dX, float dY, int size)
        {
            int stFrom = Math.Max(0, tl.Count - (int)dY / size + 30);
            for (int i = stFrom; i < tl.Count; i++)
                tl[i].Draw(G, dX, dY, i - stFrom, (float)(Math.PI * (  1 / 4.0)), size, tl.Count - stFrom);
        }

        public void View(Graphics G, float dX, float dY)
        {
            Draw(G, dX, dY, 8);
        }

        public void Update()
        {
            if (!ff.isDead())
            {
                ff.UpdateMap();
                tl.Add(new TowerLevel(ff.CurrentState()));
            }
            else
            {
                ff = new Field(new Random(), 100, 100, false);
                Random rnd = new Random();
                int W = rnd.Next(20, 80), H = rnd.Next(10, 50);
                
                for (int i = 50 - W/2; i < 50 + W/2; i++)
                    for (int j = 50 - H/2; j <= 50 + H/2; j++)
                        ff.set_point(i, j, true);
            }
        }
    }

    public interface IViewable
    {
        void View(Graphics G, float dX, float dY);
        void Update();
    }
}
