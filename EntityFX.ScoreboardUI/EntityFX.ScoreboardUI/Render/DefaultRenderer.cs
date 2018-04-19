using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.Menu;
using EntityFX.ScoreboardUI.Elements.Controls.StatusBar;
using EntityFX.ScoreboardUI.Elements.MessageBox;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI.Render
{
    internal class DefaultRenderer : IRenderer
    {
        public IConsoleAdapter ConsoleAdapter { get;  }

        public DefaultRenderer(IConsoleAdapter consoleAdapter, IRenderOptions renderOptions)
        {
            ConsoleAdapter = consoleAdapter;
            RenderOptions = renderOptions;
        }

        public void Initialize()
        {
            //_consoleAdapter.WindowHeight = RenderOptions.WindowHeight;
            //_consoleAdapter.WindowWidth = RenderOptions.WindowWidth;
            ConsoleAdapter.CursorVisible = false;
        }

        public void Render(UiElement uiElement)
        {
            var scoreboard = uiElement as Scoreboard;
            if (scoreboard != null)
            {
                RenderBackgroundBox(scoreboard.Location, scoreboard.Size, scoreboard.BackgroundColor);
                if (scoreboard.IsBorderVisible)
                {
                    RenderBorder(scoreboard, scoreboard.Location, scoreboard.Size, new BorderCharType
                    {
                        TopLeftChar = '╔',
                        BottomLeftChar = '╚',
                        BottomRightChar = '╝',
                        TopRightChar = '╗',
                        HorizontalChar = '═',
                        VerticalChar = '║'
                    });
                }
                RenderTitle(scoreboard);

                var messageBox = scoreboard as MessageBox;

                if (messageBox != null)
                {
                }

                if (scoreboard.Menu != null)
                {
                    RenderMenu(scoreboard);
                }

                if (scoreboard.StatusStrip != null)
                    RenderStatusStrip(scoreboard);

                Render(scoreboard.RootPanel);
            }

            var panel = uiElement as Panel;

            if (panel != null)
            {
                foreach (ControlBase control in panel.Controls)
                {
                    Render(control);
                }
                RenderPanel(panel);
            }

            var controlElement = uiElement as ControlBase;
            if (controlElement != null)
            {
                RenderControl(controlElement);
            }
        }

        private void RenderStatusStrip(Scoreboard scoreboard)
        {
            var stripLocation = new Point
            {
                Left = 1,
                Top = scoreboard.Size.Height - 1
            };
            var statusStripWidth = scoreboard.Size.Width + (scoreboard.IsBorderVisible ? -2 : 0);

            RenderVerticalLine(
                stripLocation,
                statusStripWidth, '▒'
            );

            foreach (var stripItem in scoreboard.StatusStrip.Controls)
            {
                RenderStripItem(stripItem);
            }
        }

        private void RenderMenu(Scoreboard scoreboard)
        {
            var stripLocation = new Point
            {
                Left = 1,
                Top = 1
            };
            var statusStripWidth = scoreboard.Size.Width + (scoreboard.IsBorderVisible ? -2 : 0);

            RenderVerticalLine(
                stripLocation,
                statusStripWidth, '▒'
            );

            foreach (var menuItem in scoreboard.Menu.Controls)
            {
                RenderMenuItem(menuItem);
            }
        }

        private void RenderStripItem(StatusStripItem stripItem)
        {
            RenderControl(stripItem.InternalControl);
        }

        private void RenderMenuItem<TData>(MenuItem<TData> stripItem)
        {
            RenderControl(stripItem.InternalControl);
        }

        public IRenderOptions RenderOptions { get; private set; }

        private void RenderControl(ControlBase control)
        {
            ConsoleColor previousForeground = ConsoleAdapter.ForegroundColor;
            ConsoleColor previousBackground = ConsoleAdapter.BackgroundColor;
            if (control.IsEnabled)
            {
                ConsoleAdapter.BackgroundColor = control.IsFocused ? RenderOptions.ColorScheme.FocusedBackgroundColor : control.BackgroundColor;
                ConsoleAdapter.ForegroundColor = control.IsFocused ? RenderOptions.ColorScheme.FocusedForegroundColor : control.ForegroundColor;
            }
            else
            {
                ConsoleAdapter.BackgroundColor = RenderOptions.ColorScheme.DisabledBackgroundColor;
                ConsoleAdapter.ForegroundColor = RenderOptions.ColorScheme.DisabledForegroundColor;
            }

            var borderable = control as IBorderable;
            var sizeable = control as ISizable;
            if (borderable != null && sizeable != null)
            {
                RenderBorder(borderable, control.Location, sizeable.Size, new BorderCharType
                {
                    TopLeftChar = '┌',
                    BottomLeftChar = '└',
                    BottomRightChar = '┘',
                    TopRightChar = '┐',
                    HorizontalChar = '─',
                    VerticalChar = '│'
                });
            }

            Point loc = control.AbsoluteLocation();
            ConsoleAdapter.MoveCursor(loc.Left, loc.Top);
            var checkbox = control as Checkbox;
            if (checkbox != null)
            {
                RenderCheckBox(checkbox);
            }

            var radioButton = control as RadioButton;
            if (radioButton != null)
            {
                RenderRadioButton(radioButton);
            }

            var label = control as Label;
            if (label != null)
            {
                RenderLabel(label);
            }

            var passwordBox = control as PasswordBox;
            if (passwordBox != null)
            {
                RenderPasswordBox(passwordBox);
            }

            var text = control as TextBox;
            if (text != null && !(control is PasswordBox))
            {
                RenderTextBox(text);
            }

            var button = control as Button;
            if (button != null)
            {
                RenderButton(button);
            }

            var progressBar = control as ProgressBar;
            if (progressBar != null)
            {
                RenderProgressBar(progressBar);
            }

            var image = control as Image;
            if (image != null)
            {
                RenderImage(image);
            }

            var panel = control as Panel;
            if (panel != null)
            {
                RenderPanel(panel);
            }

            var stripItem = control as StatusStripItem;
            if (stripItem != null)
            {
                RenderStripItem(stripItem);
            }

            var menuItem = control as MenuItem<object>;
            if (menuItem != null)
            {
                RenderMenuItem(menuItem);
            }

            var comboBox = control as ComboBox;
            if (comboBox != null)
            {
                RenderComboBox(comboBox);
            }

            var listView = control as ListView;
            if (listView != null)
            {
                RenderListView(listView);
            }

            ConsoleAdapter.ForegroundColor = previousForeground;
            ConsoleAdapter.BackgroundColor = previousBackground;
        }

        private void RenderListView(ListView listView)
        {
            foreach (var listViewItemsControl in listView.ItemsControls)
            {
                RenderListViewItem(listViewItemsControl);
            }
        }

        private void RenderListViewItem(ListViewItem listView)
        {
            foreach (var listViewItemsControl in listView.Controls)
            {
                Render(listViewItemsControl);
            }
        }

        private static IEnumerable<ControlBase> GetOverlappedControls(Point startPoint, Point endPoint)
        {
            return ScoreboardContext.CurrentState.ControlsList.Where(_ =>
            {
                var controlStartPoint = _.AbsoluteLocation();
                var sizableControl = _ as ISizable;
                var linerableControl = _ as ILinearable;
                Point controlEndPoint;
                if (sizableControl != null)
                {
                    controlEndPoint = new Point
                    {
                        Left = controlStartPoint.Left + sizableControl.Size.Width,
                        Top = controlStartPoint.Top + sizableControl.Size.Height
                    };

                }
                else if (linerableControl != null)
                {
                    controlEndPoint = new Point
                    {
                        Left = controlStartPoint.Left + linerableControl.Width,
                        Top = controlStartPoint.Top + 1
                    };
                }
                else
                {
                    controlEndPoint = new Point()
                    {
                        Left = controlStartPoint.Left + _.VisibleText.Length,
                        Top = controlStartPoint.Top + 1
                    };
                }
                return controlEndPoint.Left >= startPoint.Left
                       && controlStartPoint.Left <= endPoint.Left
                       && controlEndPoint.Top >= startPoint.Top
                       && controlEndPoint.Top <= endPoint.Top;
            });
        }

        private void RenderComboBox(ComboBox comboBox)
        {
            ConsoleAdapter.Write(new string(' ', comboBox.Width));
            ConsoleAdapter.MoveCursor(comboBox.Location.Left, comboBox.Location.Top);
            ConsoleAdapter.Write(comboBox.VisibleText);
            ConsoleAdapter.MoveCursor(comboBox.Location.Left + comboBox.Width - 1, comboBox.Location.Top);
            ConsoleColor saveColor = ConsoleAdapter.BackgroundColor;
            ConsoleAdapter.BackgroundColor = comboBox.ExpanderBackground;

            var expanded = comboBox.DroppedDown;
            ConsoleAdapter.Write(expanded ? '▲' : '▼');
            ConsoleAdapter.BackgroundColor = saveColor;
            var initialCurosrTopPosition = comboBox.Location.Top + 1;
            var itemLinesToDraw = comboBox.Items.Count > comboBox.VisibleItemsCount
                                                        ? comboBox.VisibleItemsCount
                                                        : comboBox.Items.Count;

            var expanderLocation = new Point
            {
                Left = comboBox.Location.Left + 1,
                Top = initialCurosrTopPosition
            };

            var expanderSize = new Size {Width = comboBox.Width - 1, Height = itemLinesToDraw};

            if (expanded)
            {
                ConsoleAdapter.MoveCursor(comboBox.Location.Left + comboBox.Width - 1, comboBox.Location.Top);

                RenderBackgroundBox(
                    expanderLocation,
                    expanderSize,
                    comboBox.BackgroundColor
                );
                ConsoleAdapter.ForegroundColor = comboBox.ForegroundColor;
                ConsoleAdapter.BackgroundColor = comboBox.BackgroundColor;

                var startItem = comboBox.SelectedIndex >= comboBox.VisibleItemsCount ? comboBox.SelectedIndex - comboBox.VisibleItemsCount + 1 : 0;
                for (int i = startItem; i < startItem + itemLinesToDraw; i++)
                {
                    ConsoleAdapter.MoveCursor(comboBox.Location.Left + 1, initialCurosrTopPosition);
                    if (comboBox.Items[i] == comboBox.Items[comboBox.SelectedIndex])
                    {
                        ConsoleAdapter.BackgroundColor = RenderOptions.ColorScheme.FocusedBackgroundColor;
                        ConsoleAdapter.ForegroundColor = comboBox.SelectedColor;
                    }
                    ConsoleAdapter.Write(comboBox.Items[i].Text);
                    ConsoleAdapter.BackgroundColor = comboBox.BackgroundColor;
                    ConsoleAdapter.ForegroundColor = comboBox.ForegroundColor;
                    ++initialCurosrTopPosition;
                }

                if (comboBox.Items.Count > comboBox.VisibleItemsCount)
                {
                    var rollerSize = (int)((comboBox.VisibleItemsCount / (float)comboBox.Items.Count) * (comboBox.VisibleItemsCount - 2));
                    var rollDiff = comboBox.Items.Count - comboBox.VisibleItemsCount;
                    var rollerOffset = comboBox.SelectedIndex < comboBox.VisibleItemsCount ? 
                        0 :
                        (int)(((comboBox.SelectedIndex - comboBox.VisibleItemsCount + 1) / (float)rollDiff) * (comboBox.VisibleItemsCount - 2 - rollerSize));
                    RenderVerticalScroll(
                        new Point() { Left = comboBox.Location.Left + comboBox.Width - 1, Top = comboBox.Location.Top + 1 },
                        comboBox.VisibleItemsCount,
                        rollerSize,
                        rollerOffset
                    );
                }
            }
            else
            {

                RenderBackgroundBox(
                    expanderLocation,
                    expanderSize,
                    ScoreboardContext.Navigation.Current.Scoreboard.BackgroundColor
                );
                var overlapped = GetOverlappedControls(expanderLocation, new Point
                {
                    Left = expanderLocation.Left + expanderSize.Width,
                    Top = expanderLocation.Top + expanderSize.Height
                }).Where(_ => _ != comboBox);
                foreach (var overlappedControl in overlapped)
                {
                    overlappedControl.Render();
                }
            }
        }

        private void RenderVerticalScroll(Point point, int height, int rollerSize = 1, int rollerOffset = 0)
        {
            RenderHorizontalLine(new Point() { Left = point.Left, Top = point.Top + 1 }, height - 2, '░');
            ConsoleAdapter.BackgroundColor = ConsoleColor.DarkGray;
            ConsoleAdapter.MoveCursor(point.Left, point.Top);
            ConsoleAdapter.Write('▲');
            ConsoleAdapter.MoveCursor(point.Left, point.Top + height - 1);
            ConsoleAdapter.Write('▼');

            RenderHorizontalLine(new Point() { Left = point.Left, Top = point.Top + 1 + rollerOffset }, rollerSize, '█');
        }

        private void RenderPanel(Panel panel)
        {
            if (panel.DisplayText)
            {
                var loc = panel.AbsoluteLocation();
                ConsoleAdapter.MoveCursor(loc.Left + 1, loc.Top);
                ConsoleAdapter.Write(panel.VisibleText);
            }
        }

        private void RenderImage(Image image)
        {
            Point loc = image.AbsoluteLocation();
            for (int y = 0; y < image.Size.Height; ++y)
            {
                for (int x = 0; x < image.Size.Width; x++)
                {
                    ConsoleAdapter.MoveCursor(x + loc.Left, y + loc.Top);
                    ConsoleAdapter.Write(image.ImageArray[y, x]);
                }
            }
        }

        private void RenderLabel(Label label)
        {
            Point loc = label.AbsoluteLocation();
            ConsoleAdapter.MoveCursor(loc.Left, loc.Top);
            ConsoleAdapter.Write(label.Text);
        }

        private void RenderCheckBox(Checkbox checkbox)
        {
            char marker = checkbox.IsChecked != null ? (bool)checkbox.IsChecked ? '■' : ' ' : '?';
            string label = checkbox.DisplayText ? " " + checkbox.Text : string.Empty;
            ConsoleAdapter.Write(string.Format("[{0}]{1}", marker, label));
        }

        private void RenderTextBox(TextBox textBox)
        {
            ConsoleAdapter.Write(new string(' ', textBox.InputLength));
            ConsoleAdapter.MoveCursor(textBox.Location.Left, textBox.Location.Top);
            ConsoleAdapter.Write(textBox.VisibleText);
        }


        private void RenderPasswordBox(PasswordBox textBox)
        {
            ConsoleAdapter.Write(new string(' ', textBox.InputLength));
            ConsoleAdapter.MoveCursor(textBox.Location.Left, textBox.Location.Top);
            if (textBox.IsFocused && textBox.VisibleText.Length > 0)
            {
                ConsoleAdapter.Write(new String('*', textBox.VisibleText.Length - 1) + textBox.VisibleText[textBox.VisibleText.Length - 1]);
            }
            else
            {
                ConsoleAdapter.Write(new String('*', textBox.VisibleText.Length));
            }
        }

        private void RenderButton(Button button)
        {
            int lineLength = button.Width;

            var str = new string(' ', lineLength);
            Point loc = button.AbsoluteLocation();
            ConsoleAdapter.MoveCursor(loc.Left, loc.Top);
            ConsoleAdapter.Write(str);

            if (button.DisplayText)
            {
                int titleLength = button.VisibleText.Length;
                int left = 0;
                switch (button.TextAligment)
                {
                    case TitleAligment.Left:
                        left = button.Location.Left;
                        ConsoleAdapter.MoveCursor(loc.Left, loc.Top);
                        break;
                    case TitleAligment.Center:
                        left = (button.Width / 2) - (titleLength / 2) + loc.Left;
                        break;
                    case TitleAligment.Right:
                        left = button.Width - titleLength + loc.Left;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("aligment");
                }
                ConsoleAdapter.MoveCursor(left, loc.Top);
                ConsoleAdapter.Write(button.VisibleText);
            }
        }

        private void RenderRadioButton(RadioButton radioButton)
        {
            char marker = radioButton.IsChecked != null ? (bool)radioButton.IsChecked ? '∙' : ' ' : '?';
            string label = radioButton.DisplayText ? " " + radioButton.Text : string.Empty;
            ConsoleAdapter.Write(string.Format("({0}){1}", marker, label));
        }

        private void RenderProgressBar(ProgressBar progressBar)
        {
            var location = progressBar.AbsoluteLocation();
            ConsoleAdapter.MoveCursor(location.Left, location.Top);
            ConsoleAdapter.Write(new string(' ', progressBar.Width));

            int normalMax = progressBar.Maximum - progressBar.Minimum;
            int normalValue = progressBar.Value - progressBar.Minimum;
            var size = (int)((normalValue / (normalMax * 1.0)) * progressBar.Width);

            ConsoleAdapter.MoveCursor(location.Left, location.Top);
            ConsoleColor saveColor = ConsoleAdapter.BackgroundColor;
            ConsoleAdapter.BackgroundColor = progressBar.StripeColor;
            ConsoleAdapter.Write(new string(' ', size));
            ConsoleAdapter.BackgroundColor = saveColor;

            if (progressBar.DisplayText)
            {
                int titleLength = progressBar.VisibleText.Length;
                int left = 0;
                switch (progressBar.TextAligment)
                {
                    case TitleAligment.Left:
                        left = location.Left;
                        ConsoleAdapter.MoveCursor(location.Left, location.Top);
                        break;
                    case TitleAligment.Center:
                        left = (progressBar.Width / 2) - (titleLength / 2) + location.Left;
                        break;
                    case TitleAligment.Right:
                        left = progressBar.Width - titleLength + location.Left;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("aligment");
                }
                ConsoleAdapter.MoveCursor(left, location.Top);
                ConsoleAdapter.Write(progressBar.VisibleText);
            }
        }

        private void RenderBackgroundBox(Point point, Size size, ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackground = ConsoleAdapter.BackgroundColor;
            ConsoleAdapter.BackgroundColor = backgroundColor;
            int lineLength = size.Width;

            var str = new string(' ', lineLength);

            for (int y = point.Top; y < point.Top + size.Height; y++)
            {
                ConsoleAdapter.MoveCursor(point.Left, y);
                ConsoleAdapter.Write(str);
            }
            ConsoleAdapter.MoveCursor(point.Left, point.Top);
            ConsoleAdapter.BackgroundColor = previousBackground;
        }

        private void RenderBorder(IBorderable control, Point location, Size size, BorderCharType borderCharType)
        {
            ConsoleColor previousBackground = ConsoleAdapter.BackgroundColor;
            ConsoleAdapter.BackgroundColor = control.BorderBackgroundColor;

            ConsoleColor previousForeground = ConsoleAdapter.ForegroundColor;
            ConsoleAdapter.ForegroundColor = control.BorderForegroundColor;
            RenderVerticalLine(
                new Point
            {
                Left = location.Left + 1,
                Top = location.Top
            },
                size.Width - 2, borderCharType.HorizontalChar);
            RenderVerticalLine(
                new Point
            {
                Left = location.Left + 1,
                Top = location.Top + size.Height - 1
            },
                size.Width - 2, borderCharType.HorizontalChar);
            RenderHorizontalLine(
                new Point
            {
                Left = location.Left,
                Top = location.Top + 1
            },
                size.Height - 2, borderCharType.VerticalChar);
            RenderHorizontalLine(
                new Point
            {
                Left = location.Left + size.Width - 1,
                Top = location.Top + 1
            },
                size.Height - 2, borderCharType.VerticalChar);

            ConsoleAdapter.MoveCursor(location.Left + size.Width - 1, location.Top + size.Height - 1);
            if (!(RenderOptions.WindowHeight == location.Left + size.Width && RenderOptions.WindowHeight == location.Top + size.Height))
            {
                ConsoleAdapter.Write(borderCharType.BottomRightChar);
            }
            else
            {
                ConsoleAdapter.MoveCursor(location.Left, location.Top);
                ConsoleAdapter.Write(borderCharType.BottomRightChar);
                ConsoleAdapter.MoveArea(location.Left, location.Top, 1, 1, location.Left + size.Width - 1, location.Top + size.Height - 1);
            }

            ConsoleAdapter.MoveCursor(location.Left, location.Top);
            ConsoleAdapter.Write(borderCharType.TopLeftChar);

            ConsoleAdapter.MoveCursor(location.Left, location.Top + size.Height - 1);
            ConsoleAdapter.Write(borderCharType.BottomLeftChar);

            ConsoleAdapter.MoveCursor(location.Left + size.Width - 1, location.Top);
            ConsoleAdapter.Write(borderCharType.TopRightChar);
            ConsoleAdapter.BackgroundColor = previousBackground;
            ConsoleAdapter.ForegroundColor = previousForeground;
        }

        private void RenderVerticalLine(Point location, int width, char character = '-')
        {
            var str = new string(character, width);
            ConsoleAdapter.MoveCursor(location.Left, location.Top);
            ConsoleAdapter.Write(str);
        }

        private void RenderHorizontalLine(Point location, int height, char character = '-')
        {
            for (int i = 0; i < height; i++)
            {
                ConsoleAdapter.MoveCursor(location.Left, location.Top + i);
                ConsoleAdapter.Write(character);
            }
        }

        private void RenderTitle(Scoreboard scoreboard)
        {
            ConsoleColor previousForeground = ConsoleAdapter.ForegroundColor;
            ConsoleColor previousBackground = ConsoleAdapter.BackgroundColor;
            ConsoleAdapter.BackgroundColor = scoreboard.BackgroundColor;
            ConsoleAdapter.ForegroundColor = scoreboard.ForegroundColor;

            int titleLength = scoreboard.Title.Length;
            int left = 0;
            switch (scoreboard.TitleAligment)
            {
                case TitleAligment.Left:
                    left = scoreboard.Location.Left;
                    ConsoleAdapter.MoveCursor(scoreboard.Location.Left, scoreboard.Location.Top);
                    break;
                case TitleAligment.Center:
                    left = (scoreboard.Size.Width / 2) - (titleLength / 2) + scoreboard.Location.Left;
                    break;
                case TitleAligment.Right:
                    left = scoreboard.Size.Width - titleLength + scoreboard.Location.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("aligment");
            }
            ConsoleAdapter.MoveCursor(left, scoreboard.Location.Top);
            ConsoleAdapter.Write(scoreboard.Title);
            ConsoleAdapter.ForegroundColor = previousForeground;
            ConsoleAdapter.BackgroundColor = previousBackground;
        }
    }
}