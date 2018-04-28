using System;
using System.Collections.Generic;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class ListControlBase<TListItem> : ControlBase where TListItem : class
    {
        private List<TListItem> _items;

        private int _selectedIndexInternal;

        public ListControlBase(List<TListItem> items = null)
        {
            Items = items ?? new List<TListItem>();
        }

        public List<TListItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnItemsChanged();
            }
        }

        public TListItem SelectedValue
        {
            get
            {
                if (Items != null && Items.Count > 0)
                {
                    return Items[SelectedIndex];
                }
                return null;
            }
            set
            {
                if (Items != null)
                {
                    Items[SelectedIndex] = value;
                }
            }
        }


        public int SelectedIndex
        {
            get
            {
                return Items == null ? -1 : _selectedIndexInternal;
            }
            set
            {
                if (Items != null && (value > Items.Count || value <= -1))
                {
                    throw new ArgumentOutOfRangeException("Selected Index");
                }
                _selectedIndexInternal = value;
            }
        }

        public event EventHandler<TListItem> SelectedValueChanged;

        protected virtual void OnItemsChanged()
        {
            
        }

        public virtual void Clear()
        {

        }

        public override void Initialize()
        {
            OnItemsChanged();
            base.Initialize();
        }

        protected virtual void OnSelectedValueChanged(TListItem e)
        {
            SelectedValueChanged?.Invoke(this, e);
        }
    }
}