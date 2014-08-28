using System;

namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public class StatusStripLabel : StatusStripItem
    {
        public StatusStripLabel()
        {
            BackgroundColor = ConsoleColor.DarkGray;
            InternalControl = new Label()
            {
                Parent = this
            };
        }
    }
}