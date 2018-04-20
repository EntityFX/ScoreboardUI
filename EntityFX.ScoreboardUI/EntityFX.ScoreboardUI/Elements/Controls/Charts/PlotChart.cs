using System;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls.Charts
{
    public class PlotChart : Control<ControlBase>, ISizable
    {
        private Image _image;
        private Size _size = new Size() {Height = 15, Width = 40};
        private Point[] _points;
        private object _valueLockObject = new {};


        public Point[] Points
        {
            get { return _points; }
            set
            {
                _points = value;
                _image.Clear();
                foreach (var point in Points)
                {
                    if (point.Left < _image.Size.Width && point.Top < _image.Size.Height)
                    {
                        _image.ImageArray[point.Top, point.Left] = PlotSymbol;
                    }
                }

                lock (_valueLockObject)
                {
                    ReRender();
                }

            }
        }

        public ConsoleColor PointColor { get; set; } = ConsoleColor.Cyan;

        public char PlotSymbol { get; set; } = '*';

        public PlotChart()
        {
            _image = new Image(new char[_size.Height, _size.Width]);
            AddChild(_image);
        }

        public Size Size
        {
            get { return _size; }
            set
            {
                _size = value;
                _image.ImageArray = new char[value.Height, value.Width];
            }
        }
    }
}