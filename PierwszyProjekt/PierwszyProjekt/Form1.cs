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

namespace PierwszyProjekt
{
    public partial class Form1 : Form
    {
        string thePath;
        BaseImage baseImage;
        Sinogram sinogram;
        BaseImage outImage;

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
                MessageBox.Show("There was an error opening the bitmap." + "Please check the path." + ex );
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

        //button "Start algorithm" {our algorithm}
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkIsImageNull(this.pictureBox1)){
                    throw new Exception();
                }

                //DoRandonTransform
                sinogram = new Sinogram(baseImage);
                sinogram.Display(this.pictureBox2);

                //DoReversedRandonTransform
                outImage = new BaseImage(sinogram);
                outImage.Display(this.pictureBox3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error opening the bitmap." + "Please check the path." + ex);
            }


            /*
            int n = 100;
            CircleCreator cc = new CircleCreator(n - 1, n - 1);
            int[,] array = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = 0;
                }
            }

            cc.PointsOnCircle.ForEach(p => array[p.X, p.Y] = 1);
            LineCreator lc = new LineCreator(new Point(6, 6), new Point(54, 99));
            lc.Line.ForEach(p => array[p.X, p.Y] = 5);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
            */
        }
    }
}
