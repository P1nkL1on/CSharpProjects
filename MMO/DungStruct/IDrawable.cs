using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace DungStruct
{
    public struct DrawMeta{
        public Point _where;
        /// <summary>
        /// In Rad (-Pi .. + Pi)
        /// </summary>
        public float _rotation; // in rad
        public float _scale;

        public DrawMeta(Point _where, float _rotation, float _scale)
        {
            this._where = _where;
            this._rotation = _rotation;
            this._scale = _scale;
        }

        public DrawMeta(Point offset, DrawMeta parentMeta)
        {
            this._scale = parentMeta._scale;
            this._rotation = parentMeta._rotation;
            this._where = new Point(parentMeta._where.X + offset.X, parentMeta._where.Y + offset.Y);
        }
    }


    public interface IDrawable
    {
        void Draw(Graphics G, DrawMeta how);
    }
}
