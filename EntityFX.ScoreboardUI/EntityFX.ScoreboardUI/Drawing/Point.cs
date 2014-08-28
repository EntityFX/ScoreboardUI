namespace EntityFX.ScoreboardUI.Drawing
{
    public struct Point
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public static Point operator +(Point left, Point right)
        {
            return new Point
            {
                Left = left.Left + right.Left,
                Top = left.Top + right.Top
            };
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point
            {
                Left = left.Left - right.Left,
                Top = left.Top - right.Top
            };
        }
    }
}