using System;

namespace EntityFX.ScoreboardUI
{
    public class NavigatedEventArgs : EventArgs
    {
        public object Data { get; set; }

        public NavigationType NavigationType { get; set; }
    }

}
