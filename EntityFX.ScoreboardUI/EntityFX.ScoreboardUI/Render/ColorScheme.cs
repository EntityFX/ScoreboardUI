﻿using System;

namespace EntityFX.ScoreboardUI.Render
{
    public class ColorScheme
    {
        public ConsoleColor PrimaryBackgroundColor { get; set; }
        public ConsoleColor PrimaryForegroundColor { get; set; }
        public ConsoleColor SecondaryBackgroundColor { get; set; }
        public ConsoleColor SecondaryForegroundColor { get; set; }
        public ConsoleColor BorderColor { get; set; }
        public ConsoleColor BoxesBackgroundColor { get; set; }
        public ConsoleColor BoxesForegroundColor { get; set; }
        public ConsoleColor ButtonsBackgroundColor { get; set; }
        public ConsoleColor FocusedBackgroundColor { get; internal set; }
        public ConsoleColor FocusedForegroundColor { get; internal set; }
        public ConsoleColor DisabledBackgroundColor { get; set; }
        public ConsoleColor DisabledForegroundColor { get; set; }
        public ConsoleColor MenuForegroundColor { get; set; }
        public ConsoleColor MenuBackgroundColor { get; set; }
    }
}