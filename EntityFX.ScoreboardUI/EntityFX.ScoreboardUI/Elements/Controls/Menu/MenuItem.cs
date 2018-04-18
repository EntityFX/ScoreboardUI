using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFX.ScoreboardUI.Elements.Controls.Menu
{
    public class MenuItem<TData> : ControlBase
    {
        public ControlBase InternalControl { get; protected set; }

        public TData Data { get; set; }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (InternalControl != null)
                    InternalControl.Text = value;
            }
        }

        public override bool IsFocused
        {
            get { return base.IsFocused; }
            internal set
            {
                base.IsFocused = value;
                InternalControl.IsFocused = value;
            }
        }
    }
}
