namespace EntityFX.ScoreboardUI.Render
{
    public interface IRenderOptions
    {
        int WindowHeight { get; set; }

        int WindowWidth { get; set; }

        ColorScheme ColorScheme { get; set; }
    }
}