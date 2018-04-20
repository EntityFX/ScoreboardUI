using System;

namespace EntityFX.ScoreboardUI.Render
{
    public static class ColorSchemas
    {
        public static ColorScheme Blue
        {
            get
            {
                return new ColorScheme()
                {
                    PrimaryBackgroundColor = ConsoleColor.DarkBlue,
                    PrimaryForegroundColor = ConsoleColor.Yellow,
                    SecondaryBackgroundColor = ConsoleColor.Gray,
                    SecondaryForegroundColor = ConsoleColor.DarkRed,
                    BorderColor = ConsoleColor.Gray,
                    BoxesBackgroundColor = ConsoleColor.White,
                    BoxesForegroundColor = ConsoleColor.DarkBlue,
                    ButtonsBackgroundColor = ConsoleColor.DarkCyan,
                    FocusedBackgroundColor = ConsoleColor.DarkYellow,
                    FocusedForegroundColor = ConsoleColor.Black,
                    DisabledBackgroundColor = ConsoleColor.Gray,
                    DisabledForegroundColor = ConsoleColor.Black,
                    MenuBackgroundColor = ConsoleColor.Gray,
                    MenuForegroundColor = ConsoleColor.DarkBlue,
                    MessageBoxBackgroundColor = ConsoleColor.DarkBlue,
                    MessageBoxForegroundColor = ConsoleColor.Yellow,
                    WarningMessageBoxBackgroundColor = ConsoleColor.Yellow,
                    WarningMessageBoxForegroundColor = ConsoleColor.Black
                };
            }
        }

        public static ColorScheme Matrix { get
            {
                return new ColorScheme()
                {
                    PrimaryBackgroundColor = ConsoleColor.Black,
                    PrimaryForegroundColor = ConsoleColor.Green,
                    SecondaryBackgroundColor = ConsoleColor.Gray,
                    SecondaryForegroundColor = ConsoleColor.Green,
                    BorderColor = ConsoleColor.Green,
                    BoxesBackgroundColor = ConsoleColor.Gray,
                    BoxesForegroundColor = ConsoleColor.Green,
                    ButtonsBackgroundColor = ConsoleColor.Green,
                    ButtonsForegroundColor = ConsoleColor.White,
                    FocusedBackgroundColor = ConsoleColor.Gray,
                    FocusedForegroundColor = ConsoleColor.Black,
                    DisabledBackgroundColor = ConsoleColor.Black,
                    DisabledForegroundColor = ConsoleColor.Gray,
                    MenuBackgroundColor = ConsoleColor.Gray,
                    MenuForegroundColor = ConsoleColor.Green,
                    MessageBoxBackgroundColor = ConsoleColor.Black,
                    MessageBoxForegroundColor = ConsoleColor.Green,
                    WarningMessageBoxBackgroundColor = ConsoleColor.Yellow,
                    WarningMessageBoxForegroundColor = ConsoleColor.Black
                };
            }
        }
    }
    
}