using System.Collections.Generic;

namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public interface ITableRow<out TCell, out TControl> where TCell : ITableCell<TControl> where TControl : ControlBase
    {
        IReadOnlyList<TCell> Cells { get; }
    }
}