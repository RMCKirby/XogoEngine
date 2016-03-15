using OpenTK.Graphics.OpenGL4;
using System;

namespace XogoEngine.OpenGL.Vertex
{
    public interface IElementBuffer<T> where T : struct
    {
        BufferTarget Target { get; }
        IntPtr Size { get; }

        void Bind();
        void Fill(IntPtr size, T[] data, BufferUsageHint usageHint);
    }
}
