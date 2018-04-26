namespace EntityFX.ScoreboardUI.Render
{
    public interface IRenderer
    {
        IRenderOptions RenderOptions { get; set; }
        IConsoleAdapter ConsoleAdapter { get; set; }
        void Initialize();

        void Render(UiElement uiElement);
    }
}