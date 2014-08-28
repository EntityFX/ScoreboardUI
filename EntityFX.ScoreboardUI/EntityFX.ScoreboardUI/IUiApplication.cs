using System;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI
{
    public interface IUiApplication
    {
        bool IsClosed { get; }

        ConsoleKey FocusKey { get; set; }
        void Run(Scoreboard initialScoreboard);

        void Close();

        event KeyPressEventHandler KeyPress;

        event FocusChangeEventHandler FocusChange;

        event EventHandler<EventArgs> Closed;
    }
}