using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CellularLib
{
    public class Point : IPoint
    {


        float _health;
        float _power;
        sbyte _team;

        protected void _checkDead()
        {
            if (_health <= 0) { _team = 0; _health = 0; _power = 0; }
        }


        public Point(sbyte team)
        {
            _health = 0;
            _power = 0;
            _team = team;
        }

        public void createRandom(Random seed)
        {
            _health = seed.Next((int)(Consts._bornHealth/2), (int)(Consts._bornHealth));
            _power = seed.Next((int)(Consts._bornPower / 2), (int)(Consts._bornPower));
        }

        public Color getColor
        {
            get
            {
                
                switch (_team)
                {
                    case 1: return Color.FromArgb(0,0,hk());//yellow
                    case -1: return Color.FromArgb(hk(), 0, 0);//red
                    case 2: return Color.FromArgb(0, hk(), 0);//yellow
                    case -2: return Color.FromArgb(hk(), hk(), 0);//red
                    default: break;
                }
                return Color.Black;
            }
        }
        int hk()
        {
            byte brb = Consts._colorIntensive;
            return Math.Min(255,Math.Max(0,(int)((_health/50-1)*brb/2+255-brb/2)));
        }

        public float health { get { return _health; } set { _health = value; } }
        public float power { get { return _power; } set { _power = value; } }
        public sbyte team { get { return _team; } set { _team = value; } }

        public void tick()
        {
            if (_team != 0)
            {
                _health -= Consts._dyingTickHealth;
                _power -= Consts._dyingTickPower; if (_power < 1) power = 1;
                _checkDead();
            }
        }

        public void interractWith(IPoint sosed)
        {
            
            switch (sosed.team)
            {
                case 0:
                    //capture those point
                    if (_health > 0)
                    {
                        sosed.team = _team;
                        sosed.health = _health * .5f; sosed.power = _power * .5f;
                        health *=.5f; power *= .5f;
                    }
                    return;
                default:
                    if (_team == sosed.team)
                    { //friend
                        //return;
                        float hpmax = _health + sosed.health; float pwmax = _power + sosed.power;
                        _health = hpmax * .5f; sosed.health = hpmax * .5f;
                        //_power = pwmax * .5f; sosed.power = pwmax * .5f;
                        break;
                    }
                    //if (_team == -sosed.team)
                    if (_team != sosed.team  || (_team + sosed.team ==0))
                    {
                        float healthReward = Consts._killRewardHealth; float powerReward = Consts._killRewardPower;

                        //fight is coming
                        _health -= sosed.power;
                        sosed.health -= _power;
                        _checkDead();
                        (sosed as Point)._checkDead();
                        //after fight lets watch
                        if (_team == 0 && sosed.team != 0 && sosed.health > 0)
                        {
                            /*ima dead. do nothing*/
                            _team = sosed.team;
                            _health = sosed.health * .5f; _power = sosed.power * .5f;
                            sosed.health *= .5f; sosed.power *= .5f;
                            sosed.health = Math.Min(Consts._maxHealth, sosed.health + healthReward);
                            sosed.power = Math.Min(Consts._maxPower, sosed.power + powerReward);
                        }
                        if (sosed.team == 0 && _team != 0 && _health > 0)
                        {
                            //capture a beaten neightboor
                            sosed.team = _team;
                            sosed.health = _health * .5f; sosed.power = _power * .5f;
                            _health *= .5f; _power *= .5f;
                            _health = Math.Min(Consts._maxHealth, _health + healthReward);
                            _power = Math.Min(Consts._maxPower, _power + powerReward);
                        }
                    }
                    break;
            }
        }


    }
}
