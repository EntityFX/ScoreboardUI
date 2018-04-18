using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class ListViewItem : Control<ControlBase>, ISizable
    {
        public Size Size { get; set; }

        public override void AddChild(ControlBase childControl)
        {
            controls.Add(childControl);
            childControl.Parent = this;
        }
    }
}