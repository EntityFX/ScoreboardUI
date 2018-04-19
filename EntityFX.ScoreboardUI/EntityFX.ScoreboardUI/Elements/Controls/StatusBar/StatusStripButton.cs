using System;

namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public class StatusStripButton : StatusStripItem
    {
        public StatusStripButton()
        {
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.ButtonsBackgroundColor;
            ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.ButtonsForegroundColor;
            InternalControl = new Button
            {
                Text = Text,
                Parent = this
            };
        }
    }
}