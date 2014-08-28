using System.Collections.Generic;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class Control<T> : ControlBase where T : ControlBase
    {
        private bool _renderChildren = true;
        
        private readonly IList<T> _controls = new List<T>();

        public IEnumerable<T> Controls
        {
            get { return _controls; }
        }

        protected override void ReRender()
        {
            _renderChildren = false;
            Render();
            _renderChildren = true;
        }

        public virtual void AddChild(T childControl)
        {
            _controls.Add(childControl);
            childControl.Parent = this;
            ScoreboardContext.CurrentState.AddToControlList(childControl);
            childControl.CompositionLevel = CompositionLevel + 1;
        }

        protected override void PostRender()
        {
            if (_renderChildren)
            {
                ((List<T>)_controls).ForEach(c => c.Render());
            }
        }

    }
}