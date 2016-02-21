using System;

namespace XogoEngine.OpenGL.Vertex
{
    [Serializable]
    public sealed class UnallocatedBufferSizeException : Exception
    {
        public UnallocatedBufferSizeException(string message) : base(message) { }
    }
}
