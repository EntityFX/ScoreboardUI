using System;

namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public class StatusStripButton : StatusStripItem
    {
        public StatusStripButton()
        {
            BackgroundColor = ConsoleColor.DarkGreen;
            InternalControl = new Button
            {
                Text = Text,
                Parent = this
            };
        }
    }
}