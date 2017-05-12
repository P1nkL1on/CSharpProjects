using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public interface IBuff
    {
        /// <summary>
        /// Шанс наложения (условия там разные)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        float ChanceOfApply(IStatable target);
        /// <summary>
        /// Эффект при наложении на юнита
        /// </summary>
        /// <param name="target"></param>
        void EffectStart(Unit target);
        /// <summary>
        /// Эффект, который срабатывает каждый ход
        /// </summary>
        /// <param name="target"></param>
        void EffectTick(Unit target);
        /// <summary>
        /// Эфффект при спадении бафа
        /// </summary>
        /// <param name="target"></param>
        void EffectFinish(Unit target);
        /// <summary>
        /// Описание баффа
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Ссылка на того, кто наложил этот бафф
        /// </summary>
        Unit Sourse { get; }
    }


    public class PeriodicalDamage : IBuff
    {
        sbyte turnLong;
        float CurrentDamage;
        float acs;
        Element elem;
        Unit sourse;
        /// <summary>
        /// Кто то нанесет тебе урон за Х ходов, начальный урон будет Д, будет увеличиваться в А раз каждый ход
        /// </summary>
        /// <param name="Sourse"></param>
        /// <param name="turns"></param>
        /// <param name="MaxTickDamage"></param>
        /// <param name="Acsellerator"></param>
        public PeriodicalDamage(Unit Sourse, Element elem, sbyte turns, float MaxTickDamage, float Acsellerator)
        {
            this.elem = elem;
            sourse = Sourse;
            turnLong = turns;
            acs = Acsellerator;
            CurrentDamage = MaxTickDamage;
        }
        /// <summary>
        /// Кто то нанесет тебе Д урона за Х ходов
        /// </summary>
        /// <param name="Sourse"></param>
        /// <param name="turns"></param>
        /// <param name="TotalDamage"></param>
        public PeriodicalDamage(Unit Sourse, Element elem, sbyte turns, float TotalDamage)
        {
            this.elem = elem;
            sourse = Sourse;
            turnLong = turns;
            acs = 1;
            CurrentDamage = TotalDamage / turns;
        }
        /// <summary>
        /// Ты самовозгорелся, балда. Получишь Д урона за Х ходов
        /// </summary>
        /// <param name="turns"></param>
        /// <param name="TotalDamage"></param>
        public PeriodicalDamage(sbyte turns, float TotalDamage)
        {
            sourse = null;
            turnLong = turns;
            acs = 1;
            CurrentDamage = TotalDamage / turns;
        }/// <summary>
        /// Ты самовозгорелся, балда. Получишь Д урона за Х ходов
        /// </summary>
        /// <param name="turns"></param>
        /// <param name="TotalDamage"></param>
        public PeriodicalDamage(Element elem, sbyte turns, float TotalDamage)
        {
            sourse = null;
            this.elem = elem;
            turnLong = turns;
            acs = 1;
            CurrentDamage = TotalDamage / turns;
        }

        public float ChanceOfApply(IStatable target)
        {
            //more AGL RET STB you have, more chance to dodge this shit
            return
                (float)(90 * Math.Pow(2.7, -(target.Aglity + target.Reticence + target.Stability)));
        }
        public void EffectStart(Unit target)
        {
            // do nothing
        }
        public void EffectFinish(Unit target)
        {
            // do nothing
        }
        public void EffectTick(Unit target)
        {
            target.TakeDamage(new SimpleHit(CurrentDamage, elem));
            CurrentDamage *= acs;
        }
        /// <summary>
        /// Описание баффа
        /// </summary>
        public string Description
        {
            get
            {
                return String.Format("Unit is on fire. He will recieve {0} {2} dmg. per turn. Lasts {1} turns.", CurrentDamage, turnLong, elem);
            }
        }
        /// <summary>
        /// Ссылка на того, кто наложил этот бафф
        /// </summary>
        public Unit Sourse { get { return sourse; } }
    }
}
