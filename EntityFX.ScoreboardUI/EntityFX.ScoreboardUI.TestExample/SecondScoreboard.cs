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
            _checkbox1 = new Checkbox { Location = new Point { Left = 3, Top = 3 }, Text = "Checkbox tentative" };
            RootPanel.AddChild(_checkbox1);

            _img1 = new Image()
            {
                Size = new Size() {  Height = 4, Width = 24},
                Location = new Point() {  Left = 3, Top = 10}
            };

            var imageData = new (ConsoleColor Color, char Char)[_img1.Size.Height, _img1.Size.Width];
            for (int y = 0; y < _img1.Size.Height; y++)
            {
                for (int x = 0; x < _img1.Size.Width; x++)
                {
                    ConsoleColor c = (ConsoleColor) (x / 3 + (_img1.Size.Width / 3) * (y / 2));
                    imageData[y,x] = (Color: c, Char : ' ');
                }
            }
            _img1.ImageArray = imageData;
            _img1.UseColors = true;
            _img1.UseInvertedColors = true;
            RootPanel.AddChild(_img1);
        }

        protected override void OnKeyPressed(KeyPressEventArgs e)
        {
            if (e.KeyInfo.Key == ConsoleKey.D1)
            {
                Navigate<FourthScoreboard>();
            }
        }
        
        protected override void OnEscapePressed(object data = null)
        {
            GoBack(255);
        }
    }
}