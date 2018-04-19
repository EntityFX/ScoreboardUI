using System;

namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public class StatusStripLabel : StatusStripItem
    {
        public StatusStripLabel()
        {
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MenuBackgroundColor;
            InternalControl = new Label()
            {
                Parent = this,
                ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MenuForegroundColor
            };
        }
    }
}