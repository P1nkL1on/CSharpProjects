using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CellularLib
{
    public class Field : IField
    {

        IPoint[,] _points;
        Bitmap _currentMap;


        void _RecalculateMap()
        {

            for (int i = 0; i < _points.GetLength(0); i++) for (int j = 0; j < _points.GetLength(1); j++)
                    _currentMap.SetPixel(i, j, _points[i, j].getColor);
        }


        public Field(int Width, int Height)
        {
            _points = new IPoint[Width, Height];
            _currentMap = new Bitmap(Width, Height);

        }

        public int Width
        {
            get { return _currentMap.Width; }
        }
        public int Height
        {
            get { return _currentMap.Height; }
        }
        public Bitmap getImage
        {
            get { return _currentMap; }
        }
        public void Randomise(Random seed)
        {

            for (int i = 0; i < _points.GetLength(0); i++) for (int j = 0; j < _points.GetLength(1); j++) _points[i, j] = new Point(0);

            int maxplayers = (Width + Height) / 4;
            for (int i = 0; i < maxplayers; i++)
            {
                int TEAM = 0, j = 0, k = 0;
                if (i >= maxplayers / 2)
                {
                    TEAM = seed.Next(1, 1);
                    if (Consts._numberOfPlayers >= 3) TEAM = seed.Next(1, 3);
                    //for (int k = 0; k < 1; k++) TEAM *= (seed.Next(3) - 1);
                    j = seed.Next(_points.GetLength(1) / 5, _points.GetLength(1) / 5 * 3);
                    k = seed.Next(_points.GetLength(0) / 5, _points.GetLength(1) / 5 * 3);
                }
                else
                {
                    TEAM = -seed.Next(1, 1); //for (int k = 0; k < 1; k++) TEAM *= (seed.Next(3) - 1);
                    if (Consts._numberOfPlayers >= 4) TEAM = -seed.Next(1, 3);
                    j = seed.Next(_points.GetLength(1) / 5 * 2, _points.GetLength(1) / 5 * 4);
                    k = seed.Next(_points.GetLength(0) / 5 * 2, _points.GetLength(0) / 5 * 4);

                }
                _points[k, j] = new Point((sbyte)((TEAM)));
                _points[k, j].createRandom(seed);
            }
        }
        public static float[] teamHealth = new float[5];
        public void Tick(int times, Random seed)
        {

            for (int i = 0; i < 5; i++) teamHealth[i] = 0; float TH = 0;

            sbyte[] TO = new sbyte[] { 1, 0, -1, 0, 0, 1, 0, -1, 1, 1, 1, -1, -1, -1, -1, 1 };
            for (byte time = 0; time < times; time++)
                for (int i = 0; i < _points.GetLength(0); i++) for (int j = 0; j < _points.GetLength(1); j++)
                    {
                        _points[i, j].tick();
                        //attract and capture
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        int dir = seed.Next(8);
                        if (i + TO[dir * 2] >= 0 && i + TO[dir * 2] <= _points.GetLength(0) - 1 &&
                            j + TO[dir * 2 + 1] >= 0 && j + TO[dir * 2 + 1] <= _points.GetLength(1) - 1)
                            _points[i, j].interractWith(_points[i + TO[dir * 2], j + TO[dir * 2 + 1]]);
                        //usuall tick
                        teamHealth[_points[i, j].team + 2] += _points[i, j].health; TH += _points[i, j].health;
                    }
            if (TH != 0) for (int i = 0; i < 5; i++) teamHealth[i] = (float)Math.Round(teamHealth[i] * 10000 / TH) / 100;
            _RecalculateMap();
        }

    }
}
