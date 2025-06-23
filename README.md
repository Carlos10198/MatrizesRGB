# MatrizesRGB

Este projeto foi desenvolvido como parte de um trabalho acadêmico e representa meu primeiro contato com a linguagem **C#**. Seu principal objetivo é aplicar uma série de técnicas de **processamento digital de imagens** sobre imagens nos formatos BMP, JPG e PNG, usando **Windows Forms** como interface gráfica.

---

## 🧠 Objetivo do Projeto

Criar uma aplicação capaz de ler, manipular e exibir imagens por meio de operações aritméticas, lógicas, morfológicas e filtros espaciais, utilizando manipulação de pixels RGB em C#. A ideia é proporcionar um entendimento prático sobre como cada transformação afeta visualmente a imagem e como se dá a manipulação dos canais de cor.

---

## 💻 Tecnologias Utilizadas

- **Linguagem**: C#
- **IDE**: Visual Studio
- **Framework**: .NET (Windows Forms)
- **Tipos de Imagem Suportados**: BMP, JPG, PNG

---

## 🖼️ Estrutura de Dados

As imagens são manipuladas diretamente por meio da classe `Bitmap`:

```csharp
Bitmap imagem = new Bitmap(pictureBox.Image);
Color pixel = imagem.GetPixel(x, y);
int r = pixel.R;
int g = pixel.G;
int b = pixel.B;
```

Cada pixel é acessado por coordenadas `(x, y)`, e os valores de vermelho, verde e azul são obtidos separadamente.  
As alterações nos pixels são aplicadas com:

```csharp
Color novoPixel = Color.FromArgb(r, g, b);
imagem.SetPixel(x, y, novoPixel);
```

Essa abordagem permite manipular diretamente os dados da imagem sem a necessidade de criar uma matriz tridimensional intermediária.

---

## ⚙️ Funcionalidades Implementadas

### 📥 Entrada e Saída
- [x] Leitura de duas imagens (BMP, JPG, PNG)
- [x] Armazenamento em objetos Bitmap
- [x] Exibição das imagens na interface
- [x] Salvamento da imagem resultante

---

### 🎨 Operações Aritméticas
- [x] Soma de duas imagens
- [x] Soma de valor constante (aumentar brilho) — com tratamento de overflow
- [x] Subtração entre duas imagens
- [x] Subtração de valor constante (diminuir brilho) — com tratamento de underflow
- [x] Multiplicação por constante (ajuste de contraste) — com tratamento de overflow/underflow
- [x] Divisão por constante (ajuste de contraste) — com tratamento de overflow/underflow

---

### 🔄 Transformações Básicas
- [x] Conversão de RGB para Escala de Cinza
- [x] Inversão horizontal (espelhamento esquerda-direita)
- [x] Inversão vertical (espelhamento cima-baixo)
- [x] Diferença entre imagens
- [x] Combinação linear (blending)
- [x] Média de duas imagens

---

### 🧠 Operações Lógicas (Imagens Binárias)
- [x] AND
- [x] OR
- [x] NOT
- [x] XOR

---

### 🌈 Ajustes de Intensidade
- [x] Equalização de Histograma
- [x] Limiarização (Thresholding)

---

## 🧹 Filtros no Domínio Espacial

### 🔽 Filtros Passa-Baixa (Suavização)
- [x] Filtro **MAX**
- [x] Filtro **MIN**
- [x] Filtro **MEAN**
- [x] Filtro **MEDIANA** (remoção de ruído sal e pimenta)
- [x] Filtro **ORDEM**
- [x] Filtro de **Suavização Conservativa**
- [x] Filtro **Gaussiano**

---

### 🔼 Filtros Passa-Alta (Realce de Bordas)
- [x] Detecção de Bordas — **Primeira Ordem**
  - [x] Prewitt
  - [x] Sobel
- [x] Detecção de Bordas — **Segunda Ordem**
  - [x] Laplaciano

---

### ⚒️ Operações Morfológicas
- [x] **Dilatação**
- [x] **Erosão**
- [x] **Abertura**
- [x] **Fechamento**
- [x] **Contorno**

---

## 📂 Estrutura do Projeto

```
MatrizesRGB/
├── Form1.cs                # Lógica principal da aplicação
├── Form1.Designer.cs      # Design da interface com Windows Forms
├── Program.cs             # Ponto de entrada da aplicação
├── Resources/             # Imagens e recursos utilizados
└── README.md              # Este arquivo
```

---

## 📚 Créditos

Desenvolvido por **Carlos10198** como parte do curso de Processamento de Imagens, com foco em aprendizagem de **C#** e técnicas fundamentais de **manipulação de imagens digitais**.

---

## ✅ Status

✔ Projeto finalizado com todas as técnicas requeridas implementadas e testadas.

---

## 🚀 Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/Carlos10198/MatrizesRGB.git
   ```

2. Abra o projeto com o **Visual Studio**.

3. Execute com `F5`.

---

## 📝 Licença

Este projeto é acadêmico e não possui fins comerciais.
