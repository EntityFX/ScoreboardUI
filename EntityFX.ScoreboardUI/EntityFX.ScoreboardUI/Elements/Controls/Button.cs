using System;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Button : ButtonBase
    {
        public Button()
        {
            TextAligment = TitleAligment.Center;
            BackgroundColor = ConsoleColor.DarkGreen;
        }

        public TitleAligment TextAligment { get; set; }
    }
}