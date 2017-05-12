using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public interface IHit
    {
        IBuff Buff { get; }
        Element HitElement { get; }
        float Damage { get; }
    }

    public class SimpleHit : IHit
    {
        IBuff buff;
        Element elem;
        float dmg;

        public SimpleHit(float dmg, Element elem, IBuff buff)
        {
            this.dmg = dmg; this.elem = elem; this.buff = buff;
        }
        public SimpleHit(float dmg, Element elem)
        {
            this.dmg = dmg; this.elem = elem; this.buff = null;
        }
        public SimpleHit(float dmg)
        {
            this.dmg = dmg; this.elem = Element.none; this.buff = null;
        }

        public IBuff Buff { get { return buff; } }
        public Element HitElement { get { return elem; } }
        public float Damage { get { return dmg; } }
    }

    public enum Element
    {
        none = 0,
        fire = 1,
        frost = 2,
        corrosive = 3,
        electric = 4,
        dark = 5,
        holy = 6,
        toxic = 7,
        magic = 8,
        wind = 9
    }
}
