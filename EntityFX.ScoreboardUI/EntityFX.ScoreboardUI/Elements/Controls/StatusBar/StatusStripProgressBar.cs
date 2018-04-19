using System;

namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public class StatusStripProgressBar : StatusStripItem, IProgressBar, ILinearable
    {
        private readonly ProgressBar _progressBar;
        
        public StatusStripProgressBar()
        {
            InternalControl = new ProgressBar
            {
                Text = Text,
                Parent = this
            };
            _progressBar = (ProgressBar)InternalControl;
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.SecondaryBackgroundColor;
            StripeColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.SecondaryForegroundColor;
        }

        public int Minimum
        {
            get { return _progressBar.Minimum; }
            set { _progressBar.Minimum = value; }
        }

        public int Maximum
        {
            get { return _progressBar.Maximum; }
            set { _progressBar.Maximum = value; }
        }

        public int Step
        {
            get { return _progressBar.Step; }
            set { _progressBar.Step = value; }
        }

        public int Width
        {
            get { return _progressBar.Width; }
            set { _progressBar.Width = value; }
        }

        public int Value
        {
            get { return _progressBar.Value; }
            set { _progressBar.Value = value; }
        }

        public ConsoleColor StripeColor
        {
            get { return _progressBar.StripeColor; }
            set { _progressBar.StripeColor = value; }
        }

        public event EventHandler<int> ValueChanged
        {
            add { _progressBar.ValueChanged += value; }
            remove { _progressBar.ValueChanged -= value; }
        }

        public void Increment(int? value = null)
        {
            _progressBar.Increment(value);
        }

        public void Decrement(int? value = null)
        {
            _progressBar.Decrement(value);
        }
    }
}