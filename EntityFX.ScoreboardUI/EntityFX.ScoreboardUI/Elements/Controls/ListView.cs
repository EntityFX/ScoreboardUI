using System;
using System.Collections.Generic;
using System.Diagnostics;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls
{

    public class ListView: ListControlBase<object>, ILinearable, ICloneable
    {
        public int Width { get; set; }

        public int ItemHeight { get; set; } = 1;

        public ListViewItem Template { get; set; }

        public IEnumerable<ListViewItem> ItemsControls { get; private set; }

        public event EventHandler<ItemInformation> ItemDataBound;

        protected override void OnItemsChanged()
        {
            var itemsControls = new List<ListViewItem>();
            var top = 0;
            foreach (var item in Items)
            {
                var itemControls = (ListViewItem) Template.Clone();
                itemControls.Location = new Point() {Left = 0, Top = top};
                top += ItemHeight;
                itemControls.Position = PositionEnum.RELATIVE;
                itemControls.Parent = this;
                foreach (var control in itemControls.Controls)
                {
                    ScoreboardContext.CurrentState.AddToControlList(control);
                    control.CompositionLevel = CompositionLevel + 1;
                }
                itemsControls.Add(itemControls);
                ItemDataBound?.Invoke(this, new ItemInformation { Item = item, ItemControls = itemControls });
            }
            ItemsControls = itemsControls;
        }

        public class ItemInformation
        {
            public ListViewItem ItemControls { get; set; }

            public object Item { get; set; }
        }
    }
}