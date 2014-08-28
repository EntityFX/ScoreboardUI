using System;
using System.Collections.Generic;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI.Elements.MessageBox
{
    public class MessageBox : Scoreboard
    {
        private readonly MessageBoxButtonsEnum _buttonsType;
        private readonly string _message;

        private readonly MessageBoxTypeEnum _messageBoxType;
        private Label _labelMessage;

        private Image _messageBoxImage;

        private Action<MessageBoxResultEnum> _result;

        public MessageBox(string message, Action<MessageBoxResultEnum> result, string title, MessageBoxTypeEnum type,
            MessageBoxButtonsEnum buttons)
            : base(new Panel())
        {
            _message = message;
            _buttonsType = buttons;
            _messageBoxType = type;
            _result = result;
            Title = title;
        }

        public static void Show(string message, Action<MessageBoxResultEnum> resultAction, string title)
        {
            Show(message, resultAction, title, MessageBoxTypeEnum.Info, MessageBoxButtonsEnum.Ok);
        }

        public static void Show(string message, Action<MessageBoxResultEnum> resultAction, string title,
            MessageBoxTypeEnum type)
        {
            Show(message, resultAction, title, type, MessageBoxButtonsEnum.Ok);
        }

        public static void Show(string message, Action<MessageBoxResultEnum> resultAction = null,
            string title = "Message", MessageBoxTypeEnum type = MessageBoxTypeEnum.Info,
            MessageBoxButtonsEnum buttons = MessageBoxButtonsEnum.Ok)
        {
            int minWidth = ScoreboardContext.Navigation.Current.Scoreboard.Size.Width/2;
            int width = message.Length > minWidth ? message.Length + 2 : minWidth;
            ScoreboardContext.CurrentState.IsNavigating = true;
            var mb = new MessageBox(message, resultAction, title, type, buttons)
            {
                BackgroundColor = ConsoleColor.DarkRed,
                Size = new Size
                {
                    Height = 10,
                    Width = width
                },
                Location = new Point
                {
                    Left = ScoreboardContext.Navigation.Current.Scoreboard.Size.Width/2 - width/2,
                    Top = ScoreboardContext.Navigation.Current.Scoreboard.Size.Height/2 - 4
                }
            };
            ScoreboardContext.Navigation.Navigate(mb);
            Console.Beep();
        }

        public static void Alert(string message, Action<MessageBoxResultEnum> resultAction, string title)
        {
            Show(message);
        }

        public static string Prompt(string message, Action<MessageBoxResultEnum> resultAction, string title)
        {
            return "Ok";
        }

        public static void Confirm(string message, Action<MessageBoxResultEnum> resultAction, string title)
        {
            Show(message, resultAction, string.Empty, MessageBoxTypeEnum.Info, MessageBoxButtonsEnum.YesNo);
        }

        protected override void Initialize()
        {
            base.Initialize();
            BorderBackgroundColor = ConsoleColor.DarkRed;
            BorderForegroundColor = ConsoleColor.Yellow;
            _labelMessage = new Label
            {
                Text = _message,
                Location = new Point
                {
                    Top = 2,
                    Left = 5
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
            PerfomMessageBoxResultAction(MessageBoxResultEnum.None);
        }

        private void InitializeButtons()
        {
            int buttonTop = Size.Height - 2;
            int buttonWidth = 10;

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
            int buttonAreaWidth = Size.Width/buttonsCount;
            int buttonLeftStep = (buttonAreaWidth - buttonWidth)/2;
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

        private void button_Pressed(object sender, EventArgs e)
        {
            GoBack();
            var senderButton = (Button) sender;
            PerfomMessageBoxResultAction((MessageBoxResultEnum) senderButton.Tag);
        }

        private void PerfomMessageBoxResultAction(MessageBoxResultEnum messageBoxResult)
        {
            if (_result != null)
            {
                _result(messageBoxResult);
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