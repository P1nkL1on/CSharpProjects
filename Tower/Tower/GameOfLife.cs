using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower
{
    public class Field
    {
        /// <summary>
        /// Количество точек, которые изменили свое состояние за последний ход
        /// </summary>
        int changed;
        int changed_last;
        /// <summary>
        ///  узор не менялся уже ... ходов
        /// </summary>
        short not_changed_for;
        int turn;
        int height;
        int width;

        // viewing parameters
        int zoom;
        int fromWid;
        int fromHei;

        Point[,] points;

        public int[,] CurrentState()
        {
            int[,] res = new int[points.GetLength(0), points.GetLength(1)];
            for (int i = 0; i < res.GetLength(0); i++)
                for (int j = 0; j < res.GetLength(1); j++)
                    res[i,j] = (points[i, j].Life) ? 1 : 0;
            return res;
        }

        public void set_point(int X, int Y, bool life)
        {
            points[bh(X), bw(Y)] = new Point(life);
        }
        public bool get_point(int X, int Y)
        {
            return points[bh(X), bw(Y)].Life;
        }

        public Field(Random rnd, int heigth, int width, bool random_generate)
        {
            this.fromHei = 0;
            this.fromWid = 0;
            this.zoom = 1;
            this.turn = 0;
            this.changed = 1;
            this.not_changed_for = 0;
            this.height = heigth;
            this.width = width;
            points = new Point[heigth, width];
            // set them all
            for (int i = height / 4; i < height * 3 /4; i++)
                for (int j = width / 4; j < width * 3 / 4; j++)
                    if (!random_generate)
                        points[i, j] = new Point(false);
                    else
                        points[i, j] = new Point((rnd.Next(Math.Abs(i + j - width / 2 - height / 2)) == 0) ? true : false);
        }

        int bw(int X)
        {
            if (X < 0) return width + X;
            if (X >= width) return X - width;
            return X;
        }
        int bh(int X)
        {
            if (X < 0) return height + X;
            if (X >= height) return X - height;
            return X;
        }

        public void UpdateMap()
        {
            turn++; changed_last = changed; changed = 0;
            // calculate
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    points[i, j].Calculate(new Point[] {     points[bh(i-1), bw(j-1)], points[bh(i-1), j], points[bh(i-1), bw(j+1)],
                                                             points[i, bw(j-1)],                           points[i, bw(j+1)],
                                                             points[bh(i+1), bw(j-1)], points[bh(i+1), j], points[bh(i+1), bw(j+1)]});
            // update
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    bool was = points[i, j].Life;
                    points[i, j].Update();

                    if (points[i, j].Life != was)
                        changed++;
                }
        }

        public int Zoom { get { return zoom; } set { zoom = Math.Max(1, value); } }
        public int FromWid { get { return fromWid; } set { fromWid = Math.Max(0, Math.Min(width - 1, value)); } }
        public int FromHei { get { return fromHei; } set { fromHei = Math.Max(0, Math.Min(height - 1, value)); } }

        public bool isDead()
        {
            if (changed_last == changed) not_changed_for++; else not_changed_for = 0;
            return (not_changed_for > 10 || changed == 0);
        }
    }


    struct Point
    {
        short last;
        //public Point[] neight;
        // -1/-1 -1/0 -1/1
        //  0/-1       0/1
        //  1/-1  1/0  1/1
        bool life;
        bool life_become;

        public bool Life { get { return life; } }

        public Point(bool alife)
        {
            this.last = 0;
            this.life = alife;
            this.life_become = this.life;
            //neight = new Point[1];
        }

        public void Calculate(Point[] neight)
        {
            short alife = 0;
            for (int i = 0; i < neight.Length; i++)
                if (neight[i].Life)
                    alife++;
            // if stand steal default
            //life_become = life;
            // if change
            if (!life)
            {
                if (alife == 3)
                    life_become = true;
            }
            else
            {
                if (alife != 2 && alife != 3)
                    life_become = false;
            }
            last = alife;
        }

        public void Update()
        {
            life = life_become;
        }

        public override string ToString()
        {

            if (life)
                return "█";
            else
                return " ";
        }
        public string ToStringCrossed()
        {

            if (life)
                return "▓";
            else
                return "░";
        }

    }
}
