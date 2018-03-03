﻿using PierwszyProjekt.Algorithms;
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
        string element_a;
        string element_n;
        string element_l;
        public int element_int_a;
        public int element_int_n;
        public int element_int_l;
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

        private bool checkIsElementsNull()
        {
            return element_a.Equals(null) || element_n.Equals(null) || element_l.Equals(null) || !int.TryParse(element_a, out element_int_a) || !int.TryParse(element_n, out element_int_n) || !int.TryParse(element_l, out element_int_l);
        }

        //button "Start algorithm" {our algorithm}
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkIsImageNull(this.pictureBox1) || checkIsElementsNull())
                {
                    throw new Exception();
                }

                Console.Write("Elements: " + element_int_a + ", " +  element_int_n + ", " + element_int_l + "\n");

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
        }

        //textbox "Wartość a" {load text from textbox to variable thePath}
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //delta
            TextBox objTextBox = (TextBox)sender;
            element_a = objTextBox.Text;
            Console.Write("Element 'a' is loaded...\n");
        }

        //textbox "Wartość n" {load text from textbox to variable thePath}
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //liczba rzutów
            TextBox objTextBox = (TextBox)sender;
            element_n = objTextBox.Text;
            Console.Write("Element 'n' is loaded...\n");
        }

        //textbox "Wartość l" {load text from textbox to variable thePath}
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //rozwartość/rozpiętość
            TextBox objTextBox = (TextBox)sender;
            element_l = objTextBox.Text;
            Console.Write("Element 'l' is loaded...\n");
        }
    }
}