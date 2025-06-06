using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace MatrizesRGB
{

    public partial class Form1 : Form
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
                pictureBoxSaida1.Image = imgC;
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
                pictureBoxSaida1.Image = imgC;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            salvarImg(pictureBoxSaida1);
        }

        private Bitmap subtrair(Bitmap imagemEntrada, Bitmap imagemSaida)
        {
            Bitmap imgA = new Bitmap(imagemEntrada);
            Bitmap imgB = new Bitmap(imagemSaida);

            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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

                return imgC;
            }
        }



        /*------------------------------------------------- TELA 1 -------------------------------------------------*/

        private void ConfigurarNumeric(decimal min, decimal max, decimal increment, int decimais)
        {
            Tela1_numEntrada.Minimum = min;
            Tela1_numEntrada.Maximum = max;
            Tela1_numEntrada.Increment = increment;
            Tela1_numEntrada.DecimalPlaces = decimais;
        }

        private void carregarImg(PictureBox pictureBox)
        {
            // Configurações iniciais da OpenFileDialogBox
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\\\cadue\\\\Documents\\\\MatLab\\\\Material Matlab\\\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 5;
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
                        pictureBoxSaida1.Image.Save(saveFileDialog.FileName, formato);
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

        private void multiplicarImg(PictureBox pictureEntrada, PictureBox pictureSaida, float valorEntrada)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int finalR = (int)(pixel.R * valorEntrada);
                    int finalG = (int)(pixel.G * valorEntrada);
                    int finalB = (int)(pixel.B * valorEntrada);
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

        private void dividirImg(PictureBox pictureEntrada, PictureBox pictureSaida, float valorEntrada)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int finalR = (int)(pixel.R / valorEntrada);
                    int finalG = (int)(pixel.G / valorEntrada);
                    int finalB = (int)(pixel.B / valorEntrada);
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

        private void escalaCinza(PictureBox pictureEntrada, PictureBox pictureSaida)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            Bitmap img2 = new Bitmap(pictureE.Width, pictureE.Height);

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

        public void not(PictureBox pictureEntrada, PictureBox pictureSaida)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int finalR = 255 - pixel.R;
                    int finalG = 255 - pixel.G;
                    int finalB = 255 - pixel.B;
                    Color cor = Color.FromArgb(255, finalR, finalG, finalB);
                    img2.SetPixel(i, j, cor);
                }
            }
            pictureSaida.Image = img2;
        }

        private void equalizacaoHistograma(PictureBox pictureEntrada, PictureBox pictureSaida, Chart histoSaida, Chart histoOrig)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);

            int[] histogramaImagem = new int[256];
            int[] distribuicaoCumulativa = new int[256];
            int[] novaCor = new int[256];
            int totalPixels = pictureE.Width * pictureE.Height;

            // Calcula original
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int cinza = (pixel.R + pixel.G + pixel.B) / 3;
                    histogramaImagem[cinza]++;
                }
            }

            //Exibe no Chart
            histoOrig.Series.Clear();
            Series serie = new Series("Histograma Original");
            serie.ChartType = SeriesChartType.Column;

            for (int i = 0; i < 256; i++)
            {
                serie.Points.AddXY(i, histogramaImagem[i]);
            }

            histoOrig.Series.Add(serie);
            histoOrig.ChartAreas[0].AxisX.Title = "Nível de Cinza";
            histoOrig.ChartAreas[0].AxisX.Minimum = 0;
            histoOrig.ChartAreas[0].AxisX.Maximum = 255;
            histoOrig.ChartAreas[0].AxisY.Title = "Frequência";
            histoOrig.ChartAreas[0].AxisY.Minimum = 0;
            histoOrig.ChartAreas[0].AxisY.Maximum = 12000;
            histoOrig.ChartAreas[0].AxisY.Interval = 500;

            // Distribuicao cumulativa
            distribuicaoCumulativa[0] = histogramaImagem[0];
            for (int i = 1; i < 256; i++)
            {
                distribuicaoCumulativa[i] = distribuicaoCumulativa[i - 1] + histogramaImagem[i];
            }

            int CFD_min = distribuicaoCumulativa.First(x => x > 0);

            // novos valores de cinza
            for (int i = 0; i < 256; i++)
            {
                novaCor[i] = (int)Math.Floor(((double)(distribuicaoCumulativa[i] - CFD_min) / (totalPixels - CFD_min)) * (256 - 1));
            }

            //Aplica a equalização na imagem
            Bitmap imagemEqualizada = new Bitmap(pictureE.Width, pictureE.Height);
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int cinza = (pixel.R + pixel.G + pixel.B) / 3;
                    int novoCinza = novaCor[cinza];
                    Color novaCorPixel = Color.FromArgb(novoCinza, novoCinza, novoCinza);
                    imagemEqualizada.SetPixel(i, j, novaCorPixel);
                }
            }
            pictureSaida.Image = imagemEqualizada;

            //histograma da nova imagem
            int[] novoHistograma = new int[256];
            for (int i = 0; i < imagemEqualizada.Width; i++)
            {
                for (int j = 0; j < imagemEqualizada.Height; j++)
                {
                    int cinza = imagemEqualizada.GetPixel(i, j).R;
                    novoHistograma[cinza]++;
                }
            }

            //Exibe no Chart
            histoSaida.Series.Clear();
            Series serieSaida = new Series("Histograma Equalizado");
            serieSaida.ChartType = SeriesChartType.Column;

            for (int i = 0; i < 256; i++)
            {
                serieSaida.Points.AddXY(i, novoHistograma[i]);
            }

            histoSaida.Series.Add(serieSaida);
            histoSaida.ChartAreas[0].AxisX.Title = "Nível de Cinza";
            histoSaida.ChartAreas[0].AxisX.Minimum = 0;
            histoSaida.ChartAreas[0].AxisX.Maximum = 255;
            histoSaida.ChartAreas[0].AxisY.Title = "Frequência";
            histoSaida.ChartAreas[0].AxisY.Minimum = 0;
            histoSaida.ChartAreas[0].AxisY.Maximum = 12000;
            histoSaida.ChartAreas[0].AxisY.Interval = 500;
        }

        private void limiar(PictureBox pictureEntrada, PictureBox pictureSaida)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            int limiar = 128;
            for (int i = 0; i < pictureE.Width; i++)
            {
                for (int j = 0; j < pictureE.Height; j++)
                {
                    Color pixel = pictureE.GetPixel(i, j);
                    int cinza = (pixel.R + pixel.G + pixel.B) / 3;
                    if (cinza > limiar)
                    {
                        img2.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        img2.SetPixel(i, j, Color.Black);
                    }
                }
            }
            pictureSaida.Image = img2;
        }

        private void max(PictureBox pictureEntrada, PictureBox pictureSaida, int valorCombo)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            int largura = pictureE.Width;
            int altura = pictureE.Height;

            Bitmap pictureS = new Bitmap(largura, altura);
            int localCombo = valorCombo / 2;
            int tamanhoJanela = valorCombo * valorCombo;

            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    byte[] valoresR = new byte[tamanhoJanela];
                    byte[] valoresG = new byte[tamanhoJanela];
                    byte[] valoresB = new byte[tamanhoJanela];

                    int index = 0;

                    for (int i = -localCombo; i <= localCombo; i++)
                    {
                        for (int j = -localCombo; j <= localCombo; j++)
                        {
                            int px = x + i;
                            int py = y + j;

                            if (px >= 0 && px < largura && py >= 0 && py < altura)
                            {
                                Color pixel = pictureE.GetPixel(px, py);
                                valoresR[index] = pixel.R;
                                valoresG[index] = pixel.G;
                                valoresB[index] = pixel.B;
                                index++;
                            }
                        }
                    }

                    // Redimensiona os arrays para considerar só os valores válidos (nas bordas)
                    Array.Resize(ref valoresR, index);
                    Array.Resize(ref valoresG, index);
                    Array.Resize(ref valoresB, index);

                    // Encontra o maior valor de cada cor
                    byte maximoR = 0;
                    byte maximoG = 0;
                    byte maximoB = 0;

                    for (int k = 0; k < index; k++)
                    {
                        if (valoresR[k] > maximoR) maximoR = valoresR[k];
                        if (valoresG[k] > maximoG) maximoG = valoresG[k];
                        if (valoresB[k] > maximoB) maximoB = valoresB[k];
                    }

                    pictureS.SetPixel(x, y, Color.FromArgb(maximoR, maximoG, maximoB));
                }
            }

            pictureSaida.Image = pictureS;
        }


        private void min(PictureBox pictureEntrada, PictureBox pictureSaida, int valorCombo)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            int largura = pictureE.Width;
            int altura = pictureE.Height;

            Bitmap pictureS = new Bitmap(largura, altura);
            int localCombo = valorCombo / 2;
            int tamanhoJanela = valorCombo * valorCombo;

            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    byte[] valoresR = new byte[tamanhoJanela];
                    byte[] valoresG = new byte[tamanhoJanela];
                    byte[] valoresB = new byte[tamanhoJanela];

                    int index = 0;

                    for (int i = -localCombo; i <= localCombo; i++)
                    {
                        for (int j = -localCombo; j <= localCombo; j++)
                        {
                            int px = x + i;
                            int py = y + j;

                            if (px >= 0 && px < largura && py >= 0 && py < altura)
                            {
                                Color pixel = pictureE.GetPixel(px, py);
                                valoresR[index] = pixel.R;
                                valoresG[index] = pixel.G;
                                valoresB[index] = pixel.B;
                                index++;
                            }
                        }
                    }

                    // Ajusta o tamanho dos arrays para considerar só os valores válidos (bordas)
                    Array.Resize(ref valoresR, index);
                    Array.Resize(ref valoresG, index);
                    Array.Resize(ref valoresB, index);

                    // Encontra o menor valor de cada cor
                    byte minimoR = 255;
                    byte minimoG = 255;
                    byte minimoB = 255;

                    for (int k = 0; k < index; k++)
                    {
                        if (valoresR[k] < minimoR) minimoR = valoresR[k];
                        if (valoresG[k] < minimoG) minimoG = valoresG[k];
                        if (valoresB[k] < minimoB) minimoB = valoresB[k];
                    }

                    pictureS.SetPixel(x, y, Color.FromArgb(minimoR, minimoG, minimoB));
                }
            }

            pictureSaida.Image = pictureS;
        }


        private void mean(PictureBox pictureEntrada, ref PictureBox pictureSaida, int valorCombo)
        {
            Bitmap pictureE = (Bitmap)pictureEntrada.Image;
            Bitmap pictureS = new Bitmap(pictureE.Width, pictureE.Height);
            int offset = valorCombo / 2;

            for (int y = offset; y < pictureE.Height - offset; y++)
            {
                for (int x = offset; x < pictureE.Width - offset; x++)
                {
                    int somaR = 0, somaG = 0, somaB = 0;

                    for (int j = -offset; j <= offset; j++)
                    {
                        for (int i = -offset; i <= offset; i++)
                        {
                            Color pixel = pictureE.GetPixel(x + i, y + j);
                            somaR += pixel.R;
                            somaG += pixel.G;
                            somaB += pixel.B;
                        }
                    }

                    int totalPixels = valorCombo * valorCombo;
                    byte mediaR = (byte)(somaR / totalPixels);
                    byte mediaG = (byte)(somaG / totalPixels);
                    byte mediaB = (byte)(somaB / totalPixels);

                    pictureS.SetPixel(x, y, Color.FromArgb(mediaR, mediaG, mediaB));
                }
            }

            pictureSaida.Image = pictureS;
        }

        private void mediana(PictureBox pictureEntrada, PictureBox pictureSaida, int valorCombo)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            int largura = pictureE.Width;
            int altura = pictureE.Height;

            Bitmap pictureS = new Bitmap(largura, altura);
            int localCombo = valorCombo / 2;
            int tamanhoJanela = valorCombo * valorCombo;

            for (int y = localCombo; y < altura - localCombo; y++)
            {
                for (int x = localCombo; x < largura - localCombo; x++)
                {
                    byte[] valoresR = new byte[tamanhoJanela];
                    byte[] valoresG = new byte[tamanhoJanela];
                    byte[] valoresB = new byte[tamanhoJanela];

                    int index = 0;

                    for (int i = -localCombo; i <= localCombo; i++)
                    {
                        for (int j = -localCombo; j <= localCombo; j++)
                        {
                            int px = x + i;
                            int py = y + j;

                            if (px >= 0 && px < largura && py >= 0 && py < altura)
                            {
                                Color pixel = pictureE.GetPixel(px, py);
                                valoresR[index] = pixel.R;
                                valoresG[index] = pixel.G;
                                valoresB[index] = pixel.B;
                                index++;
                            }
                        }
                    }

                    // Se o índice for menor que o tamanho total (ocorre nas bordas), redimensionar os arrays antes de ordenar
                    Array.Resize(ref valoresR, index);
                    Array.Resize(ref valoresG, index);
                    Array.Resize(ref valoresB, index);

                    Array.Sort(valoresR);
                    Array.Sort(valoresG);
                    Array.Sort(valoresB);

                    byte medianaR = valoresR[index / 2];
                    byte medianaG = valoresG[index / 2];
                    byte medianaB = valoresB[index / 2];

                    pictureS.SetPixel(x, y, Color.FromArgb(medianaR, medianaG, medianaB));
                }
            }

            pictureSaida.Image = pictureS;
        }

        private void Ordem(PictureBox pictureEntrada, PictureBox pictureSaida, int valorCombo, int ordem)
        {
            Bitmap pictureE = new Bitmap(pictureEntrada.Image);
            int largura = pictureE.Width;
            int altura = pictureE.Height;

            Bitmap pictureS = new Bitmap(largura, altura);
            int localCombo = valorCombo / 2;
            int tamanhoJanela = valorCombo * valorCombo;

            for (int y = localCombo; y < altura - localCombo; y++)
            {
                for (int x = localCombo; x < largura - localCombo; x++)
                {
                    byte[] valoresR = new byte[tamanhoJanela];
                    byte[] valoresG = new byte[tamanhoJanela];
                    byte[] valoresB = new byte[tamanhoJanela];

                    int index = 0;

                    for (int i = -localCombo; i <= localCombo; i++)
                    {
                        for (int j = -localCombo; j <= localCombo; j++)
                        {
                            int px = x + i;
                            int py = y + j;

                            if (px >= 0 && px < largura && py >= 0 && py < altura)
                            {
                                Color pixel = pictureE.GetPixel(px, py);
                                valoresR[index] = pixel.R;
                                valoresG[index] = pixel.G;
                                valoresB[index] = pixel.B;
                                index++;
                            }
                        }
                    }

                    // Se o índice for menor que o tamanho total (ocorre nas bordas), redimensionar os arrays antes de ordenar
                    Array.Resize(ref valoresR, index);
                    Array.Resize(ref valoresG, index);
                    Array.Resize(ref valoresB, index);

                    Array.Sort(valoresR);
                    Array.Sort(valoresG);
                    Array.Sort(valoresB);

                    int ordemFinal = Math.Max(0, Math.Min(ordem, valoresR.Length - 1));

                    byte medianaR = valoresR[ordemFinal];
                    byte medianaG = valoresG[ordemFinal];
                    byte medianaB = valoresB[ordemFinal];

                    pictureS.SetPixel(x, y, Color.FromArgb(medianaR, medianaG, medianaB));
                }
            }

            pictureSaida.Image = pictureS;
        }
        private double FormGaussian(double x1, double y2, double entrada)
        {
            return (1.0 / (2.0 * Math.PI * Math.Pow(entrada, 2.0)) * Math.Exp(-(Math.Pow(x1, 2.0) + Math.Pow(y2, 2.0)) / (2.0 * Math.Pow(entrada, 2.0))));
        }

        private void Gaussian(double valorCalculo, int valorEntrada)
        {
            double[,] kernel = new double[valorEntrada, valorEntrada];
            int offset = valorEntrada / 2;
            double sum = 0.0;

            // Gerar e somar os valores do kernel
            for (int y = -offset; y <= offset; y++)
            {
                for (int x = -offset; x <= offset; x++)
                {
                    double valor = FormGaussian(x, y, valorCalculo);
                    kernel[y + offset, x + offset] = valor;
                    sum += valor;

                    // Imprimir coordenadas e valor
                    Console.WriteLine($"({x},{y}) = {valor:F6}");
                }
            }

            Console.WriteLine($"\nSoma total antes da normalização: {sum:F6}");

            // Normalizar o kernel
            for (int i = 0; i < valorEntrada; i++)
            {
                for (int j = 0; j < valorEntrada; j++)
                {
                    kernel[i, j] /= sum;
                }
            }

            // Soma dos valores já normalizados
            double sumNormalizado = 0.0;
            for (int i = 0; i < valorEntrada; i++)
            {
                for (int j = 0; j < valorEntrada; j++)
                {
                    sumNormalizado += kernel[i, j];
                }
            }

            Console.WriteLine($"\nSoma após normalização: {sumNormalizado:F6}");

            // Aplicar o filtro na imagem

            Bitmap pictureE = new Bitmap(Tela1_pictureBoxAdd.Image);
            Bitmap pictureS = new Bitmap(pictureE.Width, pictureE.Height);
            int localCombo = valorEntrada / 2;
            for (int y = localCombo; y < pictureE.Height - localCombo; y++)
            {
                for (int x = localCombo; x < pictureE.Width - localCombo; x++)
                {
                    double novoR = 0.0;
                    double novoG = 0.0;
                    double novoB = 0.0;
                    for (int i = -localCombo; i <= localCombo; i++)
                    {
                        for (int j = -localCombo; j <= localCombo; j++)
                        {
                            Color pixel = pictureE.GetPixel(x + i, y + j);
                            novoR += pixel.R * kernel[j + offset, i + offset];
                            novoG += pixel.G * kernel[j + offset, i + offset];
                            novoB += pixel.B * kernel[j + offset, i + offset];
                        }
                    }
                    // Limitar os valores entre 0 e 255
                    novoR = Math.Max(0, Math.Min(255, novoR));
                    novoG = Math.Max(0, Math.Min(255, novoG));
                    novoB = Math.Max(0, Math.Min(255, novoB));
                    pictureS.SetPixel(x, y, Color.FromArgb((int)novoR, (int)novoG, (int)novoB));
                }
            }
            Tela1_pictureBoxSaida.Image = pictureS;

            // Exibir o kernel no console
            Console.WriteLine("\nKernel:");
            for (int i = 0; i < valorEntrada; i++)
            {
                for (int j = 0; j < valorEntrada; j++)
                {
                    Console.Write($"{kernel[i, j]:F6} ");
                }
                Console.WriteLine();
            }

            // Exibir a soma total do kernel
            Console.WriteLine($"\nSoma total do kernel: {sum:F6}");

            // Exibir imagem do Kernel
            Bitmap kernelImage = new Bitmap(valorEntrada, valorEntrada);
            for (int i = 0; i < valorEntrada; i++)
            {
                for (int j = 0; j < valorEntrada; j++)
                {
                    int pixelValue = (int)(kernel[i, j] * 255);
                    kernelImage.SetPixel(i, j, Color.FromArgb(pixelValue, pixelValue, pixelValue));
                }
            }
            
        }

        private Bitmap Dilatacao(Bitmap imagemEntrada, PictureBox pictureSaida, string tipoElemento, int valorCombo)
        {
            int largura = imagemEntrada.Width;
            int altura = imagemEntrada.Height;

            Bitmap imagemSaida = new Bitmap(largura, altura);

            int localCombo = valorCombo / 2;

            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++)
                    imagemSaida.SetPixel(x, y, Color.Black);

            for (int y = localCombo; y < altura - localCombo; y++)
            {
                for (int x = localCombo; x < largura - localCombo; x++)
                {
                    bool encontrouBranco = false;

                    if (tipoElemento == "Quadrado")
                    {
                        for (int j = -localCombo; j <= localCombo && !encontrouBranco; j++)
                        {
                            for (int i = -localCombo; i <= localCombo && !encontrouBranco; i++)
                            {
                                int px = x + i;
                                int py = y + j;
                                Color cor = imagemEntrada.GetPixel(px, py);

                                if (cor.R > 127 || cor.G > 127 || cor.B > 127)
                                    encontrouBranco = true;
                            }
                        }
                        if (encontrouBranco)
                            imagemSaida.SetPixel(x, y, Color.White);
                    }
                    else if (tipoElemento == "Cruz")
                    {
                        Color centro = imagemEntrada.GetPixel(x, y);
                        if (centro.R > 127 || centro.G > 127 || centro.B > 127)
                        {
                            encontrouBranco = true;
                        }

                        for (int offset = 1; offset <= localCombo && !encontrouBranco; offset++)
                        {
                            Color cima = imagemEntrada.GetPixel(x, y - offset);
                            if (cima.R > 127 || cima.G > 127 || cima.B > 127)
                            {
                                encontrouBranco = true;
                                break;
                            }

                            Color baixo = imagemEntrada.GetPixel(x, y + offset);
                            if (baixo.R > 127 || baixo.G > 127 || baixo.B > 127)
                            {
                                encontrouBranco = true;
                                break;
                            }

                            Color esquerda = imagemEntrada.GetPixel(x - offset, y);
                            if (esquerda.R > 127 || esquerda.G > 127 || esquerda.B > 127)
                            {
                                encontrouBranco = true;
                                break;
                            }

                            Color direita = imagemEntrada.GetPixel(x + offset, y);
                            if (direita.R > 127 || direita.G > 127 || direita.B > 127)
                            {
                                encontrouBranco = true;
                                break;
                            }
                        }
                        if (encontrouBranco)
                            imagemSaida.SetPixel(x, y, Color.White);
                    }
                }
            }
            pictureSaida.Image = imagemSaida;
            return imagemSaida;
        }

        private Bitmap Erosao(Bitmap imagemEntrada, PictureBox pictureSaida, string tipoElemento, int valorCombo)
        {
            
            int largura = imagemEntrada.Width;
            int altura = imagemEntrada.Height;

            Bitmap imagemSaida = new Bitmap(largura, altura);

            int localCombo = valorCombo / 2;

            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++)
                    imagemSaida.SetPixel(x, y, Color.Black);

            for (int y = localCombo; y < altura - localCombo; y++)
            {
                for (int x = localCombo; x < largura - localCombo; x++)
                {
                    bool encontrouPreto = false;

                    if (tipoElemento == "Quadrado")
                    {
                        for (int j = -localCombo; j <= localCombo && !encontrouPreto; j++)
                        {
                            for (int i = -localCombo; i <= localCombo && !encontrouPreto; i++)
                            {
                                int px = x + i;
                                int py = y + j;
                                Color cor = imagemEntrada.GetPixel(px, py);

                                if (!(cor.R > 127 && cor.G > 127 && cor.B > 127))
                                {
                                    encontrouPreto = true;
                                }
                            }
                        }
                        if (!encontrouPreto)
                        {
                            imagemSaida.SetPixel(x, y, Color.White);
                        }
                    }
                    else if (tipoElemento == "Cruz")
                    {
                        Color centro = imagemEntrada.GetPixel(x, y);
                        if (!(centro.R > 127 && centro.G > 127 && centro.B > 127))
                        {
                            encontrouPreto = true;
                        }

                        for (int offset = 1; offset <= localCombo && !encontrouPreto; offset++)
                        {
                            Color cima = imagemEntrada.GetPixel(x, y - offset);
                            if (!(cima.R > 127 && cima.G > 127 && cima.B > 127))
                            {
                                encontrouPreto = true;
                                break;
                            }

                            Color baixo = imagemEntrada.GetPixel(x, y + offset);
                            if (!(baixo.R > 127 && baixo.G > 127 && baixo.B > 127))
                            {
                                encontrouPreto = true;
                                break;
                            }

                            Color esquerda = imagemEntrada.GetPixel(x - offset, y);
                            if (!(esquerda.R > 127 && esquerda.G > 127 && esquerda.B > 127))
                            {
                                encontrouPreto = true;
                                break;
                            }

                            Color direita = imagemEntrada.GetPixel(x + offset, y);
                            if (!(direita.R > 127 && direita.G > 127 && direita.B > 127))
                            {
                                encontrouPreto = true;
                                break;
                            }
                        }

                        if (!encontrouPreto)
                            imagemSaida.SetPixel(x, y, Color.White);
                    }
                }
            }
            pictureSaida.Image = imagemSaida;
            return imagemSaida;
        }

        private void Abertura(PictureBox pictureEntrada, PictureBox pictureSaida, string tipoElemento, int valorCombo)
        {
            Bitmap imagemEntrada = new Bitmap(pictureEntrada.Image);

            Bitmap imagemErosao = Erosao(imagemEntrada, pictureSaida, tipoElemento, valorCombo);
            Bitmap imagemAbertura = Dilatacao(imagemErosao, pictureSaida, tipoElemento, valorCombo);

            pictureSaida.Image = imagemAbertura;
        }

        private void Fechamento(PictureBox pictureEntrada, PictureBox pictureSaida, string tipoElemento, int valorCombo)
        {
            Bitmap imagemEntrada = new Bitmap(pictureEntrada.Image);

            Bitmap imagemDilatacao = Dilatacao(imagemEntrada, pictureSaida, tipoElemento, valorCombo);
            Bitmap imagemFechamento = Erosao(imagemDilatacao, pictureSaida, tipoElemento, valorCombo);

            pictureSaida.Image = imagemFechamento;
        }

        private void Contorno(PictureBox pictureEntrada, PictureBox pictureSaida, string tipoElemento, int valorCombo)
        {
            Bitmap imagemEntrada = new Bitmap(pictureEntrada.Image);

            Bitmap imagemErosao = Erosao(imagemEntrada, pictureSaida, tipoElemento, valorCombo);
            Bitmap imagemContorno = subtrair(imagemEntrada, imagemErosao);

            pictureSaida.Image = imagemContorno;
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

        private void Tela1_btnMultiplicar_Click(object sender, EventArgs e)
        {
            ConfigurarNumeric(0.0M, 3.0M, 0.1M, 2); // faixa: 0 a 3
            float valor = (float)Tela1_numEntrada.Value;
            multiplicarImg(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, valor);
        }

        private void Tela1_btnDividir_Click(object sender, EventArgs e)
        {
            ConfigurarNumeric(1M, 16M, 0.1M, 2); // faixa: 1 a 16
            float valor = (float)Tela1_numEntrada.Value;
            if (valor == 0)
            {
                MessageBox.Show("Divisão por zero não é permitida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dividirImg(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, valor);
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

        private void Tela1_btnSave_Click(object sender, EventArgs e)
        {
            salvarImg(Tela1_pictureBoxSaida);
        }
        private void Tela1_btnNot_Click(object sender, EventArgs e)
        {
            not(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida);
        }
        private void Tela1_btnHistograma_Click(object sender, EventArgs e)
        {
            equalizacaoHistograma(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, Tela1_chartHistogramaPronto, Tela1_chartHistogramaNormal);
        }
        private void Tela1_btnLimiar_Click(object sender, EventArgs e)
        {
            limiar(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida);
        }

        private void Tela1_btnMAX_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox1.Text;
            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            } else
            {
                valorEntrada = 3;
            }

                max(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, valorEntrada);
        }

        private void Tela1_btnMIN_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox1.Text;
            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }

            min(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, valorEntrada);
        }

        private void Tela1_btnMedia_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox1.Text;
            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }

            mean(Tela1_pictureBoxAdd, ref Tela1_pictureBoxSaida, valorEntrada);
        }
        private void Tela1_btnMediana_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox1.Text;
            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }

            mediana(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, valorEntrada);
        }

        private void Tela1_btnOrdem_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox1.Text;
            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }

            int ordem;
            if (!int.TryParse(Tela1_numOrdem.Text, out ordem))
            {
                MessageBox.Show("Digite um número válido para a ordem.");
                return;
            }

            int totalElementos = valorEntrada * valorEntrada;
            if (ordem < 0 || ordem >= totalElementos)
            {
                MessageBox.Show($"A ordem deve estar entre 0 e {totalElementos - 1}.");
                return;
            }


            Ordem(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, valorEntrada, ordem);
        }
        private void Tela1_btnGaussian_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox1.Text;
            double valorCalculo = (double)Tela1_numEntrada.Value;
            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }

            Gaussian(valorCalculo, valorEntrada);
        }

        private void Tela1_btnDilatacao_Click(object sender, EventArgs e)
        {
            Bitmap imagemEntrada = new Bitmap(Tela1_pictureBoxAdd.Image);
            string tipoElemento = comboTipo.Text;
            string valorCombo = comboTamanho.Text;

            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }
            Dilatacao(imagemEntrada, Tela1_pictureBoxSaida, tipoElemento, valorEntrada);
        }

        private void Tela1_btnErosao_Click(object sender, EventArgs e)
        {
            Bitmap imagemEntrada = new Bitmap(Tela1_pictureBoxAdd.Image);
            string tipoElemento = comboTipo.Text;
            string valorCombo = comboTamanho.Text;

            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }
            Erosao(imagemEntrada, Tela1_pictureBoxSaida, tipoElemento, valorEntrada);
        }

        private void Tela1_btnAbertura_Click(object sender, EventArgs e)
        {
            Bitmap imagemEntrada = new Bitmap(Tela1_pictureBoxAdd.Image);
            string tipoElemento = comboTipo.Text;
            string valorCombo = comboTamanho.Text;

            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }
            Abertura(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, tipoElemento, valorEntrada);
        }

        private void Tela1_btnFechamento_Click(object sender, EventArgs e)
        {
            Bitmap imagemEntrada = new Bitmap(Tela1_pictureBoxAdd.Image);
            string tipoElemento = comboTipo.Text;
            string valorCombo = comboTamanho.Text;

            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }
            Fechamento(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, tipoElemento, valorEntrada);
        }

        private void Tela1_btnContorno_Click(object sender, EventArgs e)
        {
            Bitmap imagemEntrada = new Bitmap(Tela1_pictureBoxAdd.Image);
            string tipoElemento = comboTipo.Text;
            string valorCombo = comboTamanho.Text;

            int valorEntrada = 3;
            if (valorCombo == "5x5")
            {
                valorEntrada = 5;
            }
            else if (valorCombo == "7x7")
            {
                valorEntrada = 7;
            }
            else
            {
                valorEntrada = 3;
            }
            Contorno(Tela1_pictureBoxAdd, Tela1_pictureBoxSaida, tipoElemento, valorEntrada);
        }
        /*----------------------------------------------------------------------------------------------------------*/


        /*------------------------------------------------- TELA 2 -------------------------------------------------*/

        private void blending(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
            float entrada = (float)Tela2_valorEntrada.Value;

            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);

                        int finalR = (int)(entrada * pixelA.R + (1 - entrada) * pixelB.R);
                        int finalG = (int)(entrada * pixelA.G + (1 - entrada) * pixelB.G);
                        int finalB = (int)(entrada * pixelA.B + (1 - entrada) * pixelB.B);

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

                        Color saida = Color.FromArgb(255, finalR, finalG, finalB);

                        imgC.SetPixel(i, j, saida);
                    }
                }
            }
            pictureSaida.Image = imgC;
        }

        private void media(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);

            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);
                        int finalR = (pixelA.R + pixelB.R) / 2;
                        int finalG = (pixelA.G + pixelB.G) / 2;
                        int finalB = (pixelA.B + pixelB.B) / 2;
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
            }
            pictureSaida.Image = imgC;
        }
        private void diferImg(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaidaC, PictureBox pictureSaidaD, PictureBox pictureSaidaE)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
            Bitmap imgD = new Bitmap(imgA.Width, imgA.Height);
            Bitmap imgE = new Bitmap(imgA.Width, imgA.Height);

            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

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
                pictureSaidaC.Image = imgC;
            }


            for (int i = 0; i < imgA.Width; i++)
            {
                for (int j = 0; j < imgA.Height; j++)
                {
                    Color pixelA = imgA.GetPixel(i, j);
                    Color pixelB = imgB.GetPixel(i, j);

                    int finalR = pixelB.R - pixelA.R;
                    int finalG = pixelB.G - pixelA.G;
                    int finalB = pixelB.B - pixelA.B;

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

                    imgD.SetPixel(i, j, cor);
                }
            }
            pictureSaidaD.Image = imgD;


            for (int i = 0; i < imgC.Width; i++)
            {
                for (int j = 0; j < imgC.Height; j++)
                {
                    Color pixelC = imgC.GetPixel(i, j);
                    Color pixelD = imgD.GetPixel(i, j);

                    int finalR = pixelC.R + pixelD.R;
                    int finalG = pixelC.G + pixelD.G;
                    int finalB = pixelC.B + pixelD.B;

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

                    imgE.SetPixel(i, j, cor);
                }
            }
            pictureSaidaE.Image = imgE;
        }

        private void And(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);
                        int finalR = pixelA.R & pixelB.R;
                        int finalG = pixelA.G & pixelB.G;
                        int finalB = pixelA.B & pixelB.B;
                        Color cor = Color.FromArgb(255, finalR, finalG, finalB);
                        imgC.SetPixel(i, j, cor);
                    }
                }
            }
            pictureSaida.Image = imgC;
        }

        private void Or(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);
                        int finalR = pixelA.R | pixelB.R;
                        int finalG = pixelA.G | pixelB.G;
                        int finalB = pixelA.B | pixelB.B;
                        Color cor = Color.FromArgb(255, finalR, finalG, finalB);
                        imgC.SetPixel(i, j, cor);
                    }
                }
            }
            pictureSaida.Image = imgC;
        }

        private void Xor(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
            if (imgA.Width != imgB.Width || imgA.Height != imgB.Height)
            {
                MessageBox.Show("As imagens devem ter o mesmo tamanho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = 0; i < imgA.Width; i++)
                {
                    for (int j = 0; j < imgA.Height; j++)
                    {
                        Color pixelA = imgA.GetPixel(i, j);
                        Color pixelB = imgB.GetPixel(i, j);
                        int finalR = pixelA.R ^ pixelB.R;
                        int finalG = pixelA.G ^ pixelB.G;
                        int finalB = pixelA.B ^ pixelB.B;
                        Color cor = Color.FromArgb(255, finalR, finalG, finalB);
                        imgC.SetPixel(i, j, cor);
                    }
                }
            }
            pictureSaida.Image = imgC;
        }

        private void multiplicar2Img(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);

            for (int i = 0; i < imgA.Width; i++)
            {
                for (int j = 0; j < imgA.Height; j++)
                {
                    Color pixelA = imgA.GetPixel(i, j);
                    Color pixelB = imgB.GetPixel(i, j);
                    int finalR = (pixelA.R * pixelB.R) / 255;
                    int finalG = (pixelA.G * pixelB.G) / 255;
                    int finalB = (pixelA.B * pixelB.B) / 255;

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
            pictureSaida.Image = imgC;
        }

        private void dividir2Img(PictureBox pictureEntradaA, PictureBox pictureEntradaB, PictureBox pictureSaida)
        {
            Bitmap imgA = new Bitmap(pictureEntradaA.Image);
            Bitmap imgB = new Bitmap(pictureEntradaB.Image);
            Bitmap imgC = new Bitmap(imgA.Width, imgA.Height);
            for (int i = 0; i < imgA.Width; i++)
            {
                for (int j = 0; j < imgA.Height; j++)
                {
                    Color pixelA = imgA.GetPixel(i, j);
                    Color pixelB = imgB.GetPixel(i, j);
                    int rB = pixelB.R;
                    int gB = pixelB.G;
                    int bB = pixelB.B;

                    if (rB == 0)
                    {
                        rB = 1;
                    }
                    if (gB == 0)
                    {
                        gB = 1;
                    }
                    if (bB == 0)
                    {
                        bB = 1;
                    }

                    int finalR = (pixelA.R * 255) / rB;
                    int finalG = (pixelA.G * 255) / gB;
                    int finalB = (pixelA.B * 255) / bB;
                    if (finalR < 0)
                    {
                        finalR = 0;
                    }
                    if (finalR > 255)
                    {
                        finalR = 255;
                    }
                    if (finalG < 0)
                    {
                        finalG = 0;
                    }
                    if (finalG > 255)
                    {
                        finalG = 255;
                    }
                    if (finalB < 0)
                    {
                        finalB = 0;
                    }
                    if (finalB > 255)
                    {
                        finalB = 255;
                    }
                    Color cor = Color.FromArgb(255, finalR, finalG, finalB);
                    imgC.SetPixel(i, j, cor);
                }
            }
            pictureSaida.Image = imgC;
        }

        private void Tela2_btnDife_Click(object sender, EventArgs e)
        {
            diferImg(pictureBoxA, pictureBoxB, Tela2_PictureBoxC, Tela2_PictureBoxD, Tela2_PictureBoxE);
        }

        private void Tela2_btnBlending_Click(object sender, EventArgs e)
        {
            blending(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        private void Tela2_btnAnd_Click(object sender, EventArgs e)
        {
            And(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        private void Tela2_btnOr_Click_1(object sender, EventArgs e)
        {
            Or(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        private void Tela2_btnXor_Click_1(object sender, EventArgs e)
        {
            Xor(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        private void Tela2_btnMultiplicar_Click(object sender, EventArgs e)
        {
            multiplicar2Img(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        private void Tela2_btnDividir_Click(object sender, EventArgs e)
        {
            dividir2Img(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        private void Tela2_btnMedia_Click(object sender, EventArgs e)
        {
            media(pictureBoxA, pictureBoxB, pictureBoxSaida1);
        }

        /*----------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------- TELA BORDAS --------------------------------------------*/

        private void prewitt(PictureBox Entrada, PictureBox SaidaCinza, PictureBox SaidaRuido, PictureBox SaidaHorizontal, PictureBox SaidaVertical, PictureBox SaidaFinal)
        {
            int entrada = 3;
            escalaCinza(Entrada, SaidaCinza);
            mean(SaidaCinza, ref SaidaRuido, entrada);

            Bitmap imgRuido = new Bitmap(SaidaRuido.Image);
            int largura = imgRuido.Width;
            int altura = imgRuido.Height;

            Bitmap imgSaidaVertical = new Bitmap(largura, altura);
            Bitmap imgSaidaHorizontal = new Bitmap(largura, altura);
            Bitmap imgSaidaFinal = new Bitmap(largura, altura);

            int[,] pixels = new int[largura, altura];

           
            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    pixels[x, y] = imgRuido.GetPixel(x, y).R;
                }
            }

            
            for (int x = 1; x < largura - 1; x++)
            {
                for (int y = 1; y < altura - 1; y++)
                {
                   
                    int h00 = -1 * pixels[x - 1, y - 1];
                    int h01 = 0 * pixels[x, y - 1];
                    int h02 = 1 * pixels[x + 1, y - 1];

                    int h10 = -1 * pixels[x - 1, y];
                    int h11 = 0 * pixels[x, y];
                    int h12 = 1 * pixels[x + 1, y];

                    int h20 = -1 * pixels[x - 1, y + 1];
                    int h21 = 0 * pixels[x, y + 1];
                    int h22 = 1 * pixels[x + 1, y + 1];

                    int somaH = h00 + h01 + h02 + h10 + h11 + h12 + h20 + h21 + h22;

                  
                    int v00 = -1 * pixels[x - 1, y - 1];
                    int v01 = -1 * pixels[x, y - 1];
                    int v02 = -1 * pixels[x + 1, y - 1];

                    int v10 = 0 * pixels[x - 1, y];
                    int v11 = 0 * pixels[x, y];
                    int v12 = 0 * pixels[x + 1, y];

                    int v20 = 1 * pixels[x - 1, y + 1];
                    int v21 = 1 * pixels[x, y + 1];
                    int v22 = 1 * pixels[x + 1, y + 1];

                    int somaV = v00 + v01 + v02 + v10 + v11 + v12 + v20 + v21 + v22;

                    byte total = (byte)Math.Sqrt((somaH * somaH) + (somaV * somaV));

                    if (somaH < 0) somaH = 0;
                    if (somaH > 255) somaH = 255;

                    if (somaV < 0) somaV = 0;
                    if (somaV > 255) somaV = 255;

                    if (total < 0) total = 0;
                    if (total > 255) total = 255;

                    imgSaidaHorizontal.SetPixel(x, y, Color.FromArgb(somaH, somaH, somaH));
                    imgSaidaVertical.SetPixel(x, y, Color.FromArgb(somaV, somaV, somaV));
                    imgSaidaFinal.SetPixel(x, y, Color.FromArgb(total, total, total));
                }
            }
            
            SaidaHorizontal.Image = imgSaidaHorizontal;
            SaidaVertical.Image = imgSaidaVertical;
            SaidaFinal.Image = imgSaidaFinal;
        }

        private void sobel(PictureBox Entrada, PictureBox SaidaCinza, PictureBox SaidaRuido, PictureBox SaidaHorizontal, PictureBox SaidaVertical, PictureBox SaidaFinal)
        {
            int entrada = 3;
            escalaCinza(Entrada, SaidaCinza);
            mean(SaidaCinza, ref SaidaRuido, entrada);

            Bitmap imgRuido = new Bitmap(SaidaRuido.Image);
            int largura = imgRuido.Width;
            int altura = imgRuido.Height;

            Bitmap imgSaidaVertical = new Bitmap(largura, altura);
            Bitmap imgSaidaHorizontal = new Bitmap(largura, altura);
            Bitmap imgSaidaFinal = new Bitmap(largura, altura);

            int[,] pixels = new int[largura, altura];


            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    pixels[x, y] = imgRuido.GetPixel(x, y).R;
                }
            }


            for (int x = 1; x < largura - 1; x++)
            {
                for (int y = 1; y < altura - 1; y++)
                {
                    int h00 = 1 * pixels[x - 1, y - 1];
                    int h01 = 0 * pixels[x, y - 1];
                    int h02 = -1 * pixels[x + 1, y - 1];

                    int h10 = 2 * pixels[x - 1, y];
                    int h11 = 0 * pixels[x, y];
                    int h12 = -2 * pixels[x + 1, y];

                    int h20 = 1 * pixels[x - 1, y + 1];
                    int h21 = 0 * pixels[x, y + 1];
                    int h22 = -1 * pixels[x + 1, y + 1];

                    int somaH = h00 + h01 + h02 + h10 + h11 + h12 + h20 + h21 + h22;


                    int v00 =  1 * pixels[x - 1, y - 1];
                    int v01 =  2 * pixels[x, y - 1];
                    int v02 =  1 * pixels[x + 1, y - 1];

                    int v10 = 0 * pixels[x - 1, y];
                    int v11 = 0 * pixels[x, y];
                    int v12 = 0 * pixels[x + 1, y];

                    int v20 = -1 * pixels[x - 1, y + 1];
                    int v21 = -2 * pixels[x, y + 1];
                    int v22 = -1 * pixels[x + 1, y + 1];

                    int somaV = v00 + v01 + v02 + v10 + v11 + v12 + v20 + v21 + v22;

                    byte total = (byte)Math.Sqrt((somaH * somaH) + (somaV * somaV));

                    if (somaH < 0) somaH = 0;
                    if (somaH > 255) somaH = 255;

                    if (somaV < 0) somaV = 0;
                    if (somaV > 255) somaV = 255;

                    if (total < 0) total = 0;
                    if (total > 255) total = 255;

                    imgSaidaHorizontal.SetPixel(x, y, Color.FromArgb(somaH, somaH, somaH));
                    imgSaidaVertical.SetPixel(x, y, Color.FromArgb(somaV, somaV, somaV));
                    imgSaidaFinal.SetPixel(x, y, Color.FromArgb(total, total, total));
                }
            }

            SaidaHorizontal.Image = imgSaidaHorizontal;
            SaidaVertical.Image = imgSaidaVertical;
            SaidaFinal.Image = imgSaidaFinal;
        }

        private void laplaciano(PictureBox Entrada, PictureBox SaidaCinza, PictureBox SaidaRuido, PictureBox SaidaHorizontal, PictureBox SaidaVertical, PictureBox SaidaFinal,  int valorCombo)
        {
            SaidaHorizontal.Image = null;
            SaidaVertical.Image = null;

            int entrada = 3;
            escalaCinza(Entrada, SaidaCinza);
            mean(SaidaCinza, ref SaidaRuido, entrada);

            Bitmap imgRuido = new Bitmap(SaidaRuido.Image);
            int largura = imgRuido.Width;
            int altura = imgRuido.Height;

            Bitmap imgSaidaFinal = new Bitmap(largura, altura);

            int[,] pixels = new int[largura, altura];


            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    pixels[x, y] = imgRuido.GetPixel(x, y).R;
                }
            }

            switch (valorCombo)
            {
                case 1:

                    for (int x = 1; x < largura - 1; x++)
                    {
                        for (int y = 1; y < altura - 1; y++)
                        {
                            int h00 = 0 * pixels[x - 1, y - 1];
                            int h01 = 1 * pixels[x, y - 1];
                            int h02 = 0 * pixels[x + 1, y - 1];

                            int h10 = 1 * pixels[x - 1, y];
                            int h11 = -4 * pixels[x, y];
                            int h12 = 1 * pixels[x + 1, y];

                            int h20 = 0 * pixels[x - 1, y + 1];
                            int h21 = 1 * pixels[x, y + 1];
                            int h22 = 0 * pixels[x + 1, y + 1];

                            int soma = h00 + h01 + h02 + h10 + h11 + h12 + h20 + h21 + h22;

                            byte total = (byte)Math.Sqrt((soma * soma) + (soma * soma));

                            if (soma < 0) soma = 0;
                            if (soma > 255) soma = 255;

                            if (total < 0) total = 0;
                            if (total > 255) total = 255;

                            imgSaidaFinal.SetPixel(x, y, Color.FromArgb(total, total, total));
                        }
                    }
                    break;

                case 2:

                        for (int x = 1; x < largura - 1; x++)
                        {
                        for (int y = 1; y < altura - 1; y++)
                        {
                            int h00 = 1 * pixels[x - 1, y - 1];
                            int h01 = 1 * pixels[x, y - 1];
                            int h02 = 1 * pixels[x + 1, y - 1];

                            int h10 = 1 * pixels[x - 1, y];
                            int h11 = -8 * pixels[x, y];
                            int h12 = 1 * pixels[x + 1, y];

                            int h20 = 1 * pixels[x - 1, y + 1];
                            int h21 = 1 * pixels[x, y + 1];
                            int h22 = 1 * pixels[x + 1, y + 1];

                            int soma = h00 + h01 + h02 + h10 + h11 + h12 + h20 + h21 + h22;

                            byte total = (byte)Math.Sqrt((soma * soma) + (soma * soma));

                            if (soma < 0) soma = 0;
                            if (soma > 255) soma = 255;

                            if (total < 0) total = 0;
                            if (total > 255) total = 255;

                            imgSaidaFinal.SetPixel(x, y, Color.FromArgb(total, total, total));
                        }
                             }
                    break;

                default:
                    MessageBox.Show("Valor de filtro inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            SaidaFinal.Image = imgSaidaFinal;
        }
        private void TelaBordas_btnAdd_Click(object sender, EventArgs e)
        {
            carregarImg(TelaBordas_pictureBoxAdd);
        }

        private void TelaBordas_btnSave_Click(object sender, EventArgs e)
        {
            salvarImg(TelaBordas_pictureBoxSaida);
        }

        private void TelaBordas_btnPrewitt_Click(object sender, EventArgs e)
        {
            prewitt(TelaBordas_pictureBoxAdd, TelaBordas_pictureBoxCinza, TelaBordas_pictureBoxRuido, TelaBordas_pictureBoxHorizontal, TelaBordas_pictureBoxVertical, TelaBordas_pictureBoxSaida);
        }

        private void TelaBordas_btnSobel_Click(object sender, EventArgs e)
        {
            sobel(TelaBordas_pictureBoxAdd, TelaBordas_pictureBoxCinza, TelaBordas_pictureBoxRuido, TelaBordas_pictureBoxHorizontal, TelaBordas_pictureBoxVertical, TelaBordas_pictureBoxSaida);
        }

        private void TelaBordas_btnLaplaciano_Click(object sender, EventArgs e)
        {
            string valorCombo = comboBox2.Text;
            int valorEntrada = 0;
            if (valorCombo == "Normal")
            {
                valorEntrada = 1;
            }
            else if (valorCombo == "Agressiva")
            {
                valorEntrada = 2;
            }
            else
            {
                valorEntrada = 3;
            }

            laplaciano(TelaBordas_pictureBoxAdd, TelaBordas_pictureBoxCinza, TelaBordas_pictureBoxRuido, TelaBordas_pictureBoxHorizontal, TelaBordas_pictureBoxVertical, TelaBordas_pictureBoxSaida, valorEntrada);

        }

        
    }
}
