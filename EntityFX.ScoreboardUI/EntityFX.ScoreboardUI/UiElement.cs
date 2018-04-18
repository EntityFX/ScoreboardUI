using System;
using System.ComponentModel;
using EntityFX.ScoreboardUI.Drawing;

namespace EntityFX.ScoreboardUI
{
    public abstract class UiElement
    {
        private static readonly object _syncObject = new object();

        public UiElement()
        {
            IsVisible = true;
            IsEnabled = true;
            BackgroundColor = ConsoleColor.Blue;
            ForegroundColor = ConsoleColor.Yellow;
        }

        public Guid Uid { get; set; }

        public string Name { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEnabled { get; set; }

        public Point Location { get; set; }

        public ConsoleColor ForegroundColor { get; set; }

        public ConsoleColor BackgroundColor { get; set; }

        [TypeConverter(typeof (StringConverter))]
        [Bindable(true)]
        public Object Tag { get; set; }

        public event EventHandler Rendered;

        public event KeyPressEventHandler KeyPress;

        public virtual void PressKey(KeyPressEventArgs e)
        {
            KeyPressEventHandler handler = KeyPress;
            if (handler != null)
            {
                handler(this, new KeyPressEventArgs
                {
                    KeyInfo = e.KeyInfo
                });
            }
            OnKeyPressed(e);
        }

        protected virtual void OnKeyPressed(KeyPressEventArgs e)
        {
        }

        protected virtual void OnRendered(EventArgs e)
        {
            EventHandler handler = Rendered;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual bool PreRender()
        {
            return ScoreboardContext.CurrentState.ScoreboardInitialized && !ScoreboardContext.CurrentState.IsNavigating;
        }

        protected abstract void PostRender();

        public virtual void Render()
        {
            lock (_syncObject)
            {
                if (IsVisible && PreRender())
                {
                    ScoreboardContext.RenderEngine.Render(this);

                    OnRendered(new EventArgs());
                    PostRender();
                }
            }
        }

        public virtual void Initialize()
        {
        }

        protected virtual bool PreInitialize()
        {
            return true;
        }

        protected virtual void PostInitialize()
        {
        }

        public event EventHandler<EventArgs> Initialized;

        protected virtual void OnInitialized(EventArgs e)
        {
            EventHandler<EventArgs> handler = Initialized;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}