using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public class Unit : IStatable, IHitable, IHealthMana
    {
        
        private sbyte[] stats = new sbyte[9];
        float hp; float hpmax;
        float mp; float mpmax;
        List<IWeapon> weaponEquipped;
        List<IArmor> armorEquipped;
        sbyte level = 1;

        public Unit(HeroLoadOuts loadout)
        {
            hp = hpmax = 1; mp = mpmax = 1;         //helth and mana standart
            stats = loadout.stats;                  //factrory methods
            weaponEquipped = loadout.weaponEquipped;
            armorEquipped = loadout.armorEquipped;
            RecalculateStats();                     //recalculate after computing stats
        }

        void RecalculateStats()
        {
            double koe = hp / hpmax;
            hpmax = Math.Min((float)Math.Round(60 + Math.Pow(2, level)),
                Math.Max(1, 10 + (float)Math.Round(Math.Pow(1.15, (Vitality + .2f * Stability - .5f * Reticence - .2f * Intelligence - .2f * Aglity)))));
            hp = (float)(hpmax * koe);
            koe = mp / mpmax;
            mpmax = Math.Min((float)Math.Round(60 + Math.Pow(2, level)),
                Math.Max(1, 10 + (float)Math.Round(Math.Pow(1.25, (Erudition + .2f * Intelligence - .8f * Might)))));
            mp = (float)(mpmax * koe);
        }

        public void AddStat(int whatStat, sbyte number)
        {
            level += number;
            stats[Math.Max(0, Math.Min(stats.Length - 1, whatStat))] += number;
            RecalculateStats();
        }

        public string StatsToString()
        {
            string res = "STATS:\n";
            res += String.Format("HP: {0}/{1}",hp,hpmax).PadLeft(15)+"  "+String.Format("MP: {0}/{1}",mp,mpmax).PadRight(15)+"\n";
            for (int i = 0; i < 9; i++)
            {
                res += string.Format("  {0}: {1}", Help.statsName[i], stats[i]).PadRight(10);
                if (i % 3 == 2) res += "\n";
            }

            return res;
        }

        #region statsPropertyes
        /// <summary>
        /// Параметр жизненной силы
        /// </summary>
        public sbyte Vitality { get { return stats[0]; } }
        /// <summary>
        /// Ловкость
        /// </summary>
        public sbyte Aglity { get { return stats[1]; } }
        /// <summary>
        /// Скорость и проворность
        /// </summary>
        public sbyte Velocity { get { return stats[2]; } }
        /// <summary>
        /// Укрепленность, жосткость, баланс
        /// </summary>
        public sbyte Stability { get { return stats[3]; } }
        /// <summary>
        /// Физическая мощь
        /// </summary>
        public sbyte Might { get { return stats[4]; } }
        /// <summary>
        /// Разум и сообразительность
        /// </summary>
        public sbyte Intelligence { get { return stats[5]; } }
        /// <summary>
        /// Образование
        /// </summary>
        public sbyte Erudition { get { return stats[6]; } }
        /// <summary>
        /// Харизма
        /// </summary>
        public sbyte Charisma { get { return stats[7]; } }
        /// <summary>
        /// Скрытность
        /// </summary>
        public sbyte Reticence { get { return stats[8]; } }
        #endregion
        #region HealthMana
        /// <summary>
        /// Максимальное количество жизней
        /// </summary>
        public float HealthMax
        { get { return hpmax; } }
        /// <summary>
        /// Текущее количество жизней
        /// </summary>
        public float HealthCurrent
        { get { return hp; } }
        /// <summary>
        /// Максимальное количество маны
        /// </summary>
        public float ManaMax
        { get { return mpmax; } }
        /// <summary>
        /// Текущее количество маны
        /// </summary>
        public float ManaCurrent
        { get { return mp; } }
        public void ChangeHealth(float number)
        { hp += number; }
        public void ChanceMana(float number)
        { mp += number; }
        #endregion
        #region chanceHits
        /// <summary>
        /// Шанс того, что получающий урон сможет его заюлокировать
        /// </summary>
        public float chanceOfBlock
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Шанс того, что получающий урон сможет его отдоджить
        /// </summary>
        public float chanceOfDodge
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Шанс того, что после получения урона объект контр атакует
        /// </summary>
        public float chanceCounterAttack
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Объект принимает удар
        /// </summary>
        /// <param name="hit">Удар</param>
        public void TakeDamage(IHit hit)
        {
            ChangeHealth(hit.Damage);
        }
        #endregion
    }
}
