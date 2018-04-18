using System;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFx.ScorbordUI.TestExample
{
    class SecondScoreboard : Scoreboard
    {
        private Checkbox _checkbox1;

        private Image _img1;

        public SecondScoreboard()
            : base(new Panel())
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            _checkbox1 = new Checkbox { Location = new Point { Left = 3, Top = 4 }, Text = "Checkbox tentative" };
            RootPanel.AddChild(_checkbox1);

            _img1 = new Image()
            {
            };
        }

        protected override void OnKeyPressed(KeyPressEventArgs e)
        {
            if (e.KeyInfo.Key == ConsoleKey.D1)
            {
                Navigate<FourthScoreboard>();
            }
        }
        
        protected override void OnEscapePressed()
        {
            GoBack(255);
        }
    }
}