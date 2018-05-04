using System;
using System.Globalization;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class NumericBox : TextBox
    {
        private decimal? _numberValue = null;

        public Decimal? NumberValue
        {
            get { return _numberValue; }
            set
            {
                _numberValue = value;
                if (value != null)
                {
                    Text = value.ToString();
                }
            }
        }

        public NumericBox() : base()
        {
            InitValue();
        }

        public override void Initialize()
        {
            base.Initialize();
            InitValue();
        }

        private void InitValue()
        {
            if (Text != "")
            {

                Decimal dt = 0;
                if (!Decimal.TryParse(Text, NumberStyles.Number, CultureInfo.CurrentCulture, out dt))
                {
                    dt = 0;
                }
                NumberValue = dt;
                Text = NumberValue.ToString();
            }
        }

        public override void OnFocusChanged(bool isFocused)
        {
            base.OnFocusChanged(isFocused);

            if (!isFocused && !string.IsNullOrWhiteSpace(Text))
            {
                Decimal dt;
                Decimal.TryParse(Text, NumberStyles.Number, CultureInfo.CurrentCulture, out dt);
                NumberValue = dt;
                Text = NumberValue.ToString();
            }
        }

        public override void PressKey(KeyPressEventArgs e)
        {
            if ((
                (e.KeyInfo.KeyChar >= '0' && e.KeyInfo.KeyChar <= '9') ||
                (e.KeyInfo.KeyChar.ToString() == CultureInfo.CurrentCulture.NumberFormat.NegativeSign && Text == "") ||
                e.KeyInfo.KeyChar.ToString() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator && !Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))

                && !Char.IsControl(e.KeyInfo.KeyChar))
            {
                base.PressKey(e);
                if (Text != CultureInfo.CurrentCulture.NumberFormat.NegativeSign &&       
                    !Text.EndsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
                {
                    Decimal dt; 
                    Decimal.TryParse(Text, NumberStyles.Number, CultureInfo.CurrentCulture, out dt);
                    NumberValue = dt;
                    Text = NumberValue != null ? ((decimal)NumberValue).ToString(CultureInfo.CurrentCulture) : "";
                }
            }
            else if (Char.IsControl(e.KeyInfo.KeyChar))
            {
                base.PressKey(e);
            }

            if (e.KeyInfo.Key == ConsoleKey.UpArrow)
            {
                NumberValue++;
                Text = NumberValue.ToString();
            }

            if (e.KeyInfo.Key == ConsoleKey.DownArrow)
            {
                NumberValue--;
                Text = NumberValue.ToString();
            }
        }
    }
}