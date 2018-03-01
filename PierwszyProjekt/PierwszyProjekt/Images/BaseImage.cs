using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Images
{
    public class BaseImage : IDisplayable
    {
        public Bitmap Bitmap { private set; get; }

        public BaseImage(string filePath)
        {
            Bitmap = new Bitmap(filePath);
        }

        public BaseImage(Sinogram sinogram)
        {
            DoReversedRandonTransform(sinogram);
        }

        public void Display()
        {
            throw new NotImplementedException();
        }

        private void DoReversedRandonTransform(Sinogram sinogram)
        {
            throw new NotImplementedException();
        }
    }
}
