using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.Menu;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI.Elements.MessageBox
{
    public class MessageBox : Scoreboard
    {
        private readonly MessageBoxButtonsEnum _buttonsType;
        private readonly MessageBoxButtonsDirectionEnum _direction;
        private readonly IEnumerable<SubmenuContext<object>> _buttonsList;
        private readonly string _message;

        private readonly MessageBoxTypeEnum _messageBoxType;
        private Label _labelMessage;

        private Image _messageBoxImage;

        private Action<MessageBoxResultEnum, object> _result;

        public MessageBox(string message, Action<MessageBoxResultEnum, object> result, string title, MessageBoxTypeEnum type,
            MessageBoxButtonsEnum buttons,
            MessageBoxButtonsDirectionEnum direction = MessageBoxButtonsDirectionEnum.Horizontal)
            : base(new Panel())
        {
            _message = message;
            _buttonsType = buttons;
            _direction = direction;
            _messageBoxType = type;
            _result = result;
            Title = title;
        }

        public MessageBox(string message, Action<MessageBoxResultEnum, object> result, string title, MessageBoxTypeEnum type,
            MessageBoxButtonsDirectionEnum direction = MessageBoxButtonsDirectionEnum.Horizontal,
            IEnumerable<SubmenuContext<object>> buttonsList = null)
            : base(new Panel())
        {
            _message = message;
            _direction = direction;
            _buttonsList = buttonsList;
            _messageBoxType = type;
            _result = result;
            Title = title;
        }

        public static void Show(string message, Action<MessageBoxResultEnum, object> resultAction, string title)
        {
            Show(message, resultAction, title, MessageBoxTypeEnum.Info, MessageBoxButtonsEnum.Ok);
        }

        public static void Show(string message, Action<MessageBoxResultEnum, object> resultAction, string title,
            MessageBoxTypeEnum type)
        {
            Show(message, resultAction, title, type, MessageBoxButtonsEnum.Ok);
        }

        public static void Show(string message,
    IEnumerable<SubmenuContext<object>> buttonsList, Action<MessageBoxResultEnum, object> resultAction = null,
    string title = "Message", MessageBoxTypeEnum type = MessageBoxTypeEnum.Info,
    MessageBoxButtonsDirectionEnum direction = MessageBoxButtonsDirectionEnum.Horizontal)
        {
            int width = 0;
            ScoreboardContext.CurrentState.IsNavigating = true;
            MessageBox mb;
            int minWidth = 16;
            var largestTextLength = buttonsList.Select(i => i.Text.Length).Max();
            width = largestTextLength > minWidth ? largestTextLength + 6 : minWidth;
            mb = new MessageBox(message, resultAction, title, type, direction, buttonsList)
            {
                BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MessageBoxBackgroundColor,
                ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MessageBoxForegroundColor,
                BorderForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BorderColor,
                Size = new Size
                {
                    Height = direction == MessageBoxButtonsDirectionEnum.Horizontal ? 10 : buttonsList.Count() * 3 + 3,
                    Width = width
                },
                Location = new Point
                {
                    Left = ScoreboardContext.Navigation.Current.Scoreboard.Size.Width / 2 - width / 2,
                    Top = ScoreboardContext.Navigation.Current.Scoreboard.Size.Height / 2 - 4
                }
            };
            ScoreboardContext.Navigation.Navigate(mb);
            ScoreboardContext.RenderEngine.ConsoleAdapter.Beep();
        }

        public static void Show(string message, Action<MessageBoxResultEnum, object> resultAction = null,
            string title = "Message", MessageBoxTypeEnum type = MessageBoxTypeEnum.Info,
            MessageBoxButtonsEnum buttons = MessageBoxButtonsEnum.Ok,
            MessageBoxButtonsDirectionEnum direction = MessageBoxButtonsDirectionEnum.Horizontal)
        {

            int width = 0;
            ScoreboardContext.CurrentState.IsNavigating = true;
            MessageBox mb;
            int minWidth = ScoreboardContext.Navigation.Current.Scoreboard.Size.Width / 2;
            width = message.Length > minWidth ? message.Length + 2 : minWidth;
            mb = new MessageBox(message, resultAction, title, type, buttons, direction)
            {
                BackgroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MessageBoxBackgroundColor,
                ForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.MessageBoxForegroundColor,
                BorderForegroundColor = ScoreboardContext.RenderEngine.RenderOptions.ColorScheme.BorderColor,
                Size = new Size
                {
                    Height = direction == MessageBoxButtonsDirectionEnum.Horizontal ? 10 : 20,
                    Width = width
                },
                Location = new Point
                {
                    Left = ScoreboardContext.Navigation.Current.Scoreboard.Size.Width / 2 - width / 2,
                    Top = ScoreboardContext.Navigation.Current.Scoreboard.Size.Height / 2 - 4
                }
            };
            ScoreboardContext.Navigation.Navigate(mb);
            ScoreboardContext.RenderEngine.ConsoleAdapter.Beep();

        }

        public static void Alert(string message, Action<MessageBoxResultEnum> resultAction, string title)
        {
            Show(message);
        }

        public static string Prompt(string message, Action<MessageBoxResultEnum> resultAction, string title)
        {
            return "Ok";
        }

        public static void Confirm(string message, Action<MessageBoxResultEnum, object> resultAction, string title)
        {
            Show(message, resultAction, string.Empty, MessageBoxTypeEnum.Info, MessageBoxButtonsEnum.YesNo);
        }

        public override void Initialize()
        {
            base.Initialize();
            _labelMessage = new Label
            {
                Text = _message,
                Location = new Point
                {
                    Top = 2,
                    Left = Size.Width / 2 - _message.Length / 2
                },
                BackgroundColor = BackgroundColor,
                ForegroundColor = ConsoleColor.White
            };
            RootPanel.AddChild(_labelMessage);
            InitializeButtons();
            InitializeImage();
        }

        protected override void OnEscapePressed()
        {
            base.OnEscapePressed();
            PerfomMessageBoxResultAction(MessageBoxResultEnum.None, null);
        }

        private void InitializeCustomButtons()
        {
            int buttonWidth = 10;
            int buttonTopStep = 2;
            int buttonStartTopPointer = 0;

            var largestTextLength = _buttonsList.Select(i => i.Text.Length).Max();
            buttonWidth = largestTextLength > 10 ? (largestTextLength + 2) : buttonWidth;

            foreach (var context in _buttonsList)
            {
                var button = new Button
                {
                    Text = context.Text,
                    Location = new Point
                    {
                        Left = this.Size.Width / 2 - (buttonWidth) / 2,
                        Top = buttonStartTopPointer + 4
                    },
                    Width = buttonWidth,
                    Tag = context.Data
                };
                Debug.WriteLine(this.Size.Width);
                Debug.WriteLine(largestTextLength);
                RootPanel.AddChild(button);
                button.Pressed += button_Pressed;
                buttonStartTopPointer += buttonTopStep;
            }
            return;
        }

        private void InitializeStandardButtons()
        {
            int buttonWidth = 10;
            int buttonTop = Size.Height - 2;
            var resultButtonsDictionary = new Dictionary<MessageBoxResultEnum, string>
            {
                {MessageBoxResultEnum.Ok, "Ok"},
                {MessageBoxResultEnum.Cancel, "Cancel"},
                {MessageBoxResultEnum.Yes, "Yes"},
                {MessageBoxResultEnum.No, "No"},
            };

            var buttonTypeTextDisctionary = new Dictionary
                <MessageBoxButtonsEnum, List<Tuple<MessageBoxResultEnum, string>>>
            {
                {
                    MessageBoxButtonsEnum.Ok,
                    new List<Tuple<MessageBoxResultEnum, string>>
                    {
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.Ok,
                            resultButtonsDictionary[MessageBoxResultEnum.Ok])
                    }
                },
                {
                    MessageBoxButtonsEnum.OkCancel,
                    new List<Tuple<MessageBoxResultEnum, string>>
                    {
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.Ok,
                            resultButtonsDictionary[MessageBoxResultEnum.Ok]),
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.Cancel,
                            resultButtonsDictionary[MessageBoxResultEnum.Cancel]),
                    }
                },
                {
                    MessageBoxButtonsEnum.YesNo,
                    new List<Tuple<MessageBoxResultEnum, string>>
                    {
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.Yes,
                            resultButtonsDictionary[MessageBoxResultEnum.Yes]),
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.No,
                            resultButtonsDictionary[MessageBoxResultEnum.No]),
                    }
                },
                {
                    MessageBoxButtonsEnum.YesNoCancel,
                    new List<Tuple<MessageBoxResultEnum, string>>
                    {
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.Yes,
                            resultButtonsDictionary[MessageBoxResultEnum.Yes]),
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.No,
                            resultButtonsDictionary[MessageBoxResultEnum.No]),
                        new Tuple<MessageBoxResultEnum, string>(MessageBoxResultEnum.Cancel,
                            resultButtonsDictionary[MessageBoxResultEnum.Cancel]),
                    }
                }
            };

            int buttonsCount = buttonTypeTextDisctionary[_buttonsType].Count;
            int buttonAreaWidth = Size.Width / buttonsCount;
            int buttonLeftStep = (buttonAreaWidth - buttonWidth) / 2;
            int buttonStartLeftPointer = 0;

            foreach (var text in buttonTypeTextDisctionary[_buttonsType])
            {
                var button = new Button
                {
                    Text = text.Item2,
                    Location = new Point
                    {
                        Left = buttonStartLeftPointer + buttonLeftStep,
                        Top = buttonTop
                    },
                    Tag = text.Item1
                };
                RootPanel.AddChild(button);
                button.Pressed += button_Pressed;
                buttonStartLeftPointer += buttonAreaWidth;
            }
        }

        private void InitializeButtons()
        {

            if (_buttonsList != null)
            {
                InitializeCustomButtons();
            }
            else
            {
                InitializeStandardButtons();
            }
        }

        private void button_Pressed(object sender, EventArgs e)
        {
            GoBack();
            var senderButton = (Button)sender;
            var enumResult = senderButton.Tag is MessageBoxResultEnum @enum ? @enum : MessageBoxResultEnum.None;
            PerfomMessageBoxResultAction(enumResult, senderButton.Tag);
        }

        private void PerfomMessageBoxResultAction(MessageBoxResultEnum messageBoxResult, object data)
        {
            if (_result != null)
            {
                _result(messageBoxResult, data);
                _result = null;
            }
        }

        private void InitializeImage()
        {
            char[,] imageArray;
            switch (_messageBoxType)
            {
                case MessageBoxTypeEnum.Error:
                    imageArray = new[,]
                    {
                        {' ', '*', ' '},
                        {'*', ' ', '*'},
                        {' ', ' ', '*'},
                        {' ', '*', ' '},
                        {' ', ' ', ' '},
                        {' ', '*', ' '}
                    };
                    break;
                case MessageBoxTypeEnum.Warning:
                    imageArray = new[,]
                    {
                        {' ', '*', ' '},
                        {' ', '*', ' '},
                        {' ', '*', ' '},
                        {' ', '*', ' '},
                        {' ', ' ', ' '},
                        {' ', '*', ' '}
                    };
                    break;
                case MessageBoxTypeEnum.Info:
                    imageArray = new[,]
                    {
                        {' ', '*', ' '},
                        {' ', ' ', ' '},
                        {' ', '*', ' '},
                        {' ', '*', ' '},
                        {' ', '*', ' '},
                        {'*', '*', '*'}
                    };
                    break;
                case MessageBoxTypeEnum.Question:
                    imageArray = new[,]
                    {
                        {' ', '*', ' '},
                        {'*', ' ', '*'},
                        {' ', ' ', '*'},
                        {' ', '*', ' '},
                        {' ', ' ', ' '},
                        {' ', '*', ' '}
                    };
                    break;
                default:
                    imageArray = new char[0, 0];
                    break;
            }
            _messageBoxImage = new Image(imageArray)
            {
                Location = new Point
                {
                    Left = 1,
                    Top = 1
                },
                BackgroundColor = BackgroundColor
            };
            RootPanel.AddChild(_messageBoxImage);
        }
    }
}