using System;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Render
{
    public interface IConsoleAdapter
    {
        Size Size { get; set; }

        void Write(string value);
        void Write(char value);

        void MoveCursor(int left, int top);

        ConsoleColor ForegroundColor { get; set; }

        ConsoleColor BackgroundColor { get; set; }
        bool CursorVisible { get; set; }

        void MoveArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop);

        ConsoleKeyInfo ReadKey(bool intercept);
        void Beep();
    }
}