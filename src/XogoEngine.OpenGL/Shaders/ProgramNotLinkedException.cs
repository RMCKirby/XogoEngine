using System;

namespace XogoEngine.OpenGL.Shaders
{
    [Serializable]
    public sealed class ProgramNotLinkedException : Exception
    {
        public ProgramNotLinkedException(string message) : base(message) { }
    }
}
