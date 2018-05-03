using System;
using System.Threading;
using EntityFX.ScoreboardUI.Elements.MessageBox;
using EntityFX.ScoreboardUI.Elements.Scoreboards;
using EntityFX.ScoreboardUI.Render;

namespace EntityFX.ScoreboardUI
{
    public class UiApplication : IUiApplication
    {
        private Scoreboard _initialScoreboard;

        public UiApplication()
        {
            IsClosed = false;
            FocusKey = ConsoleKey.Tab;
        }

        protected ConsoleKeyInfo PressedKey { get; private set; }

        public void Run(Scoreboard initialScoreboard)
        {
            _initialScoreboard = initialScoreboard;
            ScoreboardContext.Navigation.Navigate(initialScoreboard);

            while (!IsClosed)
            {
                Thread.Sleep(1);
                PerformStepInternal();
            }
        }

        public IRenderOptions RenderOptions
        {
            get => ScoreboardContext.RenderEngine.RenderOptions;
            set => ScoreboardContext.RenderEngine.RenderOptions = value;
        }

        public void Close()
        {
            IsClosed = true;
            if (Closed != null)
            {
                Closed(this, new EventArgs());
            }
        }

        public bool IsClosed { get; private set; }

        public event KeyPressEventHandler KeyPress;

        public event FocusChangeEventHandler FocusChange;

        public ConsoleKey FocusKey { get; set; }

        public event EventHandler<EventArgs> Closed;

        private void PerformStepInternal()
        {
            PressedKey = ScoreboardContext.RenderEngine.ConsoleAdapter.ReadKey(true);
            KeyPressEventHandler handler = KeyPress;
            var keyPressArgs = new KeyPressEventArgs
            {
                KeyInfo = PressedKey
            };
            try
            {
                ScoreboardContext.Navigation.Current.Scoreboard.PressKey(keyPressArgs);
                OnKeyPressed(keyPressArgs);

                if (PressedKey.Key == ConsoleKey.Escape)
                {
                    ScoreboardContext.Navigation.Current.Scoreboard.PressEscape(keyPressArgs);
                }

                if (PressedKey.Key == FocusKey && (PressedKey.Modifiers & ConsoleModifiers.Shift) == 0)
                {
                    ScoreboardContext.CurrentState.NextFocus();
                }

                if (PressedKey.Key == FocusKey && (PressedKey.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    ScoreboardContext.CurrentState.PreviousFocus();
                }

                if (PressedKey.Key == ConsoleKey.R && (PressedKey.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    ScoreboardContext.Navigation.Current.Scoreboard.Render();
                }

                if (handler != null)
                {
                    handler(ScoreboardContext.Navigation.Current.Scoreboard, keyPressArgs);
                }

                CheckNavigationStatck();
                PerformStep();
            }
            catch (Exception e)
            {
                ScoreboardContext.Navigation.Reset();
                if (ScoreboardContext.CurrentState != null)
                {
                    ScoreboardContext.Navigation.Navigate(_initialScoreboard);
                    MessageBox.Alert("Application restarted after critical error", (@enum, o) => { }, "Critical Error", MessageBoxTypeEnum.Error);
                }
            }

        }

        private void CheckNavigationStatck()
        {
            if (ScoreboardContext.Navigation.NavigationStack.Count == 0)
            {
                Close();
            }
        }

        protected virtual void OnKeyPressed(KeyPressEventArgs eventArgs)
        {
        }

        protected virtual void OnFocusKeyPressed()
        {
        }

        protected virtual void PerformStep()
        {
        }
    }
}