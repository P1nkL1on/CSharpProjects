using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WawkiClasses
{
    public abstract class Figure
    {
        public abstract FigureState State { get; set; }
        public abstract Point Position { get; set; }
        public abstract sbyte Team { get; }
        public abstract bool CanMoveTo(Point moveTo);
    }

    public class Wawka : Figure
    {

        Point position;
        FigureState state;
        sbyte team;

        public Wawka()
        {
            position = new Point(0, 0);
            state = FigureState.usuall;
            team = 0;
        }

        public Wawka(sbyte team)
        {
            position = new Point(0, 0);
            state = FigureState.usuall;
            this.team = team;
        }

        public Wawka(sbyte team, Point position)
        {
            this.position = position;
            state = FigureState.usuall;
            this.team = team;
        }
        public Wawka(sbyte team, Point position, bool damka)
        {
            this.position = position;
            state = FigureState.usuall;
            if (damka) state = FigureState.damka;
            this.team = team;
        }
        //свойства

        public override Point Position
        {
            get { return position; }
            set
            {
                if (value.X >= 0 && value.Y <= 7 && value.Y >= 0 && value.Y <= 7)
                    position = value;
                else
                    throw new WException("Invalid wawka position!");
            }
        }

        public override FigureState State
        {
            get { return state; }
            set
            {
                if (state == FigureState.damka && value == FigureState.usuall)
                    throw new WException("Cannot become usuall from damka!!!");
                else
                    state = value;
            }
        }

        public override sbyte Team
        {
            get { return team; }
        }

        //методы

        public override bool CanMoveTo(Point moveTo)
        {
            if (moveTo.X >= 0 && moveTo.Y <= 7 && moveTo.Y >= 0 && moveTo.Y <= 7)
            {
                int deltaX = Math.Abs(moveTo.X - position.X);
                int deltaY = Math.Abs(moveTo.Y - position.Y);

                //for damka \ usuall
                if (state == FigureState.damka && deltaX == deltaY && deltaX != 0) return true;
                if (state == FigureState.usuall && deltaX == deltaY && deltaX == 1) return true;

            }
            else throw new WException("Out of field position!");

            return false;
        }
    }

    public enum FigureState
    {
        usuall = 0,
        damka = 1,
        dead = -1
    }
}
