using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatveevV2
{
    struct Word
    {
        string S;
        public float[] nicewith;

        public Word(string S, int much)
        {
            this.S = S;
            this.nicewith = new float[much];
            for (int i = 0; i < much; i++)
                nicewith[i] = 100.0f;
        }

        public void Change(int i, float incr)
        {
            nicewith[i] += incr;
        }

        public string Str
        {
            get { return this.S; }
        }
    }
}
