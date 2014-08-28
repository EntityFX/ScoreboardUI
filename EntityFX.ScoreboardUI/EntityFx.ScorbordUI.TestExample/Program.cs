using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.StatusBar;
using EntityFX.ScoreboardUI.Elements.Controls.Table;
using EntityFX.ScoreboardUI.Elements.MessageBox;
using EntityFX.ScoreboardUI.Elements.Scoreboards;
using System.Runtime.InteropServices;

namespace EntityFx.ScorbordUI.TestExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            IUiApplication application = new UiApplication();
            Scoreboard initialScoreboard = new MainScoreboard();
            application.Run(initialScoreboard);
        }
    }

    class MainScoreboard : Scoreboard, IDisposable
    {
        private Checkbox _checkbox1;

        private Checkbox _checkbox2;

        private Checkbox _checkbox3;

        private Checkbox _checkbox4;

        private Label _label1;

        private RadioButton _radioButton1;

        private RadioButton _radioButton2;

        private RadioButton _radioButton3;

        private TextBox _textBox1;

        private Button _button1;

        private Button _button2;

        private Button _button3;

        private ProgressBar _progressBar1;

        private Timer _timer = new Timer(500);

        private Timer _timer2 = new Timer(200);

        private Button _bnext1;

        private Button _bnext2;

        private Button _bnext3;

        private BorderPanel _panel1;

        private StatusStripItem _timeStripItem;

        private StatusStripProgressBar _statusStripProgressBar;

        private ComboBox _comboBox1;

        private Table _table1;

        public MainScoreboard()
            : base(new Panel())
        {
        }

        protected override void Initialize()
        {
            _checkbox1 = new Checkbox { Location = new Point { Left = 3, Top = 4 }, Text = "Checkbox tentative" };
            _checkbox1.CheckedChanged += checkbox1_CheckedChanged;
            RootPanel.AddChild(_checkbox1);

            _checkbox2 = new Checkbox { Location = new Point { Left = 3, Top = 6 }, IsChecked = true, Text = "Checkbox checked" };
            RootPanel.AddChild(_checkbox2);

            _checkbox3 = new Checkbox { Location = new Point { Left = 3, Top = 8 }, IsChecked = false, Text = "Checkbox unchecked" };
            RootPanel.AddChild(_checkbox3);

            _checkbox4 = new Checkbox { Location = new Point { Left = 3, Top = 10 }, IsChecked = false, Text = "Checkbox disabled", IsEnabled = false };
            RootPanel.AddChild(_checkbox4);

            _label1 = new Label { Location = new Point { Left = 3, Top = 12 }, Text = "Label" };
            RootPanel.AddChild(_label1);

            _radioButton1 = new RadioButton { Location = new Point { Left = 3, Top = 14 }, Text = "Radio unchecked" };
            RootPanel.AddChild(_radioButton1);

            _radioButton2 = new RadioButton { Location = new Point { Left = 3, Top = 16 }, Text = "Radio checked", IsChecked = true };
            RootPanel.AddChild(_radioButton2);

            _radioButton3 = new RadioButton { Location = new Point { Left = 3, Top = 18 }, Text = "Radio checked", IsChecked = true, IsEnabled = false };
            RootPanel.AddChild(_radioButton3);

            _textBox1 = new TextBox { Location = new Point { Left = 3, Top = 20 } };
            _textBox1.TextChanged += textBox1_TextChanged;
            RootPanel.AddChild(_textBox1);

            _button1 = new Button { Location = new Point { Left = 3, Top = 22 }, Width = 10 };
            _button1.Pressed += button1_Pressed;
            RootPanel.AddChild(_button1);

            _button2 = new Button { Location = new Point { Left = 16, Top = 22 }, Width = 10, IsEnabled = false, Text = "Disabled button" };
            _button2.Pressed += button2_Pressed;
            RootPanel.AddChild(_button2);

            _progressBar1 = new ProgressBar { Location = new Point { Left = 3, Top = 2 }, Width = 74 };
            RootPanel.AddChild(_progressBar1);

            _bnext1 = new Button { Location = new Point { Left = 40, Top = 22 }, Width = 10, Text = "Next 1", Tag = 1 };
            _bnext1.Pressed += bNextPressed;
            RootPanel.AddChild(_bnext1);

            _bnext2 = new Button { Location = new Point { Left = 51, Top = 22 }, Width = 10, Text = "Next 2", Tag = 2 };
            _bnext2.Pressed += bNextPressed;
            RootPanel.AddChild(_bnext2);

            _bnext3 = new Button { Location = new Point { Left = 62, Top = 22 }, Width = 10, Text = "Next 3", Tag = 3 };
            _bnext3.Pressed += bNextPressed;
            RootPanel.AddChild(_bnext3);

            _panel1 = new BorderPanel
            {
                Location = new Point { Left = 28, Top = 3 },
                Size = new Size { Height = 7, Width = 25 }
            };
            RootPanel.AddChild(_panel1);

            var subRadiobuton1 = new RadioButton
            {
                Location = new Point
                {
                    Left = 1,
                    Top = 1
                }
            };
            _panel1.AddChild(subRadiobuton1);

            var subRadiobuton2 = new RadioButton
            {
                Location = new Point
                {
                    Left = 1,
                    Top = 3
                }
            };
            _panel1.AddChild(subRadiobuton2);

            var subRadiobuton3 = new RadioButton
            {
                Location = new Point
                {
                    Left = 1,
                    Top = 5
                }
            };
            _panel1.AddChild(subRadiobuton3);


            _comboBox1 = new ComboBox
            {
                Location = new Point {Left = 29, Top = 12},
                VisibleItemsCount = 7
            };
            _comboBox1.Items.AddRange(new[]
            {
                "EntityFX", "Green.Dragon", "Guzalianna", "Prozz", "Aik2029", "Zombie", "Wesker", "Perez", "Chuvak", "Magistr", "Mad", "XOBAH",
            });
            RootPanel.AddChild(_comboBox1);

            RootPanel.AddChild(new Checkbox { Location = new Point { Left = 29, Top = 14 }, Text = "Checkbox for overlapp" });

            RootPanel.AddChild(new BorderPanel { Location = new Point { Left = 29, Top = 16 }, Size = new Size {Width = 15, Height = 3} });

            StatusStrip = new StatusStrip();
            StatusStrip.AddChild(new StatusStripLabel
            {
                Text = "Item 1"
            });

            StatusStrip.AddChild(new StatusStripLabel
            {
                Text = "Item 2"
            });

            _timeStripItem = new StatusStripLabel
            {
                Text = "Item 3",
                ItemLocation = StatusStripItemLocationEnum.Right
            };
            StatusStrip.AddChild(_timeStripItem);

            StatusStrip.AddChild(new StatusStripLabel
            {
                Text = "Item 4",
                ItemLocation = StatusStripItemLocationEnum.Right
            });

            StatusStrip.AddChild(new StatusStripButton
            {
                Text = "But 1",
                ItemLocation = StatusStripItemLocationEnum.Left
            });

            StatusStrip.AddChild(new StatusStripButton
            {
                Text = "But 2",
                ItemLocation = StatusStripItemLocationEnum.Left
            });

            _statusStripProgressBar = new StatusStripProgressBar
            {
                Text = "P: {0}",
                Width = 14,
                Minimum = 0,
                Value = 16,
                Maximum = 25
            };
            StatusStrip.AddChild(_statusStripProgressBar);

            Initializetable();

            _timer.Elapsed += timer_Elapsed;
            _timer.Start();

            _timer2.Elapsed += _timer2_Elapsed;
            _timer2.Start();
        }

        private void Initializetable()
        {
            _table1 = new Table();

            _table1.Rows.AddRange(new List<ITableRow<ITableCell<ControlBase>,ControlBase>> {
                new TableHeaderRow
                {
                    Cells = new List<TableHeaderCell>
                    {
                        new TableHeaderCell(), new TableHeaderCell(), new TableHeaderCell()
                    }
                },
                new TableViewRow
                {
                    Cells = new List<TableCellBase>
                    {
                        new TableCheckboxCell(), new TableViewCell(), new TableViewCell()
                    }
                }
            });
        }

        private void _timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_statusStripProgressBar.Value >= _statusStripProgressBar.Maximum)
            {
                _statusStripProgressBar.Value = _statusStripProgressBar.Minimum;
            }
            _statusStripProgressBar.Increment();
        }

        private void bNextPressed(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                switch ((int)b.Tag)
                {
                    case 1:
                        Navigate<SecondScoreboard>();
                        break;
                    case 2:
                        Navigate<ThirdScoreboard>();
                        break;
                    case 3:
                        MessageBox.Show("Do you really want to do this?", MessageBoxResult, "Some title", MessageBoxTypeEnum.Question, MessageBoxButtonsEnum.YesNo);
                        break;

                }
            }
        }

        private void MessageBoxResult(MessageBoxResultEnum result)
        {

        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_progressBar1.Value >= _progressBar1.Maximum)
            {
                _progressBar1.Value = _progressBar1.Minimum;
            }
            _progressBar1.Increment();
            _timeStripItem.Text = DateTime.Now.ToLongTimeString();
        }

        private void button2_Pressed(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkbox1_CheckedChanged(ToggleButtonBase sender)
        {

        }

        private void button1_Pressed(object sender, EventArgs e)
        {

        }

        protected override void OnKeyPressed(KeyPressEventArgs e)
        {

        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }

    class SecondScoreboard : Scoreboard
    {
        private Checkbox _checkbox1;

        private Image _img1;

        public SecondScoreboard()
            : base(new Panel())
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            _checkbox1 = new Checkbox { Location = new Point { Left = 3, Top = 4 }, Text = "Checkbox tentative" };
            RootPanel.AddChild(_checkbox1);

            _img1 = new Image()
            {
            };
        }

        protected override void OnKeyPressed(KeyPressEventArgs e)
        {
            if (e.KeyInfo.Key == ConsoleKey.D1)
            {
                Navigate<FourthScoreboard>();
            }
        }
    }

    class ThirdScoreboard : Scoreboard
    {
        public ThirdScoreboard()
            : base(new Panel())
        {
        }
    }

    class FourthScoreboard : Scoreboard
    {
        public FourthScoreboard()
            : base(new Panel())
        {
        }
    }
}
