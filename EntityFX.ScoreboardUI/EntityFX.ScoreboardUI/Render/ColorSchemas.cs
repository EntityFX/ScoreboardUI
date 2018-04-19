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
                    SecondaryBackgroundColor = ConsoleColor.DarkGray,
                    SecondaryForegroundColor = ConsoleColor.DarkRed,
                    BorderColor = ConsoleColor.DarkGray,
                    BoxesBackgroundColor = ConsoleColor.White,
                    BoxesForegroundColor = ConsoleColor.DarkBlue,
                    ButtonsBackgroundColor = ConsoleColor.DarkCyan,
                    FocusedBackgroundColor = ConsoleColor.DarkYellow,
                    FocusedForegroundColor = ConsoleColor.White,
                    DisabledBackgroundColor = ConsoleColor.DarkGray,
                    DisabledForegroundColor = ConsoleColor.Gray,
                    MenuBackgroundColor = ConsoleColor.DarkGray,
                    MenuForegroundColor = ConsoleColor.DarkBlue,
                };
            }
        }

        public static ColorScheme Matrix { get
            {
                return new ColorScheme()
                {
                    PrimaryBackgroundColor = ConsoleColor.Black,
                    PrimaryForegroundColor = ConsoleColor.Green,
                    SecondaryBackgroundColor = ConsoleColor.DarkGray,
                    SecondaryForegroundColor = ConsoleColor.Green,
                    BorderColor = ConsoleColor.Green,
                    BoxesBackgroundColor = ConsoleColor.DarkGray,
                    BoxesForegroundColor = ConsoleColor.Green,
                    ButtonsBackgroundColor = ConsoleColor.DarkGreen,
                    FocusedBackgroundColor = ConsoleColor.Green,
                    FocusedForegroundColor = ConsoleColor.DarkGreen,
                    DisabledBackgroundColor = ConsoleColor.Black,
                    DisabledForegroundColor = ConsoleColor.DarkGray,
                    MenuBackgroundColor = ConsoleColor.DarkGray,
                    MenuForegroundColor = ConsoleColor.DarkBlue,
                };
            }
        }
    }
    
}