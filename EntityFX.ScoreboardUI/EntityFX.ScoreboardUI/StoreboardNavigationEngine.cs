using System.Collections.Generic;
using System.Linq;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI
{
    internal class StoreboardNavigationEngine : IStoreboardNavigationEngine
    {
        private readonly Stack<StateItem> _scoreboardNavigationStack = new Stack<StateItem>();

        public Stack<Scoreboard> NavigationStack
        {
            get { return new Stack<Scoreboard>(_scoreboardNavigationStack.Select(_ => _.Scoreboard)); }
        }

        public StateItem Previous { get; private set; }

        public StateItem Current { get; set; }

        public void GoBack(object data = null)
        {
            Previous = _scoreboardNavigationStack.Pop();
            if (_scoreboardNavigationStack.Count != 0)
            {
                StateItem stateItem = _scoreboardNavigationStack.Peek();
                stateItem.NavigationData = data;
                Current = stateItem;
                Current.Scoreboard.IsVisible = true;
                Current.Scoreboard.NavigateInternal(NavigationType.GoBack, stateItem.NavigationData);
                Current.Scoreboard.Render();
            }
            else
            {
                Current = null;
            }
        }

        public void Navigate<TScoreboard>(object data = null) where TScoreboard : Scoreboard, new()
        {
            StateItem firstOrDefault =
                _scoreboardNavigationStack.FirstOrDefault(_ => _.Scoreboard is TScoreboard);
            Scoreboard scoreboard;
            StateItem state = null;
            if (firstOrDefault != null)
            {
                scoreboard = firstOrDefault.Scoreboard;

                NavigateBackward(firstOrDefault);
                state = firstOrDefault;
            }
            else
            {
                Current.ScoreboardState.IsNavigating = true;
                scoreboard = new TScoreboard();
            }
            Navigate(scoreboard, state, data);
        }

        public void NavigateFromStart<TScoreboard>(object data = null) where TScoreboard : Scoreboard, new()
        {
            Current.ScoreboardState.IsNavigating = true;
            NavigateBackward(_scoreboardNavigationStack.First());
            Scoreboard scoreboard = new TScoreboard();
            Navigate(scoreboard, null, data);
        }

        private void NavigateBackward(StateItem stateItem)
        {
            var scoreboard = stateItem.Scoreboard;

            if (scoreboard != null)
            {
                Scoreboard popScoreboard = null;
                do
                {
                    popScoreboard = _scoreboardNavigationStack.Pop().Scoreboard;
                    popScoreboard.Dispose();
                } while (popScoreboard != null && scoreboard != popScoreboard);
            }

            scoreboard.IsVisible = true;
        }

        public void Navigate(Scoreboard scoreboard, StateItem state = null, object data = null)
        {
            Current.ScoreboardState.IsNavigating = true;
            var stateItem = state ?? new StateItem
            {
                Scoreboard = scoreboard,
                ScoreboardState = new ScoreboardState(),
                NavigationData = data
            };

            Current = stateItem;

            if (_scoreboardNavigationStack.Count > 0)
            {
                StateItem currentStackItem = _scoreboardNavigationStack.Peek();
                Previous = currentStackItem;
                Previous.Scoreboard.IsVisible = false;
            }

            _scoreboardNavigationStack.Push(stateItem);
            if (!Current.ScoreboardState.ScoreboardInitialized) Current.Scoreboard.InitializeInternal();
            Current.Scoreboard.NavigateInternal(NavigationType.Navigate, stateItem.NavigationData);
            ScoreboardContext.CurrentState.ResetFocus();
            Current.ScoreboardState.IsNavigating = false;
            scoreboard.Render();
        }
    }
}