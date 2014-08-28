using System;

namespace EntityFX.ScoreboardUI.Elements
{
    public interface IRenderable
    {
        void Render();

        event EventHandler Rendered;
    }
}