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
        public DefaultRenderer()
        {
            RenderOptions = new DefaultRenderOptions();
        }

        public void Initialize()
        {
            Console.WindowHeight = RenderOptions.WindowHeight;
            Console.WindowWidth = RenderOptions.WindowWidth;
            Console.CursorVisible = false;
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
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;
            if (control.IsEnabled)
            {
                Console.BackgroundColor = control.IsFocused ? ConsoleColor.DarkYellow : control.BackgroundColor;
                Console.ForegroundColor = control.ForegroundColor;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkGray;
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
            Console.SetCursorPosition(loc.Left, loc.Top);
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

            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
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
            Console.Write(new string(' ', comboBox.Width));
            Console.SetCursorPosition(comboBox.Location.Left, comboBox.Location.Top);
            Console.Write(comboBox.VisibleText);
            Console.SetCursorPosition(comboBox.Location.Left + comboBox.Width - 1, comboBox.Location.Top);
            ConsoleColor saveColor = Console.BackgroundColor;
            Console.BackgroundColor = comboBox.ExpanderBackground;

            var expanded = comboBox.DroppedDown;
            Console.Write(expanded ? '▲' : '▼');
            Console.BackgroundColor = saveColor;
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
                Console.SetCursorPosition(comboBox.Location.Left + comboBox.Width - 1, comboBox.Location.Top);

                RenderBackgroundBox(
                    expanderLocation,
                    expanderSize,
                    comboBox.BackgroundColor
                );
                Console.ForegroundColor = comboBox.ForegroundColor;
                Console.BackgroundColor = comboBox.BackgroundColor;

                var startItem = comboBox.SelectedIndex >= comboBox.VisibleItemsCount ? comboBox.SelectedIndex - comboBox.VisibleItemsCount + 1 : 0;
                for (int i = startItem; i < startItem + itemLinesToDraw; i++)
                {
                    Console.SetCursorPosition(comboBox.Location.Left + 1, initialCurosrTopPosition);
                    if (comboBox.Items[i] == comboBox.Items[comboBox.SelectedIndex])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = comboBox.SelectedColor;
                    }
                    Console.Write(comboBox.Items[i].Text);
                    Console.BackgroundColor = comboBox.BackgroundColor;
                    Console.ForegroundColor = comboBox.ForegroundColor;
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
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(point.Left, point.Top);
            Console.Write('▲');
            Console.SetCursorPosition(point.Left, point.Top + height - 1);
            Console.Write('▼');

            RenderHorizontalLine(new Point() { Left = point.Left, Top = point.Top + 1 + rollerOffset }, rollerSize, '█');
        }

        private void RenderPanel(Panel panel)
        {
            if (panel.DisplayText)
            {
                var loc = panel.AbsoluteLocation();
                Console.SetCursorPosition(loc.Left + 1, loc.Top);
                Console.Write(panel.VisibleText);
            }
        }

        private void RenderImage(Image image)
        {
            Point loc = image.AbsoluteLocation();
            for (int y = 0; y < image.Size.Height; ++y)
            {
                for (int x = 0; x < image.Size.Width; x++)
                {
                    Console.SetCursorPosition(x + loc.Left, y + loc.Top);
                    Console.Write(image.ImageArray[y, x]);
                }
            }
        }

        private void RenderLabel(Label label)
        {
            Point loc = label.AbsoluteLocation();
            Console.SetCursorPosition(loc.Left, loc.Top);
            Console.Write(label.Text);
        }

        private void RenderCheckBox(Checkbox checkbox)
        {
            char marker = checkbox.IsChecked != null ? (bool)checkbox.IsChecked ? '■' : ' ' : '?';
            string label = checkbox.DisplayText ? " " + checkbox.Text : string.Empty;
            Console.Write("[{0}]{1}", marker, label);
        }

        private void RenderTextBox(TextBox textBox)
        {
            Console.Write(new string(' ', textBox.InputLength));
            Console.SetCursorPosition(textBox.Location.Left, textBox.Location.Top);
            Console.Write(textBox.VisibleText);
        }


        private void RenderPasswordBox(PasswordBox textBox)
        {
            Console.Write(new string(' ', textBox.InputLength));
            Console.SetCursorPosition(textBox.Location.Left, textBox.Location.Top);
            if (textBox.IsFocused && textBox.VisibleText.Length > 0)
            {
                Console.Write(new String('*', textBox.VisibleText.Length - 1) + textBox.VisibleText[textBox.VisibleText.Length - 1]);
            }
            else
            {
                Console.Write(new String('*', textBox.VisibleText.Length));
            }
        }

        private void RenderButton(Button button)
        {
            int lineLength = button.Width;

            var str = new string(' ', lineLength);
            Point loc = button.AbsoluteLocation();
            Console.SetCursorPosition(loc.Left, loc.Top);
            Console.Write(str);

            if (button.DisplayText)
            {
                int titleLength = button.VisibleText.Length;
                int left = 0;
                switch (button.TextAligment)
                {
                    case TitleAligment.Left:
                        left = button.Location.Left;
                        Console.SetCursorPosition(loc.Left, loc.Top);
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
                Console.SetCursorPosition(left, loc.Top);
                Console.Write(button.VisibleText);
            }
        }

        private void RenderRadioButton(RadioButton radioButton)
        {
            char marker = radioButton.IsChecked != null ? (bool)radioButton.IsChecked ? '∙' : ' ' : '?';
            string label = radioButton.DisplayText ? " " + radioButton.Text : string.Empty;
            Console.Write("({0}){1}", marker, label);
        }

        private void RenderProgressBar(ProgressBar progressBar)
        {
            var location = progressBar.AbsoluteLocation();
            Console.SetCursorPosition(location.Left, location.Top);
            Console.Write(new string(' ', progressBar.Width));

            int normalMax = progressBar.Maximum - progressBar.Minimum;
            int normalValue = progressBar.Value - progressBar.Minimum;
            var size = (int)((normalValue / (normalMax * 1.0)) * progressBar.Width);

            Console.SetCursorPosition(location.Left, location.Top);
            ConsoleColor saveColor = Console.BackgroundColor;
            Console.BackgroundColor = progressBar.StripeColor;
            Console.Write(new string(' ', size));
            Console.BackgroundColor = saveColor;

            if (progressBar.DisplayText)
            {
                int titleLength = progressBar.VisibleText.Length;
                int left = 0;
                switch (progressBar.TextAligment)
                {
                    case TitleAligment.Left:
                        left = location.Left;
                        Console.SetCursorPosition(location.Left, location.Top);
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
                Console.SetCursorPosition(left, location.Top);
                Console.Write(progressBar.VisibleText);
            }
        }

        private void RenderBackgroundBox(Point point, Size size, ConsoleColor backgroundColor)
        {
            ConsoleColor previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            int lineLength = size.Width;

            var str = new string(' ', lineLength);

            for (int y = point.Top; y < point.Top + size.Height; y++)
            {
                Console.SetCursorPosition(point.Left, y);
                Console.Write(str);
            }
            Console.SetCursorPosition(point.Left, point.Top);
            Console.BackgroundColor = previousBackground;
        }

        private void RenderBorder(IBorderable control, Point location, Size size, BorderCharType borderCharType)
        {
            ConsoleColor previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = control.BorderBackgroundColor;

            ConsoleColor previousForeground = Console.ForegroundColor;
            Console.ForegroundColor = control.BorderForegroundColor;
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

            Console.SetCursorPosition(location.Left + size.Width - 1, location.Top + size.Height - 1);
            if (!(Console.BufferWidth == location.Left + size.Width && Console.BufferHeight == location.Top + size.Height))
            {
                Console.Write(borderCharType.BottomRightChar);
            }
            else
            {
                Console.SetCursorPosition(location.Left, location.Top);
                Console.Write(borderCharType.BottomRightChar);
                Console.MoveBufferArea(location.Left, location.Top, 1, 1, location.Left + size.Width - 1, location.Top + size.Height - 1);
            }

            Console.SetCursorPosition(location.Left, location.Top);
            Console.Write(borderCharType.TopLeftChar);

            Console.SetCursorPosition(location.Left, location.Top + size.Height - 1);
            Console.Write(borderCharType.BottomLeftChar);

            Console.SetCursorPosition(location.Left + size.Width - 1, location.Top);
            Console.Write(borderCharType.TopRightChar);
            Console.BackgroundColor = previousBackground;
            Console.ForegroundColor = previousForeground;
        }

        private void RenderVerticalLine(Point location, int width, char character = '-')
        {
            var str = new string(character, width);
            Console.SetCursorPosition(location.Left, location.Top);
            Console.Write(str);
        }

        private void RenderHorizontalLine(Point location, int height, char character = '-')
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(location.Left, location.Top + i);
                Console.Write(character);
            }
        }

        private void RenderTitle(Scoreboard scoreboard)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;
            Console.BackgroundColor = scoreboard.BackgroundColor;
            Console.ForegroundColor = scoreboard.ForegroundColor;

            int titleLength = scoreboard.Title.Length;
            int left = 0;
            switch (scoreboard.TitleAligment)
            {
                case TitleAligment.Left:
                    left = scoreboard.Location.Left;
                    Console.SetCursorPosition(scoreboard.Location.Left, scoreboard.Location.Top);
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
            Console.SetCursorPosition(left, scoreboard.Location.Top);
            Console.Write(scoreboard.Title);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
    }
}