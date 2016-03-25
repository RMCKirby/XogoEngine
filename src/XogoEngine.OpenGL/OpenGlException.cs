using System;

namespace XogoEngine.OpenGL
{
    [Serializable]
    public sealed class OpenGlException : Exception
    {
        public OpenGlException(string message) : base(message) { }
    }
}
