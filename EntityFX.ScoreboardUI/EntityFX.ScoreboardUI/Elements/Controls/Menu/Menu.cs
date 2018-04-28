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
            if (childControl.ItemLocation == ItemLocationEnum.Left)
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

            if (childControl is MenuItemButton<object> button)
            {
                button.KeyPress += Button_KeyPress;
            }
        }

        public virtual void Clear()
        {
            if (controls != null && controls.Any())
            {
                var menuControls = controls.OfType<MenuItemButton<object>>().ToArray();
                foreach (var item in menuControls)
                {
                    item.KeyPress -= Button_KeyPress;
                    ClearInternal(item);
                    RemoveChild(item);
                }
            }
        }

        protected virtual void ClearInternal(MenuItemButton<object> menuItem)
        {
            if (menuItem.SubMenuItems != null && menuItem.SubMenuItems.Any())
            {
                foreach (var subMenu in menuItem.SubMenuItems)
                {
                    subMenu.KeyPress -= Button_KeyPress;
                    ClearInternal(subMenu);
                    RemoveChild(subMenu);
                }
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
                MessageBox.MessageBox.Show("Select option",
                    sender.SubMenuItems.Select(i => new SubmenuContext<object>()
                    {
                        Data = i.Data,
                        Text = i.Text,
                        IsEnabled = i.IsEnabled
                    }), (e1, data) =>
{
    Pressed?.Invoke(sender, data);
}, sender.Text, MessageBoxTypeEnum.None,
                    MessageBoxButtonsDirectionEnum.Vertical);
            }
            else
            {
                Pressed?.Invoke(sender, sender.Data);
            }
        }

        public override void ClearEvents()
        {
            base.ClearEvents();
            Pressed = null;
        }
    }
}