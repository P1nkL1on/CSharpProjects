using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public interface IStatable
    {
        /// <summary>
        /// Параметр жизненной силы
        /// </summary>
        sbyte Vitality { get; }
        /// <summary>
        /// Ловкость
        /// </summary>
        sbyte Aglity { get; }
        /// <summary>
        /// Скорость и проворность
        /// </summary>
        sbyte Velocity { get; }
        /// <summary>
        /// Укрепленность, жосткость, баланс
        /// </summary>
        sbyte Stability { get; }
        /// <summary>
        /// Физическая мощь
        /// </summary>
        sbyte Might { get; }
        /// <summary>
        /// Разум и сообразительность
        /// </summary>
        sbyte Intelligence { get; }
        /// <summary>
        /// Образование
        /// </summary>
        sbyte Erudition { get; }
        /// <summary>
        /// Харизма
        /// </summary>
        sbyte Charisma { get; }
        /// <summary>
        /// Скрытность
        /// </summary>
        sbyte Reticence { get; }
    }

    public interface IHitable
    {
        /// <summary>
        /// Шанс того, что получающий урон сможет его заюлокировать
        /// </summary>
        float chanceOfBlock { get; }
        /// <summary>
        /// Шанс того, что получающий урон сможет его отдоджить
        /// </summary>
        float chanceOfDodge { get; }
        /// <summary>
        /// Шанс того, что после получения урона объект контр атакует
        /// </summary>
        float chanceCounterAttack { get; }
        /// <summary>
        /// Объект принимает удар
        /// </summary>
        /// <param name="hit">Удар</param>
        void TakeDamage(IHit hit);
    }

    public interface IHealthMana
    {
        float HealthMax { get; }
        float HealthCurrent { get; }
        float ManaMax { get; }
        float ManaCurrent { get; }
        void ChangeHealth(float number);
        void ChanceMana(float number);
    }
}
