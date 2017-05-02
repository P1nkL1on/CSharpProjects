using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WawkiClasses
{
    class TurnVault
    {

        List<GameField> history;

        public TurnVault()
        {
            history = new List<GameField>();
        }

        public int Count { get { return history.Count; } }

        public void PushTurn(GameField gf)
        {
            history.Add(gf);
        }

        /// <summary>
        /// Стирает все ходы, начиная с хода под номером Index. Начинается с 0.
        /// </summary>
        /// <param name="indexFrom"></param>
        public void RemoveFrom(int indexFrom)
        {
            while (history.Count > indexFrom + 1)
                history.RemoveAt(indexFrom+1);
        }

        public GameField this[int index]
        {
            get {
                //costil
                index = Math.Max(0, Math.Min(history.Count - 1, index));
                return history[index];
            }
        }

        public void Clear()
        {
            history.Clear();
        }

    }
}
