using System;
using System.Collections.Generic;
using System.Reflection;
using EntityFX.ScoreboardUI.Drawing;
using EntityFX.ScoreboardUI.Elements.Scoreboards;
using EntityFX.ScoreboardUI.Exceptions;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public abstract class ControlBase : UiElement, ICloneable
    {

        private readonly object _object = new object();
        private Scoreboard _parentScoreboard;


        private string _textValue;
        private bool _isFocused;

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

        public virtual bool IsFocused
        {
            get { return _isFocused; }
            internal set
            {
                _isFocused = value;
                OnFocusChanged(value);
            }
        }

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

        public event EventHandler<bool> FocusChanged;

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

        public virtual void OnFocusChanged(bool isFocused)
        {
            FocusChanged?.Invoke(this, isFocused);
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
            return base.PreRender() && ParentScoreboard != null && ParentScoreboard.IsVisible;
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
                if (parent != null)
                {
                    res = res + parent.Location;
                }

                if (control != null)
                {
                    parent = control.Parent;
                }
            } while (control != null);
            return res;
        }

        public object Clone()
        {
            return this.Copy();
        }
    }
}       