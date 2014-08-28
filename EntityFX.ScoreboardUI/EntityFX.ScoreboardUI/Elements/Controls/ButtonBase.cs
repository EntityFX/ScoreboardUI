using System;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public abstract class ButtonBase : ControlBase, ILinearable
    {
        public ButtonBase()
        {
            Width = 10;
        }

        public int Width { get; set; }

        internal override string VisibleText
        {
            get
            {
                int length = Text.Length;
                if (length > Width)
                {
                    int startCharIndex = length - Width;
                    return Text.Substring(startCharIndex, Width);
                }
                return base.VisibleText;
            }
        }

        public event EventHandler Pressed;

        public virtual void Press(KeyPressEventArgs e)
        {
            EventHandler handler = Pressed;
            if (handler != null)
            {
                handler(this, new KeyPressEventArgs
                {
                    KeyInfo = e.KeyInfo
                });
            }
            OnPressed(e);
        }

        public override void PressKey(KeyPressEventArgs e)
        {
            base.PressKey(e);
            if (e.KeyInfo.Key == ConsoleKey.Spacebar || e.KeyInfo.Key == ConsoleKey.Enter)
            {
                Press(e);
            }
        }

        protected virtual void OnPressed(KeyPressEventArgs e)
        {
        }
    }
}