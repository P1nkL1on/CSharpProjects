using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace DungStruct
{

    public enum GroundType
    {
        none = -1,
        unknown = 0,
        earth = 1,
        dirt = 2,
        stone = 3,
        tile = 4,
        grass = 5,
        grassless = 6
    }


    struct FieldTile : IDrawable
    {
        GroundType type;
        Bitmap img;

        public FieldTile(GroundType gt, Random rnd)
        {
            this.type = gt;
            string adress = "../Assets/Sprites/GroundTiles/tile_" + gt.ToString(),
                   default_adress = "../Assets/Sprites/GroundTiles/tile_default0.PNG";
            try
            {
                img = new Bitmap(adress + rnd.Next(3).ToString() + ".PNG");
            }
            catch (Exception e)
            {
                img = new Bitmap(default_adress);
            }
        }

        public void Draw(Graphics G, DrawMeta how)
        {
            G.DrawImage(
                img,
                how._where.X - (int)(10 * how._scale),
                how._where.Y - (int)(10 * how._scale),
                (int)(20 * how._scale),
                (int)(20 * how._scale));

        }
    }

    public struct Field
    {

        FieldTile[,] tiles;

        public Field(int width, int height)
        {
            Random rnd = new Random();
            GroundType[] ables = new GroundType[3] {GroundType.earth, GroundType.grassless, GroundType.grass };

            tiles = new FieldTile[width, height];
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j] = new FieldTile(ables[Math.Min(ables.Length- 1, rnd.Next((int)(i * i + j * j)) / 2000)], rnd);
                }
        }

        public void Draw(Graphics G, DrawMeta how)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i, j].Draw(G, new DrawMeta(
                        new Point((int)((-tiles.GetLength(0) / 2 + i) * 20 * how._scale),
                                  (int)((-tiles.GetLength(1) / 2 + j) * 20 * how._scale)),
                        how));
        }

    }
}
