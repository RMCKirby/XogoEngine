using System.Collections.Generic;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Primitives
{
    public interface IPrimitiveShape<TVertex> where TVertex : IVertexDeclarable
    {
        int Stride { get; }
        IEnumerable<TVertex> Vertices { get; }
    }
}
