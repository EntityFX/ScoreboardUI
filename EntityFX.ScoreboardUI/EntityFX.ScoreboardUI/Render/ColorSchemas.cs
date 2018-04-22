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
                    WarningMessageBoxBackgroundColor = ConsoleColor.DarkYellow,
                    WarningMessageBoxForegroundColor = ConsoleColor.Black
                };
            }
        }

        public static ColorScheme Matrix { get
            {
                return new ColorScheme()
                {
                    PrimaryBackgroundColor = ConsoleColor.Black,
                    PrimaryForegroundColor = ConsoleColor.DarkGreen,
                    SecondaryBackgroundColor = ConsoleColor.Gray,
                    SecondaryForegroundColor = ConsoleColor.DarkGreen,
                    BorderColor = ConsoleColor.DarkGreen,
                    BoxesBackgroundColor = ConsoleColor.Gray,
                    BoxesForegroundColor = ConsoleColor.DarkGreen,
                    ButtonsBackgroundColor = ConsoleColor.DarkGreen,
                    ButtonsForegroundColor = ConsoleColor.White,
                    FocusedBackgroundColor = ConsoleColor.Gray,
                    FocusedForegroundColor = ConsoleColor.Black,
                    DisabledBackgroundColor = ConsoleColor.Black,
                    DisabledForegroundColor = ConsoleColor.Gray,
                    MenuBackgroundColor = ConsoleColor.Gray,
                    MenuForegroundColor = ConsoleColor.DarkGreen,
                    MessageBoxBackgroundColor = ConsoleColor.Black,
                    MessageBoxForegroundColor = ConsoleColor.DarkGreen,
                    WarningMessageBoxBackgroundColor = ConsoleColor.DarkYellow,
                    WarningMessageBoxForegroundColor = ConsoleColor.Black,
                    ErrorMessageBoxBackgroundColor = ConsoleColor.DarkRed,
                    ErrorMessageBoxForegroundColor = ConsoleColor.Black
                };
            }
        }
    }
    
}