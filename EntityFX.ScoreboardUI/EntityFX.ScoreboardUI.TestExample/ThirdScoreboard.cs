using System.Collections.Generic;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFx.ScorbordUI.TestExample
{
    class ThirdScoreboard : Scoreboard
    {
        class Item
        {
            public bool BoolValue { get; set; }

            public string StringValue { get; set; }

            public string SecondStringValue { get; set; }
        }

        private ListView<Item> _listView;

        private Button _button;

        public ThirdScoreboard()
            : base(new Panel())
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var template = new ListViewItem()
            {
            };
            template.AddChild(new Checkbox()
            {
                Text = ""
            });
            template.AddChild(new Label()
            {
                Text = "",
                Location = new Point() { Left = 4 }
            });
            template.AddChild(new Label()
            {
                Text = "",
                Location = new Point() { Left = 14 }
            });
            template.AddChild(new Button()
            {
                Text = "Edit",
                Location = new Point() { Left = 30 }
            });

            _listView = new ListView<Item>()
            {
                Location = new Point() { Left = 2, Top = 2 },
                Template = template
            };
            _listView.ItemDataBound += _listView_ItemDataBound;

            _listView.Items = new List<Item>()
            {
                new Item()
                {
                    BoolValue = true,
                    StringValue = "Value 1",
                    SecondStringValue = "Long Value 1"
                },                new Item()
                {
                    BoolValue = false,
                    StringValue = "Value 2",
                    SecondStringValue = "Long Value 2"
                }
            };
            RootPanel.AddChild(_listView);

            _button = new Button()
            {
                Location = new Point() { Left = 2, Top = Size.Height - 3 },
                Text = "Button"
            };
            RootPanel.AddChild(_button);
        }

        private void _listView_ItemDataBound(object sender, ListView<Item>.ItemInformation<Item> e)
        {
            ((Checkbox)e.ItemControls[0]).IsChecked = e.Item.BoolValue;
            ((Label)e.ItemControls[1]).Text = e.Item.StringValue;
            ((Label)e.ItemControls[2]).Text = e.Item.SecondStringValue;
        }

        protected override bool PreRender()
        {
            return base.PreRender();
        }
    }
}