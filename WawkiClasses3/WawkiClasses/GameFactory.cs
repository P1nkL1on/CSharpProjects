using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace WawkiClasses
{
    public abstract class GameFactory
    {
        public abstract GameField CreateField();
        public abstract GameField LoadField(List<Figure> figs);
    }

    public class WawkiFactory : GameFactory
    {
        public override GameField CreateField()
        {
            List<Figure> figs = new List<Figure>();
            for (int i = 0; i < 12; i++)
                figs.Add(new Wawka(1, new Point((2 * i) % 8 + Math.Abs(1 - (2 * i) / 8), (2 * i) / 8)));
            for (int i = 0; i < 12; i++)
                figs.Add(new Wawka(2, new Point(1 + (2 * i) % 8 - Math.Abs(1 - (2 * i) / 8), 5 + (2 * i) / 8)));

            WawkiField res = new WawkiField(figs);
            
            return res;
        }
        public override GameField LoadField(List<Figure> figs)
        {
            WawkiField res = new WawkiField(figs);

            return res;
        }
    }

    public class TestFactory : GameFactory
    {
        public override GameField LoadField(List<Figure> figs)
        {

            WawkiField res = new WawkiField(figs);

            return res;
        }
        public override GameField CreateField()
        {
            List<Figure> figs = new List<Figure>();

            figs.Add(new Wawka(2, new Point(0, 7),FigureState.damka));
            for (int i = 1; i < 7; i += 2) for (int j = 0; j <= 2; j+=2 )
                    figs.Add(new Wawka(1, new Point(i,j)));


            WawkiField res = new WawkiField(figs);

            return res;
        }
    }
}
