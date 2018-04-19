using System;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class BorderPanel : Panel, IBorderable
    {

        public BorderPanel()
        {
            BorderBackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.PrimaryBackgroundColor;
            BorderForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BorderColor;
            DisplayText = true;
        }
        public bool IsBorderVisible { get; set; }
        public ConsoleColor BorderBackgroundColor { get; set; }
        public ConsoleColor BorderForegroundColor { get; set; }
    }
}