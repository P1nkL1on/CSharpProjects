using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Data;

namespace WawkiClasses
{

    abstract class Player
    {
        public abstract sbyte Team { get; }
        public abstract bool Active { get; set; }
        public abstract bool IsBot { get; }

        public abstract void RandomTurn(GameBotView game, out Figure who, out Point where);
    }
    class WawkiPlayer : Player
    {
        sbyte team;
        bool active;
        bool bot;

        public WawkiPlayer(sbyte team, bool bot)
        {
            this.team = team;
            active = false;
            this.bot = bot;
        }

        public override sbyte Team
        {
            get { return team; }
        }
        public override bool IsBot
        {
            get { return bot; }
        }
        public override bool Active
        {
            get { return active; }
            set
            {
                active = value;
            }
        }

        public override void RandomTurn(GameBotView game, out Figure who, out Point where)
        {
            int canBeSelected = 0;
            for (int i = 0; i < game.CurrentField.FigureCount; i++)
                if (game.CurrentField.GetFigureBy(i).Team == game.CurrentPlayerTeam) canBeSelected++;
            if (canBeSelected > 0)
            {

            ReRoll:
                Random rnd = new Random(DateTime.Now.Millisecond); Figure figureSelected;
                List<Point> whereCanWeGo = new List<Point>(); int tr = 0;
                do
                {
                    tr++;
                    figureSelected = game.CurrentField.GetFigureBy(rnd.Next(game.CurrentField.FigureCount));
                    whereCanWeGo = game.WhereToGo(figureSelected);
                } while ((whereCanWeGo.Count <= 1 && tr<100) || figureSelected.Team != game.CurrentPlayerTeam);
                //select a key
                Point goTo = new Point(-1, -1); int tryes = 0;
                do
                {
                    tryes++;
                    if (whereCanWeGo.Count <= 0) goto ReRoll;
                    goTo = whereCanWeGo[0 + rnd.Next(whereCanWeGo.Count - 0)];
                }
                while ((goTo.X < 0 || goTo.Y < 0 || goTo.X > 7 || goTo.Y > 7) && tryes < 100);
                if (goTo.X < 0 || goTo.Y < 0 || goTo.X > 7 || goTo.Y > 7)
                    goto ReRoll;
                //goTo = figureSelected.Position;


                game.SelectedPointsSet = whereCanWeGo;
                who = figureSelected;
                where = goTo;

                return;
            }
            //if there is NO wawkis
            who = null;
            where = new Point(-1, -1); return;
        }
    }
}
