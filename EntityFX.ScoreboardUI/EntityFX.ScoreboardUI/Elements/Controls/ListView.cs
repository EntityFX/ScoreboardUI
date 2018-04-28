using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI.Elements.Controls
{

    public class ListView<TItem> : ListControlBase<TItem>, ILinearable, ICloneable, IListView where TItem : class
    {
        public int Width { get; set; }

        public int ItemHeight { get; set; } = 1;

        public ListViewItem Template { get; set; }

        public IEnumerable<ListViewItem> ItemsControls { get; private set; }

        public event EventHandler<ItemInformation<TItem>> ItemDataBound;

        protected override void OnItemsChanged()
        {
            var itemsControls = new List<ListViewItem>();
            var top = 0;
            if (Template == null)
            {
                new InvalidOperationException("Template is null");
            }
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
                ItemDataBound?.Invoke(this, new ItemInformation<TItem> { Item = item, ItemControls = itemControls.Controls.ToArray() });
            }
            Clear();
            ItemsControls = itemsControls;
        }


        public override void Clear()
        {
            if (ItemsControls == null) return;
            foreach (var item in ItemsControls)
            {
                foreach (var control in item.Controls)
                {
                    ScoreboardContext.CurrentState.RemoveFromControlList(control);
                    control.Dispose();
                }
            }
        }

        public class ItemInformation<TItem> where TItem : class
        {
            public ControlBase[] ItemControls { get; set; }

            public TItem Item { get; set; }
        }

        public override void ClearEvents()
        {
            base.ClearEvents();
            ItemDataBound = null;
        }
    }
}