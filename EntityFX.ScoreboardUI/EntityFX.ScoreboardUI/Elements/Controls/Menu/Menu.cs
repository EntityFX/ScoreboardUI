using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.MessageBox;

namespace EntityFX.ScoreboardUI.Elements.Controls.Menu
{
    public class Menu : Control<MenuItem<object>>
    {
        private int _leftOffset;

        private int _rightOffset = -1;

        public Menu()
        {
            BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MenuBackgroundColor;
            ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MenuForegroundColor;
        }

        public override void AddChild(MenuItem<object> childControl)
        {
            childControl.InternalControl.BackgroundColor = childControl.BackgroundColor;
            childControl.InternalControl.ForegroundColor = childControl.ForegroundColor;
            base.AddChild(childControl);
            var itemWidth = GetItemWidth(childControl);

            childControl.Location = new Point
            {
                Left = _leftOffset,
                Top = 0
            };
            _leftOffset += itemWidth + 1;

            if (childControl is MenuItemButton<object> button)
            {
                button.KeyPress += Button_KeyPress;
            }
        }

        private void Button_KeyPress(UiElement sender, KeyPressEventArgs e)
        {
            if (e.KeyInfo.Key == ConsoleKey.Spacebar || e.KeyInfo.Key == ConsoleKey.Enter)
            {
                Press(sender as MenuItemButton<object>, e);
            }
        }

        private int GetItemWidth(MenuItem<object> childControl)
        {
            var linerableControl = childControl.InternalControl as ILinearable;
            return linerableControl != null ? linerableControl.Width : childControl.Text.Length;
        }

        public event EventHandler<object> Pressed;

        public virtual void Press(MenuItemButton<object> sender, KeyPressEventArgs e)
        {
            OnPressed(sender, e);
        }

        protected virtual void OnPressed(MenuItemButton<object> sender, KeyPressEventArgs e)
        {
            if (sender.SubMenuItems != null && sender.SubMenuItems.Any())
            {
                MessageBox.MessageBox.Show("Select option", (e1, data) =>
                    {
                        Pressed?.Invoke(sender, data);
                    }, sender.Text, MessageBoxTypeEnum.None,
                    MessageBoxButtonsEnum.Ok,
                    MessageBoxButtonsDirectionEnum.Vertical,
                    sender.SubMenuItems.Select(i => new SubmenuContext<object>(){ Data = i.Data, Text = i.Text}));
            }
            else
            {
                Pressed?.Invoke(sender, sender.Data);
            }
        }
    }
}