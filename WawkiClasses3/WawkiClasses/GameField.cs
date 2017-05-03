using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WawkiClasses
{
    public abstract class GameField
    {
        public abstract List<Figure> figList { get; }
        public abstract void ClearDead();
        public abstract void AddFigure(Figure fig);
        public abstract Bitmap ScreenShot { get; set; }
        public abstract void ReScreen();
        public abstract Figure WhoIsOn(Point where);
        public abstract Figure WhoIsOn(Point where, figureFilter filter);
        public abstract int FigureCount { get; }
        public abstract Figure GetFigureBy(int index);

        public abstract void MoveFromTo(Point from, Point to);
        public abstract GameField Clone();
    }


    public class WawkiField : GameField
    {

        List<Figure> figures;
        Bitmap screenShot;

        public override int FigureCount { get { return figures.Count; } }

        public override Bitmap ScreenShot
        {
            get { return screenShot; }
            set { screenShot = value; }
        }

        public override List<Figure> figList
        {
            get
            {
                return figures;
            }

        }

        public WawkiField(List<Figure> figures)
        {
            this.figures = figures;
            Drawing.ScreenField(this);
        }

        public override void AddFigure(Figure fig)
        {
            figures.Add(fig);
        }

        public override void ClearDead()
        {
            int l = figures.Count;
            for (int i = 0; i < l; i++)
                if (figures[i].State == FigureState.dead)
                { figures.RemoveAt(i); i--; l--; }
        }

        public override Figure WhoIsOn(Point where /*select all types*/)
        {
            return WhoIsOn(where, fig => true);
        }

        public override Figure WhoIsOn(Point where, figureFilter filter)
        {
            if (figures.Count > 0)
            {

                if (where.X >= 0 && where.Y <= 7 && where.Y >= 0 && where.Y <= 7)
                {
                    for (int i = 0; i < figures.Count; i++)
                    {
                        if (figures[i].Position == where &&
                            figures[i].State != FigureState.dead &&
                            filter(figures[i]))

                            return figures[i];
                    }
                }
                else
                    return null;
                    //throw new WException("Invalid position request!");
            }
            //else no figure to find
            return null;
        }

        public override Figure GetFigureBy(int index)
        {
            if (index > figures.Count - 1 || index < 0)
                throw new WException("Invalid index!");
            else
                return figures[index];
            //return null;
        }


        public override void MoveFromTo(Point from, Point to)
        {
            for (int i = 0; i < figures.Count; i++)
                if (figures[i].Position.X == from.X && figures[i].Position.Y == from.Y)
                { figures[i].Position = to; return; }
        }

        public override GameField Clone()
        {
            List<Figure> fgs = new List<Figure>();
            for (int i = 0; i < figures.Count; i++)
                fgs.Add(new Wawka(figures[i].Team, new Point(figures[i].Position.X,figures[i].Position.Y),figures[i].State/* == FigureState.damka*/));
            return new WawkiField(fgs);
        }

        public override void ReScreen()
        {
            Drawing.ScreenField(this);
        }
    }

    public delegate bool figureFilter(Figure fig);

}
