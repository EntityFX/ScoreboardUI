namespace EntityFX.ScoreboardUI.Elements.Controls.Menu
{
    public class SubmenuContext<TData>
    {
        public string Text { get; set; }

        public TData Data { get; set; }

        public bool IsEnabled { get; set; }
    }
}