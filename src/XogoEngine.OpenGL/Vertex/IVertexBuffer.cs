using OpenTK.Graphics.OpenGL4;
using System;

namespace XogoEngine.OpenGL.Vertex
{
    public interface IVertexBuffer<T> where T : struct, IVertexDeclarable
    {
        BufferTarget Target { get; }
        IntPtr Size { get; }

        void Bind();
        void Fill(IntPtr size, T[] data, BufferUsageHint usageHint);
        void FillPartial(IntPtr size, IntPtr offset, T[] data);
    }
}
