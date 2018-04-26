using System;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFx.ScorbordUI.TestExample
{
    class FourthScoreboard : Scoreboard
    {
        public FourthScoreboard()
            : base(new Panel())
        {
        }

        protected override void OnKeyPressed(KeyPressEventArgs e)
        {
            if (e.KeyInfo.Key == ConsoleKey.D1)
            {
                Navigate<ThirdScoreboard>();
            }

            if (e.KeyInfo.Key == ConsoleKey.D2)
            {
                Navigate<MainScoreboard>();
            }
        }
    }
}