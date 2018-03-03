 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Images
{
    public class Sinogram : IDisplayable
    {
        public Bitmap Bitmap { private set; get; }

        public long IterationAmount { set; get; }
        private double Alfa { set; get; }

        public Sinogram(BaseImage image)
        {
            DoRandonTransform(image);
        }

        public void Display()
        {
            throw new NotImplementedException();
        }

        private void DoRandonTransform(BaseImage image)
        {
            throw new NotImplementedException();
        }
    }
}
