using System;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class ComboBox : ListControlBase<ComboBoxItem>, ILinearable
    {
        public ComboBox()
        {
            Width = 15;
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BoxesBackgroundColor;
            ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BoxesForegroundColor;
            ExpanderBackground = ConsoleColor.Gray;
            SelectedColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.FocusedForegroundColor;
            VisibleItemsCount = 5;
        }

        public ConsoleColor ExpanderBackground { get; set; }

        public ConsoleColor SelectedColor { get; set; }

        public bool DroppedDown { get; set; }

        public int VisibleItemsCount { get; set; }

        public event EventHandler DropDown;

        public event EventHandler DropClosed;

        public override string Text
        {
            get { return SelectedIndex == -1 || Items.Count == 0 ? string.Empty : Items[SelectedIndex].Text; }
            set
            {
                if (SelectedIndex >= 0)
                {
                    Items[SelectedIndex].Text = value;
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
                OnSelectedValueChanged(SelectedValue);
                ReRender();
            }

            if (e.KeyInfo.Key == ConsoleKey.UpArrow && DroppedDown)
            {
                SelectedIndex = SelectedIndex == 0 ? Items.Count - 1 : SelectedIndex - 1;
                OnSelectedValueChanged(SelectedValue);
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

        public override void ClearEvents()
        {
            base.ClearEvents();
            DropClosed = null;
            DropDown = null;
        }
    }
}