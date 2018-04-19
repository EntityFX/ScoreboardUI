using System;

namespace EntityFX.ScoreboardUI
{
    public partial class KeyPressEventArgs : EventArgs
    {
        public ConsoleKeyInfo KeyInfo { get; set; }
    }
}