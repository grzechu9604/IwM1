using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PierwszyProjekt.Images
{
    public interface IDisplayable
    {
        // nie powinno być typu void ale na tę chwilę niech będzie żeby była zaślepka
        // niech sobie będzie void
        void Display(PictureBox pictureBox);
    }
}
