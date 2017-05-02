using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WawkiClasses
{
    public class WException : Exception
    {

        public WException()
        {
        }

        public WException(string message)
            : base(message)
        {
        }
    }


}
