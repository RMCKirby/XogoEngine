using System;

namespace XogoEngine.OpenGL.Shaders
{
    [Serializable]
    public sealed class ShaderUniformNotFoundException : Exception
    {
        public ShaderUniformNotFoundException(string message) : base(message) { }
    }
}
