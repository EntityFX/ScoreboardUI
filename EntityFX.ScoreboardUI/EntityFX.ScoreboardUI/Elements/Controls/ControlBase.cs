using System;
using System.Collections.Generic;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Scoreboards;
using EntityFX.ScoreboardUI.Exceptions;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public abstract class ControlBase : UiElement
    {

        private readonly object _object = new object();
        private Scoreboard _parentScoreboard;


        private string _textValue;

        public ControlBase()
        {
            Position = PositionEnum.ABSOLUTE;
            Text = GetType().Name;
            DisplayText = true;
            CanFocus = true;
            CompositionLevel = 0;
        }

        public UiElement Parent { get; internal set; }

        public PositionEnum Position { get; set; }

        public bool CanFocus { get; set; }

        public virtual bool IsFocused { get; internal set; }

        public int TabIndex { get; set; }

        public int CompositionLevel { get; internal set; }

        public virtual string Text
        {
            get { return _textValue; }
            set
            {
                lock (_object)
                {
                    _textValue = value;
                    ReRender();
                    OnTextChanged(new EventArgs());
                }
            }
        }

        internal virtual string VisibleText
        {
            get { return Text; }
        }

        public Scoreboard ParentScoreboard
        {
            get
            {
                if (_parentScoreboard != null)
                {
                    return _parentScoreboard;
                }
                UiElement parent = this;
                while (!(parent is Scoreboard) && parent != null)
                {
                    parent = (parent as ControlBase).Parent;
                }
                _parentScoreboard = parent as Scoreboard;
                return _parentScoreboard;
            }
        }

        public bool DisplayText { get; set; }

        public event EventHandler TextChanged;

        public void Show()
        {
            IsVisible = true;
            ReRender();
        }

        public void Hide()
        {
            IsVisible = false;
            ReRender();
        }

        public virtual void Focus()
        {
            if (CanFocus)
            {
                IsFocused = true;
                ScoreboardContext.CurrentState.ChangeFocus(this);
            }
            else
            {
                throw new ControlCantBeFocusedException("Control cannot be focused", this);
            }
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            EventHandler handler = TextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        protected virtual void ReRender()
        {
            Render();
        }

        protected override bool PreRender()
        {
            return base.PreRender() && ParentScoreboard.IsVisible;
        }

        protected override void PostRender()
        {
            
        }

        public Point AbsoluteLocation()
        {
            Point res = Location;
            UiElement parent = Parent;
            ControlBase control;
            do
            {
                control = parent as ControlBase;
                res = res + parent.Location;
                if (control != null) parent = control.Parent;
            } while (control != null);
            return res;
        }
    }
}