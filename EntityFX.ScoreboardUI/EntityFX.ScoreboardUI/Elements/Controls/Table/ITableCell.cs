namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public interface ITableCell<out TControl> where TControl : ControlBase
    {
        TControl RootControl { get; }
    }
}