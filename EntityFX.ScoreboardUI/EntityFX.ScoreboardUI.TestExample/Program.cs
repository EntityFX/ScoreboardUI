using System;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFx.ScorbordUI.TestExample
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            IUiApplication application = new UiApplication();
            Scoreboard initialScoreboard = new MainScoreboard();
            application.Run(initialScoreboard);
        }
    }
}
