namespace EntityFX.ScoreboardUI.Render
{
    public interface IRenderer
    {
        IRenderOptions RenderOptions { get; }
        IConsoleAdapter ConsoleAdapter { get; }
        void Initialize();

        void Render(UiElement uiElement);
    }
}