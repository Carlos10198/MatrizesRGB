using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrizesRGB
{

    public partial class Form1: Form
    {
        Bitmap img1;
        Bitmap img2;
        byte[,] vImg1Gray;

        byte[,] vImg1R;
        byte[,] vImg1G;
        byte[,] vImg1B;
        byte[,] vImg1A;
        bool bLoadImgOK = false;
        public Form1()
        {
            InitializeComponent();
        }
       
        private void carregarImg(PictureBox pictureBox)
        {
            // Configurações iniciais da OpenFileDialogBox
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\cadue\\Documents\\MatLab\\Material Matlab\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            // Se um arquivo foi localizado com sucesso...
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Armnazena o path do arquivo de imagem
                filePath = openFileDialog1.FileName;

                try
                {
                    img1 = new Bitmap(filePath);
                    img2 = new Bitmap(img1.Width, img1.Height);
                    bLoadImgOK = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao abrir imagem...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bLoadImgOK = false;
                }

                // Se a imagem carregou perfeitamente...
                if (bLoadImgOK == true)
                {
                    // Adiciona imagem na PictureBox
                    pictureBox.Image = img1;
                    vImg1Gray = new byte[img1.Width, img1.Height];
                    vImg1R = new byte[img1.Width, img1.Height];
                    vImg1G = new byte[img1.Width, img1.Height];
                    vImg1B = new byte[img1.Width, img1.Height];
                    vImg1A = new byte[img1.Width, img1.Height];
                }

                // Percorre todos os pixels da imagem...

            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < img1.Width; i++)
            {
                for (int j = 0; j < img1.Height; j++)
                {
                    Color pixel = img1.GetPixel(i, j);

                    int finalR = pixel.R + (int)valorEntrada.Value;
                    int finalG = pixel.G + (int)valorEntrada.Value;
                    int finalB = pixel.B + (int)valorEntrada.Value;

                    if (finalR > 255)
                    {
                        finalR = 255;
                    }

                    if (finalG > 255)
                    {
                        finalG = 255;
                    }

                    if (finalB > 255)
                    {
                        finalB = 255;
                    }


                    Color cor = Color.FromArgb(255, finalR, finalG, finalB);

                    img2.SetPixel(i, j, cor);
                }
            }
            pictureBox2.Image = img2;
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < img1.Width; i++)
            {
                for (int j = 0; j < img1.Height; j++)
                {
                    Color pixel = img1.GetPixel(i, j);

                    int finalR = pixel.R - (int)valorEntrada.Value;
                    int finalG = pixel.G - (int)valorEntrada.Value;
                    int finalB = pixel.B - (int)valorEntrada.Value;

                    if (finalR < 0)
                    {
                        finalR = 0;
                    }

                    if (finalG < 0)
                    {
                        finalG = 0;
                    }

                    if (finalB < 0)
                    {
                        finalB = 0;
                    }


                    Color cor = Color.FromArgb(255, finalR, finalG, finalB);

                    img2.SetPixel(i, j, cor);
                }
            }
            pictureBox2.Image = img2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            carregarImg(pictureBox1);
        }
        private void imgAButton_Click(object sender, EventArgs e)
        {
            carregarImg(pictureBoxA);
        }
        private void imgBButton_Click(object sender, EventArgs e)
        {
            carregarImg(pictureBoxB);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap imgA = new Bitmap(pictureBoxA.Image);
            Bitmap imgB = new Bitmap(pictureBoxB.Image);

            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else
            {
                Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);

                        int finalR = pixelA.R + pixelB.R;
                        int finalG = pixelA.G + pixelB.G;
                        int finalB = pixelA.B + pixelB.B;

                        if (finalR > 255)
                        {
                            finalR = 255;
                        }

                        if (finalG > 255)
                        {
                            finalG = 255;
                        }

                        if (finalB > 255)
                        {
                            finalB = 255;
                        }

                        Color cor = Color.FromArgb(255, finalR, finalG, finalB);

                        imgC.SetPixel(i, j, cor);
                    }
                }
                pictureBoxC.Image = imgC;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap imgA = new Bitmap(pictureBoxA.Image);
            Bitmap imgB = new Bitmap(pictureBoxB.Image);

            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);

                        int finalR = pixelA.R - pixelB.R;
                        int finalG = pixelA.G - pixelB.G;
                        int finalB = pixelA.B - pixelB.B;

                        if (finalR < 0)
                        {
                            finalR = 0;
                        }

                        if (finalG < 0)
                        {
                            finalG = 0;
                        }

                        if (finalB < 0)
                        {
                            finalB = 0;
                        }

                        Color cor = Color.FromArgb(255, finalR, finalG, finalB);

                        imgC.SetPixel(i, j, cor);
                    }
                }
                pictureBoxC.Image = imgC;
            }
        }
    }
}
