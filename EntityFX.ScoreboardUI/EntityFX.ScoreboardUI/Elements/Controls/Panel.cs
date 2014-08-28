using System;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Panel : Control<ControlBase>, ISizable
    {
        public Panel()
        {
            DisplayText = false;
        }
        public ConsoleColor BorderColor { get; set; }
        public Size Size { get; set; }
    }
}