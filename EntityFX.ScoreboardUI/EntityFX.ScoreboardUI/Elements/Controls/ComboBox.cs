using System;
using System.Globalization;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class ComboBox : ListControlBase<string>, ILinearable
    {
        public ComboBox()
        {
            Width = 15;
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Blue;
            ExpanderBackground = ConsoleColor.Gray;
            VisibleItemsCount = 5;
        }

        public ConsoleColor ExpanderBackground { get; set; }

        public bool DroppedDown { get; set; }

        public int VisibleItemsCount { get; set; }

        public event EventHandler SelectedIndexChanged;

        public event EventHandler DropDown;

        public event EventHandler DropClosed;

        public override string Text
        {
            get { return SelectedIndex == -1 ? string.Empty : Items[SelectedIndex].ToString(CultureInfo.InvariantCulture); }
            set
            {
                if (SelectedIndex >= 0)
                {
                    Items[SelectedIndex] = value;
                }
            }
        }

        public override void PressKey(KeyPressEventArgs e)
        {
            base.PressKey(e);
            if (e.KeyInfo.Key == ConsoleKey.Enter)
            {
                DroppedDown = !DroppedDown;
                if (DropDown != null)
                {
                    DropDown(this, new EventArgs());
                }
                ReRender();
            }

            if (e.KeyInfo.Key == ConsoleKey.DownArrow && DroppedDown)
            {
                SelectedIndex = SelectedIndex == Items.Count - 1? 0 : SelectedIndex + 1;
                ReRender();
            }

            if (e.KeyInfo.Key == ConsoleKey.UpArrow && DroppedDown)
            {
                SelectedIndex = SelectedIndex == 0 ? Items.Count - 1 : SelectedIndex - 1;
                ReRender();
            }
        }

        internal override string VisibleText
        {
            get
            {
                int length = Text.Length;
                var realWidth = Width - 1;
                if (length > realWidth)
                {
                    int startCharIndex = length - realWidth;
                    return Text.Substring(startCharIndex, realWidth);
                }
                return Text;
            }
        }

        public int Width { get; set; }
    }
}