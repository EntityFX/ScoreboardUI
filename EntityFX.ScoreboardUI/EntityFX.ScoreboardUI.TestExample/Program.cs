using System;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Elements.Scoreboards;
using EntityFX.ScoreboardUI.Render;

namespace EntityFx.ScorbordUI.TestExample
{
    class Program
    {
        static void Main()
        {
            IUiApplication application = new UiApplication();
            application.RenderOptions.ColorScheme = ColorSchemas.Blue;
            Scoreboard initialScoreboard = new MainScoreboard();

            application.Run(initialScoreboard);
        }
    }
}
