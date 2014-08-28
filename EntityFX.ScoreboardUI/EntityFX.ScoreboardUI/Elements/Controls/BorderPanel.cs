using System;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class BorderPanel : Panel, IBorderable
    {

        public BorderPanel()
        {
            BorderBackgroundColor = ConsoleColor.Blue;
            BorderForegroundColor = ConsoleColor.Gray;
            DisplayText = true;
        }
        public bool IsBorderVisible { get; set; }
        public ConsoleColor BorderBackgroundColor { get; set; }
        public ConsoleColor BorderForegroundColor { get; set; }
    }
}