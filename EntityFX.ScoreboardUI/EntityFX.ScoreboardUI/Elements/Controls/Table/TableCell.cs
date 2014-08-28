using System.Collections.Generic;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public interface ITableCell<out TControl> where TControl : ControlBase
    {
        TControl RootControl { get; }
    }

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