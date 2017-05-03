using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WawkiClasses
{
    public static class Drawing
    {
        static Point startPoint = new Point(30, 60);
        static int GridCount = 8;
        static int fieldWidth = 800;
        static int fieldHeight = 800;
        static Color[] colors = new Color[] { Color.Yellow, Color.Orange, Color.White, Color.Black };
        static Color backGroundColor = Color.LightGray;



        public static Point StartPoint { get { return startPoint; } }
        public static int Width { get { return fieldWidth; } }
        public static int Height { get { return fieldHeight; } }



        public static void ScreenField(GameField field)
        {
            field.ScreenShot = drawField(field);
        }

        static Bitmap drawField(GameField field)
        {

            Bitmap res = new Bitmap(fieldWidth + 2 * startPoint.X, fieldHeight + 2 * startPoint.Y);
            using (Graphics g = Graphics.FromImage(res))
            {
                //clear back
                g.Clear(backGroundColor);
                //draw a grid
                drawGrid(g);
                //drawing a figures
                for (int i = 0; i < field.FigureCount; i++)
                    drawFigure(field.GetFigureBy(i), g);
            }
            return res;
        }

        static void drawGrid(Graphics where)
        {
            int stepX = (int)(fieldWidth / (GridCount * 1.0));
            int stepY = (int)(fieldHeight / (GridCount * 1.0));

            for (int i = 0; i < GridCount; i++) for (int j = 0; j < GridCount; j++)

                    where.FillRectangle(new SolidBrush(colors[((i + j) % 2)]),
                              startPoint.X + i * stepX, startPoint.Y + j * stepY, stepX, stepY);
        }

        static void drawFigure(Figure fig, Graphics where)
        {
            Rectangle rect = new Rectangle(
                startPoint.X + (int)(fieldWidth / (GridCount * 1.0) * fig.Position.X),
                startPoint.Y + (int)(fieldHeight / (GridCount * 1.0) * fig.Position.Y),
                (int)(fieldWidth / (GridCount * 1.0)), (int)(fieldHeight / (GridCount * 1.0))
                );


            where.FillEllipse(new SolidBrush(colors[fig.Team + 1]), rect);
            where.DrawEllipse(new Pen(Color.OrangeRed,5), rect);
            if (fig.State == FigureState.damka)
            {
                int Rad = 60;
                rect = new Rectangle(
                startPoint.X + (int)(fieldWidth / (GridCount * 1.0) * fig.Position.X) + (int)(fieldWidth / (GridCount * 1.0))/2 -Rad/2,
                startPoint.Y + (int)(fieldHeight / (GridCount * 1.0) * fig.Position.Y) + (int)(fieldHeight / (GridCount * 1.0))/2 - Rad/2,
                Rad,Rad);
                where.DrawRectangle(new Pen(Color.OrangeRed), rect);
            }
        }

        static void drawSelected(Graphics where, List<Point> selectedPoitns)
        {
            int Rad = 40;
            int stepX = (int)(fieldWidth / (GridCount * 1.0));
            int stepY = (int)(fieldHeight / (GridCount * 1.0));

            for (int i = 0; i < selectedPoitns.Count; i++)
                where.DrawEllipse(new Pen(Color.Red, 8),
                          startPoint.X + selectedPoitns[i].X * stepX + stepX / 2 - Rad / 2,
                          startPoint.Y + selectedPoitns[i].Y * stepY + stepY / 2 - Rad / 2, Rad, Rad);
        }

        static void DrawPlayerWhoTurns(Graphics where, int side /*-1||+1*/)
        {
            where.FillRectangle(new SolidBrush(Color.YellowGreen),
                new Rectangle(0, Height / 2 + startPoint.Y + side*(Height/2+startPoint.Y-5)-5, Width + startPoint.X * 2, 10));
        }

        //~~~~total draw for form execution
        public static void DrawGame(Graphics where, GameController game)
        {
            where.DrawImage(game.currentField.ScreenShot, new Point(0, 0));
            drawSelected(where, game.SelectedPoints);
            DrawPlayerWhoTurns(where, game.CurrentPlayerTeam*2-3);
        }
    }
}
