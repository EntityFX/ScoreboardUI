using System;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.StatusBar;

namespace EntityFX.ScoreboardUI.Elements.Scoreboards
{
    public class Scoreboard : UiElement, ISizable, IBorderable
    {
        public Scoreboard(Panel rootPanel)
        {
            RootPanel = rootPanel;
            RootPanel.Parent = this;
            Title = GetType().Name;
            TitleAligment = TitleAligment.Center;
            IsBorderVisible = true;
            BorderBackgroundColor = ConsoleColor.Blue;
            BorderForegroundColor = ConsoleColor.Gray;
            Size = new Size
            {
                Height = Console.WindowHeight,
                Width = Console.WindowWidth
            };
        }

        public string Title { get; set; }

        public TitleAligment TitleAligment { get; set; }

        public Panel RootPanel { get; internal set; }
        public bool IsBorderVisible { get; set; }

        public ConsoleColor BorderBackgroundColor { get; set; }
        public ConsoleColor BorderForegroundColor { get; set; }
        public Size Size { get; set; }

        private StatusStrip _statusStrip;
        public StatusStrip StatusStrip
        {
            get { return _statusStrip; }
            set
            {
                _statusStrip = value;
                _statusStrip.Parent = this;
                var stripLocation = new Point
                {
                    Left = IsBorderVisible ? 1 : 0,
                    Top = Size.Height - 1
                };
                _statusStrip.Location = stripLocation;
            }
        }

        public event EventHandler<EventArgs> Initialized;

        public event EventHandler EscapePress;

        public void Navigate<TScoreboard>() where TScoreboard : Scoreboard, new()
        {
            ScoreboardContext.Navigation.Navigate<TScoreboard>();
        }

        public void GoBack()
        {
            ScoreboardContext.Navigation.GoBack();
        }

        public override void PressKey(KeyPressEventArgs e)
        {
            base.PressKey(e);
            var focusedElement = ScoreboardContext.CurrentState.FocusedElement;
            if (focusedElement != null)
                focusedElement.PressKey(e);
        }

        protected virtual void Initialize()
        {
        }

        protected virtual bool PreInitialize()
        {
            return true;
        }

        protected virtual void PostInitialize()
        {
        }

        protected virtual void OnInitialized(EventArgs e)
        {
            EventHandler<EventArgs> handler = Initialized;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void PressEscape(KeyPressEventArgs e)
        {
            EventHandler handler = EscapePress;
            if (handler != null)
            {
                handler(this, new KeyPressEventArgs
                {
                    KeyInfo = e.KeyInfo
                });
            }
            OnEscapePressed();
        }

        protected virtual void OnEscapePressed()
        {
            GoBack();
        }

        internal void InitializeInternal()
        {
            if (PreInitialize())
            {
                Initialize();
                OnInitialized(new EventArgs());
                ScoreboardContext.CurrentState.Initialized();
                PostInitialize();
            }
        }

        protected override void PostRender()
        {
        }
    }
}