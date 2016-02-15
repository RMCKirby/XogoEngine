using System;

namespace XogoEngine.OpenGL.Shaders
{
    [Serializable]
    public sealed class ShaderCompilationException : Exception
    {
        public ShaderCompilationException(string message) : base(message) { }
    }
}
