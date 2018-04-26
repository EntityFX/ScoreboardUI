using System.Collections.Generic;
using System.Linq;
using EntityFX.ScoreboardUI.Elements.Controls;
using EntityFX.ScoreboardUI.Elements.Controls.Menu;
using EntityFX.ScoreboardUI.Elements.Controls.StatusBar;

namespace EntityFX.ScoreboardUI
{
    internal class ScoreboardState : IScoreboardState
    {
        private readonly List<ControlBase> _controlsList = new List<ControlBase>();
        private int _internalFocusIndex;
        private int _internalTabIndex;

        public ScoreboardState()
        {
            ScoreboardInitialized = false;
        }

        public void ChangeFocus(ControlBase control)
        {
            ControlBase oldFocusedElement = FocusedElement;
            SetFocus(control);
            _internalFocusIndex = _controlsList.IndexOf(control);
            oldFocusedElement?.Render();
            control.Render();
        }

        public void SetFocus(ControlBase control)
        {
            foreach (ControlBase itemControl in _controlsList)
            {
                itemControl.IsFocused = false;
            }
            control.IsFocused = true;
        }

        public int InternalTabIndex
        {
            get { return _internalTabIndex; }
        }

        public IEnumerable<ControlBase> SortedControlsByFocusIndex { get; private set; }

        public IEnumerable<ControlBase> ControlsList
        {
            get { return _controlsList; }
        }

        public bool ScoreboardInitialized { get; private set; }
        public bool IsNavigating { get; set; }

        public ControlBase FocusedElement
        {
            get { return _controlsList.Count > 0 && _internalFocusIndex >=0  ? _controlsList[_internalFocusIndex] : null; }
        }

        public void Initialized()
        {
            ScoreboardInitialized = true;
            SortControlsByFocusIndex();
        }

        public void AddToControlList(ControlBase control)
        {
            _internalTabIndex++;
            control.TabIndex = _internalTabIndex;
            _controlsList.Add(control);
            if (ScoreboardInitialized)
            {
                SortControlsByFocusIndex();
            }
        }

        public void NextFocus()
        {
            ControlBase control;
            ControlBase previousControl = null;
            if (_controlsList.Count == 0) return;

            if (_controlsList.Count == 1)
            {
                control = _controlsList[0];
            }
            else
            {
                previousControl = _internalFocusIndex == -1 ? _controlsList[0] : _controlsList[_internalFocusIndex];
                do
                {
                    _internalFocusIndex++;

                    if (_internalFocusIndex == _controlsList.Count)
                    {
                        _internalFocusIndex = 0;
                    }

                    control = _controlsList[_internalFocusIndex];
                } while (!control.IsEnabled || (!(control is ButtonBase)
                                                && !(control is TextBox)
                                                && !(control is StatusStripButton)
                                                && !(control is MenuItemButton<object>)
                                                && !(control is ComboBox)));

            }

            SetFocus(control);
            previousControl?.Render();
            control.Render();
        }

        public void PreviousFocus()
        {
            ControlBase control;
            ControlBase oldFocusedElement = FocusedElement;
            if (_controlsList.Count == 0) return;

            var previousControl = _internalFocusIndex == _controlsList.Count - 1 ? _controlsList[0] : _controlsList[_internalFocusIndex];
            do
            {
                _internalFocusIndex--;

                if (_internalFocusIndex == -1)
                {
                    _internalFocusIndex = _controlsList.Count - 1;
                }

                control = _controlsList[_internalFocusIndex];
            } while (!control.IsEnabled || (!(control is ButtonBase)
                                            && !(control is TextBox)
                                            && !(control is StatusStripButton)
                                            && !(control is MenuItemButton<object>)
                                            && !(control is ComboBox)));

            SetFocus(control);
            oldFocusedElement?.Render();
            previousControl.Render();
            control.Render();
        }

        public void ResetFocus()
        {
            _internalFocusIndex = -1;
            NextFocus();
        }

        private void SortControlsByFocusIndex()
        {
            SortedControlsByFocusIndex = _controlsList.OrderBy(key => key.TabIndex);
        }

        public void RemoveFromControlList(ControlBase control)
        {

            var controls = _controlsList.Where(i => i.TabIndex > control.TabIndex);
            var controlIndex = _controlsList.IndexOf(control);
            if (controlIndex >=0 && controlIndex < _controlsList.Count)
            {
               var nextItems = _controlsList.GetRange(controlIndex, _controlsList.Count - controlIndex);
                foreach (var item in nextItems)
                {
                    item.TabIndex--;
                }
            }


            _controlsList.Remove(control);
            if (ScoreboardInitialized)
            {
                SortControlsByFocusIndex();
            }
        }
    }
}