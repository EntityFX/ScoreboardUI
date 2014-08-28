using System.Collections.Generic;
using System.Linq;

namespace EntityFX.ScoreboardUI.Elements.Controls
{
    public class RadioButton : ToggleButtonBase
    {
        public RadioButton()
        {
            IsCheckedInternal = false;
        }

        public override bool? IsChecked
        {
            get { return base.IsChecked; }
            set
            {
                if (base.IsChecked != true)
                {
                    base.IsChecked = value;
                    IEnumerable<RadioButton> otherRadios =
                        ScoreboardContext.CurrentState.ControlsList.OfType<RadioButton>().Where(_ => _ != this && _.Parent == Parent);
                    foreach (RadioButton radio in otherRadios)
                    {
                        if (radio.IsCheckedInternal != null)
                        {
                            radio.IsCheckedInternal = false;
                            radio.ReRender();
                        }
                    }
                }
            }
        }
    }
}