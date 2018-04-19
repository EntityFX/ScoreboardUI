using System;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Button : ButtonBase
    {
        public Button()
        {
            TextAligment = TitleAligment.Center;
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.ButtonsBackgroundColor;
            ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.ButtonsForegroundColor;
        }

        public TitleAligment TextAligment { get; set; }
    }
}