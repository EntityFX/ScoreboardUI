using System.Collections.Generic;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI
{
    internal interface IStoreboardNavigationEngine
    {
        Stack<Scoreboard> NavigationStack { get; }

        StateItem Previous { get; }

        StateItem Current { get; set; }

        void GoBack();

        void Navigate<TScoreboard>() where TScoreboard : Scoreboard, new();

        void Navigate(Scoreboard scoreboard);
    }
}