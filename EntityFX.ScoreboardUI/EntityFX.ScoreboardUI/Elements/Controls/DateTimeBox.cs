using System;
using System.Runtime.Serialization;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class DateTimeBox : TextBox
    {
        public string Format { get; set; } = "d";
        public DateTime DateTime { get; set; } = DateTime.Today;

        private DateTimeFormat _dateTimeFormat;

        public DateTimeBox() : base()
        {
            _dateTimeFormat = new DateTimeFormat(Format);

            if (Text != "")
            {
                if (_dateTimeFormat != null)
                {
                    DateTime dt = DateTime;
                    if (!DateTime.TryParse(Text, _dateTimeFormat.FormatProvider, _dateTimeFormat.DateTimeStyles, out dt))
                    {
                        dt = DateTime;
                    }
                    DateTime = dt;
                }
                Text = DateTime.ToString(Format);
            }
        }

        public override void OnFocusChanged(bool isFocused)
        {
            base.OnFocusChanged(isFocused);

            if (!isFocused)
            {
                if (_dateTimeFormat != null)
                {
                    DateTime dt;
                    DateTime.TryParse(Text, _dateTimeFormat.FormatProvider, _dateTimeFormat.DateTimeStyles, out dt);
                    DateTime = dt;
                }
                Text = DateTime.ToString(Format);
            }
        }
    }
}