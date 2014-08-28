namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public abstract class StatusStripItem : ControlBase
    {
        public ControlBase InternalControl { get; protected set; }

        public StatusStripItemLocationEnum ItemLocation { get; set; }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (InternalControl != null)
                    InternalControl.Text = value;
            }
        }

        public override bool IsFocused {
            get { return base.IsFocused; }
            internal set
            {
                base.IsFocused = value;
                InternalControl.IsFocused = value;
            }
        }

    }
}