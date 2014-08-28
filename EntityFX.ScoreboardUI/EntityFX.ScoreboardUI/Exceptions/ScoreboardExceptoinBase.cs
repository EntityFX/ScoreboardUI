using System;

namespace EntityFX.ScoreboardUI.Exceptions
{
    public abstract class ScoreboardExceptoinBase : Exception
    {
        public ScoreboardExceptoinBase(String message, UiElement uiElement) : base(message)
        {
            UiElement = uiElement;
        }

        public UiElement UiElement { get; private set; }
    }
}