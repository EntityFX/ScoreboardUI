using System;
using EntityFX.ScoreboardUI.Render;

namespace EntityFX.ScoreboardUI
{
    internal static class ScoreboardContext
    {
        private static readonly Lazy<IStoreboardNavigationEngine> _navigation = new Lazy<IStoreboardNavigationEngine>(
            () => new StoreboardNavigationEngine()
            );

        private static readonly Lazy<IRenderer> _renderer = new Lazy<IRenderer>(
            () =>
            {
                var renderer = new DefaultRenderer(new ConsoleAdapter(), new DefaultRenderOptions() { ColorScheme = ColorSchemas.Matrix });
                renderer.Initialize();
                return renderer;
            }
        );

        public static IStoreboardNavigationEngine Navigation => _navigation.Value;

        public static IScoreboardState CurrentState
        {
            get
            {
                if (Navigation.Current == null)
                    Navigation.Current = new StateItem {ScoreboardState = new ScoreboardState()};
                return Navigation.Current.ScoreboardState;
            }
        }

        public static IRenderer RenderEngine => _renderer.Value;
    }
}