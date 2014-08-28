namespace EntityFX.ScoreboardUI.Render
{
    public class DefaultRenderOptions : IRenderOptions
    {
        public DefaultRenderOptions()
        {
            WindowHeight = 25;
            WindowWidth = 80;
        }

        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
    }
}