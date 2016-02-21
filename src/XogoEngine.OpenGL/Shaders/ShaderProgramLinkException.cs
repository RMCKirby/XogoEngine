using System;

namespace XogoEngine.OpenGL.Shaders
{
    [Serializable]
    public sealed class ShaderProgramLinkException : Exception
    {
        public ShaderProgramLinkException(string message) : base(message) { }
    }
}
