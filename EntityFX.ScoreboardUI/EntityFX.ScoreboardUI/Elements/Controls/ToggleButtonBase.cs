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
                ReRender();
            }
        }

        public override void Press(KeyPressEventArgs e)
        {
            base.Press(e);
            IsChecked = IsChecked != null ? !IsChecked : true;
            CheckedChanged?.Invoke(this);
        }

        public event CheckedChangeEventHandler CheckedChanged;

        public override void ClearEvents()
        {
            base.ClearEvents();
            CheckedChanged = null;
        }
    }
}