using PierwszyProjekt.Algorithms;
using PierwszyProjekt.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PierwszyProjekt.DataTypes;

namespace PierwszyProjekt
{
    public partial class Form1 : Form
    {
        string thePath;
        public double ValueA;
        public int ValueN;
        public int ValueL;
        public static int trackbar;
        BaseImage baseImage;
        Sinogram sinogram;
        public static Bitmap oryginalBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        //button "load picture" {load picture to BaseImage}
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baseImage = new BaseImage(thePath);
                baseImage.Display(this.pictureBox1);
                Console.Write("Input picture is loaded...\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error opening the bitmap." + "Please check the path." + ex);
            }
        }

        //textbox "Podaj tutaj ścieżkę do pliku *.bmp" {load text from textbox to variable thePath}
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            thePath = objTextBox.Text;
            Console.Write("Path is loaded...\n");
        }

        private bool checkIsImageNull(PictureBox pictureBox)
        {
            return pictureBox == null || pictureBox.Image == null;
        }

        private bool checkIsElementsNull()
        {
            return textBoxA.Text == null || textBoxN.Text == null 
                || textBoxL.Text == null || !double.TryParse(textBoxA.Text, out ValueA) 
                || !int.TryParse(textBoxN.Text, out ValueN) 
                || !int.TryParse(textBoxL.Text, out ValueL);
        }

        //button "Start algorithm" {our algorithm}
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baseImage == null || checkIsElementsNull())
                {
                    throw new Exception("Nie wystarczające parametry do wykonania akcji!");
                }

                Console.Write("Elements: " + ValueA + ", " +  ValueN + ", " + ValueL + "\n");
                oryginalBitmap = baseImage.Bitmap;

                //DoRandonTransform
                sinogram = new Sinogram(baseImage, ValueN, ValueA, ValueL);
                sinogram.Display(this.pictureBox2);

                //DoReversedRandonTransform
                sinogram.OutPutImage.DoReversedRandonTransform();

                //Bitmap outBitmap = Filter.MedianFilter(sinogram.OutPutImage.Bitmap, 3);
                Bitmap outBitmap = sinogram.OutPutImage.Bitmap;
                pictureBox3.Image = outBitmap;
                this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
   
                //Błąd średniokwadratowy
                double MSE = 0.0;
                for (int i = 0; i < baseImage.Bitmap.Height; ++i)
                {
                    for (int j = 0; j < baseImage.Bitmap.Width; ++j)
                    {
                        MSE += Math.Pow((baseImage.Bitmap.GetPixel(i, j).ToArgb() -
                            outBitmap.GetPixel(i, j).ToArgb()), 2);
                    }
                }
                label8.Text = (Math.Sqrt(MSE/(baseImage.Bitmap.Width * baseImage.Bitmap.Height))).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error opening the bitmap." + "Please check the path." + ex);
            }
        }
     
        //przycisk ułatwia wybór pliku z poziomu eksploratora plików
        private void button3_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fileDialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int max = 80;
            CircleCreator cc = new CircleCreator(max, max);
            Circle c = new Circle(max, max, max / 2, cc.PointsOnCircle.ToArray());
            EmiterGenerator eg = new EmiterGenerator(5, c);

            List<EmiterDetectorsSystem> systems = new List<EmiterDetectorsSystem>();

            eg.Emiters.ForEach(em =>
            {
                DetectorsGenerator dg = new DetectorsGenerator(20, 60, c, em);
                systems.Add(new EmiterDetectorsSystem(em, dg.Detectors));
                
                #region debug console
                //int[,] debugTab = new int[max + 1, max + 1];
                //for (int i = 0; i < max + 1; i++)
                //{
                //    for (int j = 0; j < max + 1; j++)
                //    {
                //        debugTab[i, j] = 0;
                //    }
                //}
                //debugTab[em.Point.X, em.Point.Y] = 3;
                //dg.Detectors.ForEach(d => debugTab[d.Point.X, d.Point.Y] = 1);

                //EmiterDetectorsSystem s = systems.Last();

                //s.Detectors.ForEach(de => s.GetLineForDetector(de).Points.ForEach(point => debugTab[point.X, point.Y] = 4));

                //for (int i = 0; i < max + 1; i++)
                //{
                //    for (int j = 0; j < max + 1; j++)
                //    {
                //        Console.Write(debugTab[i, j]);
                //    }
                //    Console.WriteLine();
                //}
                //Console.ReadLine();
                //Console.Clear();
                #endregion

            });

            BitmapToBlackAndWhiteConverter converter = new BitmapToBlackAndWhiteConverter(this.baseImage.Bitmap);

        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            //label8.Text = "<>" + trackBar1.Value + "<>";
            trackbar = trackBar1.Value;
            this.button2_Click(sender, e);
            
        }
    }
}
