using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace PornTitle
{
    public static class Vault
    {

        static String[] NounGirl
            = new String[] { "Деваха", "Дама", "Жещина", "Шлюха", "Шалава", "Шельма", "Глоталка",
                "Блудница", "Шлюшандра","Проститутка","Шмара","Шкура","Соблазнительница","Блондинка",
                "Брюнетка","Шатенка","Азиатка","Шоколадка","Узкоглазая","Желтая, как китайская деревня,",
                "Секретарша","Учительница","Училка", "Распутница","Тощая","Жируха","Потаскуха","Сука",
                "Сучка","Охотница до спермы","Растлительница", "Латинка","Таджичка","Деваря",
                "Инопланетянка","Мексиканка","Лапландтка","Негритянка","Черномазая","Шоколадка","Какаха",
                "Воспитательница","Леди","Блядь","Блядищща","Шлюха по вызову","Девушка по вызову",
                "Шмара легкого поведения","Дама легкоого поведения","Хуесоска"};
        static String[] NounOldGirl
            = new String[] { "Бабуля", "Бабка", "Карга", "Пенсионерка", "Мать тереза за восемьдесят",
                "Баба Яга","Престарелая шалава","Бегемот","Развалюха","Камотозница","Восставшая из могилы",
                "Мамка","Мамаша","Одногодка Ленина", "Жена","Женушка", "Тёща", "Прабабка"};
        static String[] NounYoungGirl
            = new String[] { "Девочка", "Соплежуйка", "Школьница", "Карлица", "Студентка", "Аспирантка",
                "Девственница","Целка","Сама застенчивость","Новенькая","Ботанка","Хулиганка",
                "Староста","Молодуха","Зеленая","Малолетка"};
        static String[] NounBoy
            = new String[] { "Мужик", "Мужлан", "Господин", "Муж", "Членонос", "Араб", "Казах", "Священник",
                "Мулат","Конь","Таджик","Нигер","Негр","Чрнозадый","Макака","Уебан","Директор","Аристократ",
                "Сукан","Палкан","Быдлан","Гопарь","Самец","Альфач","Хач","Китаеза","Япошка","Кореец",
                "Папуас","Колумб","Морячок","Генерал","Солдат","Полковник","Пахан","Ментяра","Банжит",
                "Космонавт","Гонщик","Торчок","Наркоша","Учитель","Преподаватель","Святой отец","Отец",
                "Брат","Сын","Сват","Дядя","Маньячила","Подросток","Пацан","Малыш","Копатель","Дядя Степа","Партнер",
                "Гимнаст","Ученый","Премьер"};
        static String[] VerbFuck
            = new String[] { "Ебет", "Ебурит", "Наяривает", "Обрабатывает", "Чистит", "Трет", "Испытывает",
                "Урабатывает", "Чпокает", "Насаживает", "Вертит", "Лижет","Дрюкает","Оприходывает",
                "Строит", "Хуярит","Пытает","Оккредитирует","Вращает","Заставляет ебаца","Уговаривает",
                "Угощает","Мандрючил","Располовинил","Разглядывает"};
        static String[] VerbCum
            = new String[] { "Глотает", "Принимает на лицо", "Сует в гланды", "Разрабатывает горло",
                "Берет за щеку","Берет подержать","Пытается оттереть от волос", "Пробует на вкус",
                "Сосет","Члемякает","Лижет","Трогает","Закручивает","Наяривает","Ставит по стройке смирно",
                "Трет между булок","Трет между доек","Засасывает","Смотрит на","Не знает как использовать",
                "Шкрябает","Гладит","Требует"};
        static String[] NounDick
            = new String[] { "Хуй", "Член", "Ярило", "Кадило", "Дрочило", "Елда", "Пенис", "Дрын",
                "Лом","Кол","Бревно","Пиструн","Ебало","Яйцо","Алтын","Чендлер","Писюн","Болт","Шланг",
                "Пожарный гидрант","Гигант","Столактит","Бугор","Шишкан","Писос","Сас","Меч"};
        static String[] NounVagina
            = new String[] { "Вагина", "Пилотка", "Пизда", "Дыра", "Щель", "Манда", "Монда", "Пещера",
                "Ворота", "Киска", "Черная дыра", "Писька","Влагалище","Скважина","Хавырка","Кунька","Бздунька",
                "Чуча"};
        static String[] NounOchko
            = new String[] { "Очко", "Зад", "Пердак", "Кольцо", "Шоколадный глаз", "Око Саурона",
                "Задний проход","Анус","Сфинктор","Выхлопная труба","Задний привод","Розетка"};
        static String[] VerbObstacle
            = new String[] { };
        static String[] Predlog
            = new String[] { };
        static String[] Objective
            = new String[]{"Красив","Сексопильн","Обычн","Криклив","Накачан","Толст","Толстозад","Узкожоп",
                "Орущ","Глазаст","Еблив","Охуевш","Необычн","Жестк","Двухметров","Карликов","Низковат",
                "Мощн","Слаб","Волосат","Шикарн","Дерзк","Сочн"
            };
        static String[] VerbObjective = new String[]{};

        static String GetRandomGirl(Random rnd)
        {
            int age = rnd.Next(5);
            switch (age)
            {
                case 0: return GetRandom(NounYoungGirl, rnd);
                case 1: return GetRandom(NounOldGirl, rnd);
                default: return GetRandom(NounGirl, rnd);
            }
        } 
        static String GetRandomObjective(bool female, Random rnd)
        {
            int chance = rnd.Next(101);
            if (chance < 50) return "";
            string res = GetRandom(Objective, rnd);
            if (female) res += "ая"; else res += "ый";
            return res;
        }
        static String GetRandom(String[] arr, Random rnd)
        {
            return arr[rnd.Next(arr.Length)];
        }
        static String Vinit(String slovo)
        {
            if (slovo[slovo.Length - 1] == 'а')
            { slovo = Copy(slovo, 1) + "у"; }

            if (slovo[slovo.Length - 1] == 'я' && slovo[slovo.Length - 2] == 'а')
            { slovo = Copy(slovo, 2) + "ую"; }
            return slovo;
        }

        static String Rodit(String slovo)
        {
            if (slovo[slovo.Length - 1] == 'й' && slovo[slovo.Length - 2] == 'ы')
            { slovo = Copy(slovo, 2) + "ого"; return slovo; }

            if (slovo[slovo.Length - 1] == 'ь')
            { slovo = Copy(slovo, 1) + "я"; return slovo; }

            if (slovo[slovo.Length - 1] != 'а')
                slovo = slovo + "а";
            else
                slovo = Copy(slovo, 1) + "и";

            return slovo;
        }

        static String Copy(String slovo, int minimal)
        {
            String res = "";
            for (int i = 0; i < slovo.Length - minimal; i++)
                res += slovo[i];
            return res;
        }

        public static String Roll()
        {

            Random rnd = new Random();

            int variant = rnd.Next(2);
            switch (variant)
            {
                case 0:
                    string res = String.Format("{0} {1} {2}, её {3} и {4}.",
                        GetRandom(NounBoy, rnd),
                        GetRandom(VerbFuck, rnd).ToLower(),
                        Vinit(GetRandomGirl( rnd)).ToLower(),
                        Vinit(GetRandom(NounVagina, rnd)).ToLower(),
                        Vinit(GetRandom(NounOchko, rnd)).ToLower());
                    string obj = GetRandomObjective(false, rnd);
                    if (obj.Length > 0) return obj + " " + res.ToLower(); else return res;
                case 1:
                    res = String.Format("{0} {1} {2} {3}.",
                        GetRandomGirl( rnd),
                        GetRandom(VerbCum, rnd).ToLower(),
                        Vinit(GetRandom(NounDick, rnd)).ToLower(),
                        Rodit(GetRandom(NounBoy, rnd)).ToLower());
                    obj = GetRandomObjective(true, rnd);
                    if (obj.Length > 0) return obj + " " + res.ToLower(); else return res;
                default: break;
            }
            return "...";
        }
    }
}
