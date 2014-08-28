using EntityFX.ScoreboardUI.Elements.Scoreboards;

namespace EntityFX.ScoreboardUI
{
    public class StateItem
    {
        public Scoreboard Scoreboard { get; set; }

        public IScoreboardState ScoreboardState { get; set; }
    }
}