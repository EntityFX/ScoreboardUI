﻿using System;
using System.Threading;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI
{
    public class UiApplication : IUiApplication
    {
        public UiApplication()
        {
            IsClosed = false;
            FocusKey = ConsoleKey.Tab;
        }

        protected ConsoleKeyInfo PressedKey { get; private set; }

        public void Run(Scoreboard initialScoreboard)
        {
            ScoreboardContext.Navigation.Navigate(initialScoreboard);
            while (!IsClosed)
            {
                Thread.Sleep(1);
                PerformStepInternal();
            }
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
            PressedKey = Console.ReadKey(true);
            KeyPressEventHandler handler = KeyPress;
            var keyPressArgs = new KeyPressEventArgs
            {
                KeyInfo = PressedKey
            };
            ScoreboardContext.Navigation.Current.Scoreboard.PressKey(keyPressArgs);
            OnKeyPressed(keyPressArgs);

            if (PressedKey.Key == ConsoleKey.Escape)
            {
                ScoreboardContext.Navigation.Current.Scoreboard.PressEscape(keyPressArgs);
            }

            if (PressedKey.Key == FocusKey)
            {
                ScoreboardContext.CurrentState.NextFocus();
            }

            if (handler != null)
            {
                handler(ScoreboardContext.Navigation.Current.Scoreboard, keyPressArgs);
            }

            CheckNavigationStatck();
            PerformStep();
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