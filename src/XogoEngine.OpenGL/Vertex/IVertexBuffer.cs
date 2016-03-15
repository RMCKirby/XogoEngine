using OpenTK.Graphics.OpenGL4;
using System;

namespace XogoEngine.OpenGL.Vertex
{
    public interface IVertexBuffer<TVertex> where TVertex : struct, IVertexDeclarable
    {
        BufferTarget Target { get; }
        IntPtr Size { get; }
        IVertexDeclaration VertexDeclaration { get; }

        void Bind();
        void Fill(IntPtr size, TVertex[] data, BufferUsageHint usageHint);
        void FillPartial(IntPtr size, IntPtr offset, TVertex[] data);
    }
}
