using System;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class TextBox : ControlBase, IBorderable
    {
        public TextBox()
        {
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BoxesBackgroundColor;
            ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BoxesForegroundColor; ;
            InputLength = 15;
            MaxTextSize = 30;
        }

        public int InputLength { get; set; }

        public int MaxTextSize { get; set; }

        internal override string VisibleText
        {
            get
            {
                string text = IsFocused ? Text + '_' : Text;
                int length = text.Length;
                if (length > InputLength)
                {
                    int startCharIndex = length - InputLength;
                    return text.Substring(startCharIndex, InputLength);
                }
                return text;
            }
        }

        public bool IsBorderVisible { get; set; }
        public ConsoleColor BorderBackgroundColor { get; set; }
        public ConsoleColor BorderForegroundColor { get; set; }

        public override void PressKey(KeyPressEventArgs e)
        {
            base.PressKey(e);

            char character = e.KeyInfo.KeyChar;

            int length = Text.Length;
            if (!Char.IsControl(character) && length <= MaxTextSize)
            {
                Text += character;
            }
            else
            {
                if (e.KeyInfo.Key == ConsoleKey.Backspace && length > 0)
                {
                    Text = Text.Remove(length - 1, 1);
                }
            }
            ReRender();
        }
    }
}