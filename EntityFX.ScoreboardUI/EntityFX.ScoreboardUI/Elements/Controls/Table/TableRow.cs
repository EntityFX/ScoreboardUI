using System.Collections.Generic;

namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public class TableRow<TCell, TControl> : ControlBase, ITableRow<TCell, TControl> where TCell : TableCell<TControl> where TControl : ControlBase
    {
        IReadOnlyList<TCell> ITableRow<TCell, TControl>.Cells { get { return Cells; } }

        public List<TCell> Cells { get; set; }
}
}