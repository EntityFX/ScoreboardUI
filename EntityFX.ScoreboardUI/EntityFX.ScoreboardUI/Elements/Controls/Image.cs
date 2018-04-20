using System;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Image : ControlBase, ISizable
    {
        private char[,] _imageArray;

        public Image()
        {
        }

        public Image(char[,] imageArray)
        {
            ImageArray = imageArray;
        }

        public char[,] ImageArray
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
            _imageArray = new char[Size.Height, Size.Width]; 
        }
    }
}