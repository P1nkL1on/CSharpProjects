using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WawkiClasses
{
    static class MouseClickAdapter
    {

        public static Point Tranform (int X, int Y){
            Point result = new Point(0, 0);

            int XStep = (int)(Drawing.Width / 8.0);
            int YStep = (int)(Drawing.Height / 8.0);

            result.X = (X - Drawing.StartPoint.X) / XStep;
            result.Y = (Y - Drawing.StartPoint.Y) / YStep;

            return result;
        }

    }
}
