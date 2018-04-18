using System;

namespace EntityFX.ScoreboardUI
{
    public class KeyPressEventArgs : EventArgs
    {
        public ConsoleKeyInfo KeyInfo { get; set; }
    }
}