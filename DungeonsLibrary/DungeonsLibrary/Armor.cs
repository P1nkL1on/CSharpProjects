using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsLibrary
{
    public interface IArmor
    {

    }

    public interface IShelm : IArmor
    {

    }
    public interface IPlateMael : IArmor
    {

    }
    public interface IGloves : IArmor
    {

    }
    public interface IBoots : IArmor
    {

    }

    public class TypicalShelm : IShelm
    {
        //....
    }
    public class TypicalPlateMael : IPlateMael
    {
        //....
    }
    public class TypicalGloves : IGloves
    {
        //....
    }
    public class TypicalBoots : IBoots
    {
        //....
    }
}
