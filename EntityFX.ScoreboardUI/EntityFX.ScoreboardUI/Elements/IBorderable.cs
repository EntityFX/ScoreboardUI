using System;

namespace EntityFX.ScoreboardUI.Elements
{
    public interface IBorderable
    {
        bool IsBorderVisible { get; set; }

        ConsoleColor BorderBackgroundColor { get; set; }

        ConsoleColor BorderForegroundColor { get; set; }
    }
}