using XogoEngine.OpenGL.Adapters;

namespace XogoEngine
{
    internal static class EngineCore
    {
        private static readonly IGlAdapter glAdapter = new GlAdapter();

        internal static IGlAdapter GlAdapter => glAdapter;
    }
}
