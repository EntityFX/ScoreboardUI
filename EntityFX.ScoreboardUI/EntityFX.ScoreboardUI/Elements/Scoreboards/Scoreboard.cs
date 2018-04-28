using System;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.Menu;
using EntityFX.ScoreboardUI.Elements.Controls.StatusBar;

namespace EntityFX.ScoreboardUI.Elements.Scoreboards
{
    public class Scoreboard : UiElement, ISizable, IBorderable, IDisposable
    {
        public Scoreboard(Panel rootPanel)
        {
            RootPanel = rootPanel;
            RootPanel.Parent = this;
            Title = GetType().Name;
            TitleAligment = TitleAligment.Center;
            IsBorderVisible = true;
            BorderBackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.PrimaryBackgroundColor;
            BorderForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BorderColor;
            Size = new Size
            {
                Height = ScoreboardContext.RenderEngine.RenderOptions.WindowHeight,
                Width = ScoreboardContext.RenderEngine.RenderOptions.WindowWidth
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
                if (value == null)
                {
                    _menu.Parent = null;
                    _menu = null;
                    return;
                }
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
            if (PreNavigate())
            {
                ScoreboardContext.Navigation.Navigate<TScoreboard>(data);
            }
        }

        public void NavigateFromStart<TScoreboard>(object data = null) where TScoreboard : Scoreboard, new()
        {
            if (PreNavigate())
            {
                ScoreboardContext.Navigation.NavigateFromStart<TScoreboard>(data);
            }
        }

        protected virtual bool PreNavigate()
        {
            return true;
        }

        public virtual void GoBack(object data = null)
        {
            if (PreGoBack())
            {
                ScoreboardContext.Navigation.GoBack(data);
            }
        }

        protected virtual bool PreGoBack()
        {
            return true;
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

        protected virtual void OnEscapePressed(object data = null)
        {
            GoBack(data);
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
            var args = new NavigatedEventArgs() { Data = navigationData, NavigationType = navigationType };
            OnScoreboardNavigated(args);
            ScoreboardNavigated(args);
        }

        protected virtual void ScoreboardNavigated(NavigatedEventArgs e)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearEvents();
                RootPanel.Dispose();
            }

            base.Dispose(disposing);
        }

        public override void ClearEvents()
        {
            base.ClearEvents();
            EscapePress = null;
            ScoreboardNavigate = null;
        }
    }
}