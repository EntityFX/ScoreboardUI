using System.Collections.Generic;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI
{
    internal interface IStoreboardNavigationEngine
    {
        Stack<Scoreboard> NavigationStack { get; }

        StateItem Previous { get; }

        StateItem Current { get; set; }

        void GoBack(object data = null);

        void Navigate<TScoreboard>(object data = null) where TScoreboard : Scoreboard, new();

        void NavigateFromStart<TScoreboard>(object data = null) where TScoreboard : Scoreboard, new();

        void Navigate(Scoreboard scoreboard, StateItem state = null, object data = null);
    }
}