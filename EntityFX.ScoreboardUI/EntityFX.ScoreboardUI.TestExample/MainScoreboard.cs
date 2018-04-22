using System;
using System.Collections.Generic;
using System.Timers;
using EntityFX.ScoreboardUI;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.Charts;
using EntityFX.ScoreboardUI.Elements.Controls.Menu;
using EntityFX.ScoreboardUI.Elements.Controls.StatusBar;
using EntityFX.ScoreboardUI.Elements.Controls.Table;
using EntityFX.ScoreboardUI.Elements.MessageBox;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFx.ScorbordUI.TestExample
{
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

        private DateTimeBox _dateTimeBox1;

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

        private Image _image;

        private Menu _menu;

        private NumericBox _numericBox1;
        private PlotChart _plotChart;

        public MainScoreboard()
            : base(new Panel())
        {
        }

        public override void Initialize()
        {
            _menu = new Menu();
            var menuButton1 = new MenuItemButton<object>()
            {
                Text = "Menu 1",
            };
            menuButton1.AddChild(new MenuItemButton<object>()
            {
                Text = "Submenu 1.1"
            });
            _menu.AddChild(menuButton1);
            var menuButton2 = new MenuItemButton<object>()
            {
                Text = "Menu 2",
            };
            menuButton2.AddChild(new MenuItemButton<object>()
            {
                Text = "Submenu 2.1"
            });
            menuButton2.AddChild(new MenuItemButton<object>()
            {
                Text = "Submenu 2.2"
            });
            _menu.AddChild(menuButton2);
            Menu = _menu;

            _checkbox1 = new Checkbox { Location = new Point { Left = 3, Top = 5 }, Text = "Checkbox tentative" };
            _checkbox1.CheckedChanged += checkbox1_CheckedChanged;
            RootPanel.AddChild(_checkbox1);

            _checkbox2 = new Checkbox { Location = new Point { Left = 3, Top = 7 }, IsChecked = true, Text = "Checkbox checked" };
            RootPanel.AddChild(_checkbox2);

            _checkbox3 = new Checkbox { Location = new Point { Left = 3, Top = 9 }, IsChecked = false, Text = "Checkbox unchecked" };
            RootPanel.AddChild(_checkbox3);

            _checkbox4 = new Checkbox { Location = new Point { Left = 3, Top = 11 }, IsChecked = false, Text = "Checkbox disabled", IsEnabled = false };
            RootPanel.AddChild(_checkbox4);

            _label1 = new Label { Location = new Point { Left = 3, Top = 19 }, Text = "Label" };
            RootPanel.AddChild(_label1);

            _radioButton1 = new RadioButton { Location = new Point { Left = 3, Top = 13 }, Text = "Radio unchecked" };
            RootPanel.AddChild(_radioButton1);

            _radioButton2 = new RadioButton { Location = new Point { Left = 3, Top = 15 }, Text = "Radio checked", IsChecked = true };
            RootPanel.AddChild(_radioButton2);

            _radioButton3 = new RadioButton { Location = new Point { Left = 3, Top = 17 }, Text = "Radio checked", IsChecked = true, IsEnabled = false };
            RootPanel.AddChild(_radioButton3);

            _textBox1 = new TextBox { Location = new Point { Left = 29, Top = 16 } };
            _textBox1.TextChanged += textBox1_TextChanged;
            RootPanel.AddChild(_textBox1);

            _dateTimeBox1 = new DateTimeBox() { Location = new Point { Left = 29, Top = 18 } };
            RootPanel.AddChild(_dateTimeBox1);

            _numericBox1 = new NumericBox() { Location = new Point { Left = 29, Top = 20 } };
            RootPanel.AddChild(_numericBox1);

            _button1 = new Button { Location = new Point { Left = 3, Top = 22 }, Width = 10 };
            _button1.Pressed += button1_Pressed;
            RootPanel.AddChild(_button1);

            _button2 = new Button { Location = new Point { Left = 16, Top = 22 }, Width = 10, IsEnabled = false, Text = "Disabled button" };
            _button2.Pressed += button2_Pressed;
            RootPanel.AddChild(_button2);

            _progressBar1 = new ProgressBar { Location = new Point { Left = 3, Top = 3 }, Width = 74, TextAligment = TitleAligment.Left};
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
                Location = new Point { Left = 28, Top = 4 },
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
                Location = new Point { Left = 29, Top = 12 },
                VisibleItemsCount = 7
            };
            _comboBox1.Items.AddRange(new[]
            {
                new ComboBoxItem(){ Key= 1, Text="EntityFX"},
                new ComboBoxItem(){ Key= 2, Text="Green.Dragon"},
                new ComboBoxItem(){ Key= 3, Text="Guzalianna"},
                new ComboBoxItem(){ Key= 4, Text="Prozz"},
                new ComboBoxItem(){ Key= 5, Text="Aik2029"},
                new ComboBoxItem(){ Key= 6, Text="Zombie"},
                new ComboBoxItem(){ Key= 7, Text="Wesker"},
                new ComboBoxItem(){ Key= 8, Text="Perez"},
                new ComboBoxItem(){ Key= 9, Text="Chuvak"},
                new ComboBoxItem(){ Key= 10, Text="Magistr"},
                new ComboBoxItem(){ Key= 11, Text="Mad"},
                new ComboBoxItem(){ Key= 12, Text="XOBAH"}
            });
            RootPanel.AddChild(_comboBox1);

            RootPanel.AddChild(new Checkbox { Location = new Point { Left = 29, Top = 14 }, Text = "Checkbox for overlapp" });

            _image = Image.FromString(
@"───▄▄▄
─▄▀░▄░▀▄
─█░█▄▀░█
─█░▀▄▄▀█▄█▄▀
▄▄█▄▄▄▄███▀
");
            _image.Location = new Point() { Left = 55, Top = 4 };
            RootPanel.AddChild(_image);


            _plotChart = new PlotChart()
            {
                Size = new Size() { Height = 10, Width = 30 },
                Location = new Point() { Left = 48, Top = 11 },
                PlotSymbol = ':'
            };
            _plotChart.Points = GenerateSinChartPoints(_plotChart.Size, 7);
            RootPanel.AddChild(_plotChart);

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

            _table1.Rows.AddRange(new List<ITableRow<ITableCell<ControlBase>, ControlBase>> {
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

        private int _shiftX = 0;

        private void _timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_statusStripProgressBar.Value >= _statusStripProgressBar.Maximum)
            {
                _statusStripProgressBar.Value = _statusStripProgressBar.Minimum;
            }
            _statusStripProgressBar.Increment();
            _plotChart.Points = GenerateSinChartPoints(_plotChart.Size, _shiftX);
            _shiftX = _shiftX < _plotChart.Size.Width ? _shiftX + 1 : 0;
        }

        private void bNextPressed(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                switch ((int)b.Tag)
                {
                    case 1:
                        Navigate<SecondScoreboard>("Some Data");
                        break;
                    case 2:
                        Navigate<ThirdScoreboard>();
                        break;
                    case 3:
                        MessageBox.Show("Do you really want to do this?", MessageBoxResult, "Some title", MessageBoxTypeEnum.Error, MessageBoxButtonsEnum.YesNo);
                        break;

                }
            }
        }

        private void MessageBoxResult(MessageBoxResultEnum result, object data)
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

        private Point[] GenerateSinChartPoints(Size chartSize, int shiftX)
        {
            var points = new List<Point>();
            for (int i = 0; i < chartSize.Width; i++)
            {
                int step = chartSize.Width / 4;
                int shiftY = chartSize.Height / 2;
                int x, y;
                x = i + shiftX;
                y = (int)Math.Floor(Math.Sin(Math.PI / step * x) * shiftY) + shiftY;
                points.Add(new Point() { Left = i, Top = y });
            }
            return points.ToArray();
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}