using System;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public interface IProgressBar
    {
        int Minimum
        {
            get;
            set;
        }

        int Maximum
        {
            get;
            set;
        }

        int Step { get; set; }

        int Width { get; set; }

        int Value
        {
            get;
            set;
        }

        ConsoleColor StripeColor { get; set; }

        event EventHandler<int> ValueChanged;

        void Increment(int? value = null);

        void Decrement(int? value = null);
    }
}