using System;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public class StatusStrip : Control<StatusStripItem>
    {
        private int _leftOffset;

        private int _rightOffset = -1;

        public StatusStrip()
        {
            BackgroundColor = ConsoleColor.DarkGray;
            ForegroundColor = ConsoleColor.Black;
        }

        public override void AddChild(StatusStripItem childControl)
        {
            childControl.InternalControl.BackgroundColor = childControl.BackgroundColor;
            childControl.InternalControl.ForegroundColor = childControl.ForegroundColor;
            base.AddChild(childControl);
            var itemWidth = GetItemWidth(childControl);
            if (childControl.ItemLocation == StatusStripItemLocationEnum.Left)
            {
                childControl.Location = new Point
                {
                    Left = _leftOffset,
                    Top = 0
                };
                _leftOffset += itemWidth + 1;
            }
            else
            {
                if (_rightOffset == -1)
                {
                    _rightOffset = ParentScoreboard.Size.Width - 3;
                }
                _rightOffset -= (itemWidth + 1);

                childControl.Location = new Point
                {
                    Left = _rightOffset,
                    Top = 0
                };
            }
        }

        private int GetItemWidth(StatusStripItem childControl)
        {
            var linerableControl = childControl.InternalControl as ILinearable;
            return linerableControl != null ? linerableControl.Width : childControl.Text.Length;
        }
    }
}