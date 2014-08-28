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
    }
}