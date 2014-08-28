namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public class TableViewCell : TableCellBase
    {
        public TableViewCell()
        {
            RootControl = new Label()
            {
                Text = Text,
                Parent = this
            };
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (RootControl != null)
                    RootControl.Text = value;
            }
        }

        public override bool IsFocused
        {
            get { return base.IsFocused; }
            internal set
            {
                base.IsFocused = value;
                RootControl.IsFocused = value;
            }
        }
    }
}