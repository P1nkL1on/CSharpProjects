using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularLib
{
    public static class Consts
    {
        public static byte _colorIntensive = 255;
        public static byte _numberOfPlayers = 3;

        public static float _bornHealth = 100f;
        public static float _bornPower = 50f;
        public static float _maxHealth = 100f;
        public static float _maxPower = 200f;


        public static float _dyingTickHealth = .3f;
        public static float _dyingTickPower = .01f;

        public static float _killRewardHealth = 50;
        public static float _killRewardPower = 4;
    }
}
