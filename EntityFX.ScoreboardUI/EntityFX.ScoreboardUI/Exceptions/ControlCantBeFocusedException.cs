namespace EntityFX.ScoreboardUI.Exceptions
{
    public class ControlCantBeFocusedException : ScoreboardExceptoinBase
    {
        public ControlCantBeFocusedException(string message, UiElement uiElement) : base(message, uiElement)
        {
        }
    }
}