using System;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class ProgressBar : ControlBase, IProgressBar, ILinearable
    {
        private readonly object _valueLockObject = new object();
        private int _maximumInternal;
        private int _minimumInternal;

        private int _valueInternal;

        public ProgressBar()
        {
            Minimum = 0;
            Maximum = 100;
            Step = 1;
            Width = 20;
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.SecondaryBackgroundColor;
            StripeColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.SecondaryForegroundColor;
            Text = "Complete {0} %";
            TextAligment = TitleAligment.Center;
        }

        public TitleAligment TextAligment { get; set; }

        public int Minimum
        {
            get { return _minimumInternal; }
            set
            {
                if (value > Maximum)
                    throw new ArgumentOutOfRangeException(
                        "Minimum",
                        value, string.Format(
                            "Minimum with value {0} is above than maximum value", value));
                _minimumInternal = value;
            }
        }

        public int Maximum
        {
            get { return _maximumInternal; }
            set
            {
                if (value < Minimum)
                    throw new ArgumentOutOfRangeException(
                        "Maximum",
                        value, string.Format(
                            "Maximum with value {0} is above than minimum value", value));
                _maximumInternal = value;
            }
        }

        public int Step { get; set; }

        public int Width { get; set; }

        public int Value
        {
            get { return _valueInternal; }
            set
            {
                if (value > Maximum || value < Minimum)
                    throw new ArgumentOutOfRangeException(
                        "Value",
                        value, "Value {0} is out of range");
                lock (_valueLockObject)
                {
                    _valueInternal = value;
                    EventHandler<int> handler = ValueChanged;
                    if (handler != null)
                    {
                        handler(this, _valueInternal);
                    }
                    ReRender();
                }
            }
        }

        public ConsoleColor StripeColor { get; set; }

        internal override string VisibleText
        {
            get
            {
                string text = string.Format(Text, Value);
                int length = text.Length;
                if (length > Width)
                {
                    int startCharIndex = length - Width;
                    return Text.Substring(startCharIndex, Width);
                }
                return text;
            }
        }

        public event EventHandler<int> ValueChanged;

        public void Increment(int? value = null)
        {
            int newValue;
            lock (_valueLockObject)
            {
                newValue = Value + (value ?? Step);
            }
            if (newValue > Maximum)
            {
                newValue = Maximum;
            }
            Value = newValue;
        }

        public void Decrement(int? value = null)
        {
            int newValue;
            lock (_valueLockObject)
            {
                newValue = Value - (value ?? Step);
            }
            if (newValue < Minimum)
            {
                newValue = Minimum;
            }
            Value = newValue;
        }

        public override void ClearEvents()
        {
            base.ClearEvents();
            ValueChanged = null;
        }
    }
}