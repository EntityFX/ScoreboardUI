using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public abstract class TableCell<TControl> : ControlBase, ISizable, ITableCell<TControl> where TControl : ControlBase
    {
        public Size Size { get; set; }

        TControl ITableCell<TControl>.RootControl
        {
            get { return RootControl; }
        }

        public TControl RootControl { get; protected set; }
    }
}