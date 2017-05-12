using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public abstract class HeroLoadOuts
    {

        public abstract sbyte[] stats { get; }
        public abstract List<IWeapon> weaponEquipped { get; }
        public abstract List<IArmor> armorEquipped { get; }

        //public static sbyte[] KnightStats
        //{
        //    get
        //    {
        //        return new sbyte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //    }
        //}

    }
    public class PoormanLoadout : HeroLoadOuts
    {
        public override sbyte[] stats
        {
            get { return new sbyte[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 }; }
        }
        public override List<IWeapon> weaponEquipped
        {
            get
            {
                List<IWeapon> weapons = new List<IWeapon>();
                weapons.Add(new Dubinka());
                return weapons;
            }
        }
        public override List<IArmor> armorEquipped
        {
            get
            {
                List<IArmor> armors = new List<IArmor>();
                armors.Add(new TypicalShelm());
                return armors;
            }
        }
    }
    public class KnightLoadout : HeroLoadOuts
    {
        public override sbyte[] stats
        {
            get { return new sbyte[9] { 10, 2, 2, 10, 6, 2, 2, 2, 0 }; }
        }
        public override List<IWeapon> weaponEquipped
        {
            get
            {
                List<IWeapon> weapons = new List<IWeapon>();
                weapons.Add(new ShortSword());
                return weapons;
            }
        }
        public override List<IArmor> armorEquipped
        {
            get
            {
                List<IArmor> armors = new List<IArmor>();
                armors.Add(new TypicalShelm()); 
                armors.Add(new TypicalPlateMael());
                armors.Add(new TypicalBoots());
                armors.Add(new TypicalGloves());
                return armors;
            }
        }
    }
}
