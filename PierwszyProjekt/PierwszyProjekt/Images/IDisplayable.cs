using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Images
{
    public interface IDisplayable
    {
        // nie powinno być typu void ale na tę chwilę niech będzie żeby była zaślepka
        void Display();
    }
}
