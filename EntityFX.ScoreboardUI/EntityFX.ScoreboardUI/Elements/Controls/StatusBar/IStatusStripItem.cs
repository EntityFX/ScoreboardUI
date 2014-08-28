namespace EntityFX.ScoreboardUI.Elements.Controls.StatusBar
{
    public interface IStatusStripItem<TControl> where TControl : ControlBase
    {
        TControl InternalControl { get; set; }

        StatusStripItemLocationEnum ItemLocation { get; set; }
    }
}