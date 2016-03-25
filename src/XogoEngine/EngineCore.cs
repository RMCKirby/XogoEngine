using XogoEngine.OpenGL.Adapters;

namespace XogoEngine
{
    internal static class EngineCore
    {
        private static IGladapter glAdapter;

        internal static IGladapter GlAdapter => glAdapter;
    }
}
