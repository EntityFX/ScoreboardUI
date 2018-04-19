using System;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFx.ScorbordUI.TestExample
{
    class Program
    {
        static void Main()
        {
            IUiApplication application = new UiApplication();
            Scoreboard initialScoreboard = new MainScoreboard();
            application.Run(initialScoreboard);
        }
    }
}
