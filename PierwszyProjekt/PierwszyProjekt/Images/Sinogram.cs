using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void Display(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DoRandonTransform(BaseImage image)
        {
            Bitmap = image.Bitmap;
            Console.Write("I will do --> DoRandonTransform\n");
            //throw new NotImplementedException();
        }
    }
}
