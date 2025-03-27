using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        private void button4_Click(object sender, EventArgs e)
        {
           salvarImg(pictureBoxC);
        }




        
        

        private void carregarImg(PictureBox pictureBox)
        {
            // Configurações iniciais da OpenFileDialogBox
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\\\cadue\\\\Documents\\\\MatLab\\\\Material Matlab\\\\Matlab\"";
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

        private void salvarImg(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Salvar Imagem";
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveFileDialog.DefaultExt = "png"; // Define PNG como padrão
                saveFileDialog.FileName = "imagem"; // Nome sugerido

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtém o formato de acordo com a extensão escolhida
                    System.Drawing.Imaging.ImageFormat formato = System.Drawing.Imaging.ImageFormat.Png; // Padrão

                    if (saveFileDialog.FileName.EndsWith(".jpg"))
                        formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (saveFileDialog.FileName.EndsWith(".bmp"))
                        formato = System.Drawing.Imaging.ImageFormat.Bmp;

                    try
                    {
                        pictureBoxC.Image.Save(saveFileDialog.FileName, formato);
                        MessageBox.Show("Imagem salva com sucesso em:\n" + saveFileDialog.FileName, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar a imagem:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nenhuma imagem para salvar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void subtrairImg(PictureBox pictureEntrada, PictureBox pictureSaida, int valorEntrada)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);

            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);

                    int finalR = pixel.R - valorEntrada;
                    int finalG = pixel.G - valorEntrada;
                    int finalB = pixel.B - valorEntrada;

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
            pictureSaida.Image = img2;
        }

        private void somarImg(PictureBox pictureEntrada, PictureBox pictureSaida, int valorEntrada)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
           
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int finalR = pixel.R + valorEntrada;
                    int finalG = pixel.G + valorEntrada;
                    int finalB = pixel.B + valorEntrada;
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
            pictureSaida.Image = img2;
        }

        private void escalaCinza(PictureBox pictureEntrada, PictureBox pictureSaida)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            

            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int cinza = ((pixel.R + pixel.B + pixel.G) / 3);

                    Color cor = Color.FromArgb(255, cinza, cinza, cinza);

                    img2.SetPixel(i, j, cor);
                }
            }
            pictureSaida.Image = img2;
        }

        private void inverterHorizontal(PictureBox pictureEntrada, PictureBox pictureSaida)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);

            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    Color invert = pictureE.GetPixel(pictureE.Width - i - 1, j);

                    Color cor = Color.FromArgb(255, invert);

                    img2.SetPixel(i, j, cor);
                }
            }
            pictureSaida.Image = img2;
        }
        private void inverterVertical(PictureBox pictureEntrada, PictureBox pictureSaida)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);

            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    Color inverterVertical = pictureE.GetPixel(i, pictureE.Height - j - 1);

                    Color cor = Color.FromArgb(255, inverterVertical);

                    img2.SetPixel(i, j, cor);
                }
            }
            pictureSaida.Image = img2;
        }

        private void Tela1_btnAddImg_Click(object sender, EventArgs e)
        {
            carregarImg(Tela1_pictureBoxAdd);
        }

        private void Tela1_btnSubtrair_Click(object sender, EventArgs e)
        {
            subtrairImg(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, (int)Tela1_numEntrada.Value);
        }

        private void Tela1_btnSomar_Click(object sender, EventArgs e)
        {
            somarImg(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, (int)Tela1_numEntrada.Value);
        }

        private void Tela1_btnCinza_Click(object sender, EventArgs e)
        {
            escalaCinza(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida);
        }

        private void Tela1_btnHorizontal_Click(object sender, EventArgs e)
        {
            inverterHorizontal(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida);
        }

        private void Tela1_btnVertical_Click(object sender, EventArgs e)
        {
            inverterVertical(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida);
        }
    }
}
