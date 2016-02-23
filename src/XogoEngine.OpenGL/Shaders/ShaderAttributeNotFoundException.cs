using System;

namespace XogoEngine.OpenGL.Shaders
{
    [Serializable]
    public sealed class ShaderAttributeNotFoundException : Exception
    {
        public ShaderAttributeNotFoundException(string message) : base(message) { }
    }
}
