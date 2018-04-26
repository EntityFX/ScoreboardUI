using System.Collections.Generic;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public interface IListView
    {
        int ItemHeight { get; set; }
        IEnumerable<ListViewItem> ItemsControls { get; }
        ListViewItem Template { get; set; }
        int Width { get; set; }
    }
}