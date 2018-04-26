using System.Collections.Generic;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Control<T> : ControlBase where T : ControlBase
    {
        private bool _renderChildren = true;
        
        protected readonly IList<T> controls = new List<T>();

        public IEnumerable<T> Controls
        {
            get { return controls; }
        }

        protected override void ReRender()
        {
            _renderChildren = false;
            Render();
            _renderChildren = true;
        }

        public virtual void AddChild(T childControl)
        {
            controls.Add(childControl);
            childControl.Parent = this;
            ScoreboardContext.CurrentState.AddToControlList(childControl);
            childControl.CompositionLevel = CompositionLevel + 1;
        }

        public virtual void RemoveChild(T childControl)
        {
            childControl.Parent = null;
            ScoreboardContext.CurrentState.RemoveFromControlList(childControl);
            controls.Remove(childControl);
        }

        protected override void PostRender()
        {
            if (_renderChildren)
            {
                ((List<T>)controls).ForEach(c => c.Render());
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var rootPanelControl in Controls)
            {
                rootPanelControl.Initialize();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var control in Controls)
                {
                    control.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}