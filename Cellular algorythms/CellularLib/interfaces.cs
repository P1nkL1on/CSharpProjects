using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CellularLib
{
    public interface IPoint
    {

        /// <summary>
        /// Цвет, которым надо окрасить точку
        /// </summary>
        Color getColor { get; }

        /// <summary>
        /// Взаимодействовать с указанной другой точкой
        /// </summary>
        /// <param name="sosed"></param>
        void interractWith(IPoint sosed);
        /// <summary>
        /// Обычный тик
        /// </summary>
        void tick();

        sbyte team { get; set; }
        /// <summary>
        /// Задать точке случайный характер соответствующий её стандартам
        /// </summary>
        /// <param name="seed"></param>
        void createRandom(Random seed);

        float health { get;  set; }
        float power { get; set; }
    }

    public interface IField
    {
        /// <summary>
        /// Текущее состояние поля
        /// </summary>
        Bitmap getImage { get; }
        /// <summary>
        /// Ширина поля
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Высота поля
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Создать случайно сгенерированное поле
        /// </summary>
        void Randomise(Random seed);
        /// <summary>
        /// Сделать один шаг на этом самом поле
        /// </summary>
        void Tick(int times, Random rnd);
    }
}
