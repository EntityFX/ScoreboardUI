using System.Collections.Generic;
using System.Linq;

namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public class Table : ControlBase
    {
        public IList<TableCellBase> Columns
        {
            get { return null; }
        }

        public List<ITableRow<ITableCell<ControlBase>, ControlBase>> Rows { get; set; }

        public Table()
        {
            Rows = new List<ITableRow<ITableCell<ControlBase>, ControlBase>>();
        }
    }
}