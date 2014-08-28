namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public abstract class ToggleButtonBase : ButtonBase
    {
        protected bool? IsCheckedInternal;

        public virtual bool? IsChecked
        {
            get { return IsCheckedInternal; }
            set
            {
                IsCheckedInternal = value;
                CheckedChangeEventHandler handler = CheckedChanged;
                if (handler != null)
                {
                    handler(this);
                }
                ReRender();
            }
        }

        public override void Press(KeyPressEventArgs e)
        {
            base.Press(e);
            IsChecked = IsChecked != null ? !IsChecked : true;
        }

        public event CheckedChangeEventHandler CheckedChanged;
    }
}