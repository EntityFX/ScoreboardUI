using System;
using System.Runtime.Serialization;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class DateTimeBox : TextBox
    {
        public string Format { get; set; } = "d";

        public DateTime? DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                if (value != null)
                {
                    Text = DateTime?.ToString(Format);
                }
            }
        } 

        private DateTimeFormat _dateTimeFormat;
        private DateTime? _dateTime;

        public DateTimeBox() : base()
        {
            _dateTimeFormat = new DateTimeFormat(Format);

            Initialize();
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

                if (_dateTimeFormat != null && DateTime != null)
                {
                    DateTime initial = (DateTime)DateTime;
                    DateTime dt;
                    if (!System.DateTime.TryParse(Text, _dateTimeFormat.FormatProvider, _dateTimeFormat.DateTimeStyles, out dt))
                    {
                        dt = initial;
                    }
                    DateTime = dt;
                }
                Text = DateTime?.ToString(Format);
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
                    System.DateTime.TryParse(Text, _dateTimeFormat.FormatProvider, _dateTimeFormat.DateTimeStyles, out dt);
                    DateTime = dt;
                }
                Text = DateTime?.ToString(Format);
            }
        }
    }
}