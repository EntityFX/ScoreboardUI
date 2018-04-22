using System;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Image : ControlBase, ISizable
    {
        private (ConsoleColor Color, char Char)[,] _imageArray;

        public bool UseColors { get; set; } = false;

        public bool UseInvertedColors { get; set; } = false;

        public Image()
        {
            UseColors = false;
        }

        public Image(char[,] imageArray)
        {
            UseColors = false;
            (ConsoleColor Color, char Char)[,] coloredImageArray = new (ConsoleColor, char)[imageArray.GetLength(0), imageArray.GetLength(1)];
            for (int y = 0; y < imageArray.GetLength(0); y++)
            {
                for (int x = 0; x < imageArray.GetLength(1); x++)
                {
                    coloredImageArray[y, x] = (Color : ForegroundColor, Char: imageArray[y, x]);
                }
            }
            ImageArray = coloredImageArray;
        }

        public Image((ConsoleColor Color, char Char)[,] imageArray)
        {
            UseColors = true;
            ImageArray = imageArray;
        }

        public (ConsoleColor Color, char Char)[,] ImageArray
        {
            get { return _imageArray; }
            set
            {
                _imageArray = value;
                Size = new Size
                {
                    Width = _imageArray.GetLength(1),
                    Height = _imageArray.GetLength(0)
                };
            }
        }

        public Size Size { get; set; }

        public static Image FromString(string text)
        {
            var strings = text.Split('\n').Select(s => s.Replace("\r", "")).ToArray();
            var maxString = strings.Max(s => s.Length);
            char[,] imgChar = new Char[strings.Length, maxString];


            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[y].Length; x++)
                {
                    imgChar[y, x] = strings[y][x];
                }
            }

            return new Image(imgChar);
        }

        public void Clear()
        {
            _imageArray = new(ConsoleColor Color, char Char)[Size.Height, Size.Width]; 
        }
    }
}