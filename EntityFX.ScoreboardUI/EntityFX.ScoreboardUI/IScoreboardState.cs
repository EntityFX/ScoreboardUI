using System.Collections.Generic;
using EntityFX.ScoreboardUI.Elements.Controls;

namespace EntityFX.ScoreboardUI
{
    public interface IScoreboardState
    {
        int InternalTabIndex { get; }

        IEnumerable<ControlBase> SortedControlsByFocusIndex { get; }

        IEnumerable<ControlBase> ControlsList { get; }

        bool ScoreboardInitialized { get; }

        bool IsNavigating { get; set; }

        ControlBase FocusedElement { get; }
        void ChangeFocus(ControlBase control);

        void Initialized();

        void AddToControlList(ControlBase control);

        void NextFocus();

        void PreviousFocus();

        void ResetFocus();
    }
}