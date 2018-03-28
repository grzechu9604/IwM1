using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Algorithms
{
    public interface IEmiterGenerator
    {
        List<Emiter> Emiters { get; }
    }
}
