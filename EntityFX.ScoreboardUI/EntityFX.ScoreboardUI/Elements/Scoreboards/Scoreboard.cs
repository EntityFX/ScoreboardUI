using System;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.Menu;
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

        private Menu _menu;
        public Menu Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                _menu.Parent = this;
                var stripLocation = new Point
                {
                    Left = IsBorderVisible ? 1 : 0,
                    Top = 1
                };
                _menu.Location = stripLocation;
            }
        }
        
        public event EventHandler<NavigatedEventArgs> ScoreboardNavigate;

        public event EventHandler EscapePress;

        public void Navigate<TScoreboard>(object data = null) where TScoreboard : Scoreboard, new()
        {
            ScoreboardContext.Navigation.Navigate<TScoreboard>(data);
        }

        public virtual void GoBack(object data = null)
        {
            ScoreboardContext.Navigation.GoBack(data);
        }

        public override void PressKey(KeyPressEventArgs e)
        {
            base.PressKey(e);
            var focusedElement = ScoreboardContext.CurrentState.FocusedElement;
            if (focusedElement != null)
                focusedElement.PressKey(e);
        }



        
        protected virtual void OnScoreboardNavigated(NavigatedEventArgs e)
        {
            EventHandler<NavigatedEventArgs> handler = ScoreboardNavigate;
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
        
        internal void NavigateInternal(NavigationType navigationType, object navigationData)
        {
        	OnScoreboardNavigated(new NavigatedEventArgs() { Data = navigationData, NavigationType = navigationType});
        	ScoreboardNavigated(navigationData);
        }
        
        protected virtual void ScoreboardNavigated(object navigationData)
        {
        	
        }

        protected override void PostRender()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var rootPanelControl in RootPanel.Controls)
            {
                rootPanelControl.Initialize();
            }
        }
    }
}