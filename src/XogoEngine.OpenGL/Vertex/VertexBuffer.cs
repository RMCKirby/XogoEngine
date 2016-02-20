using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexBuffer<T> where T : struct
    {
        private readonly IBufferAdapter adapter;

        public VertexBuffer(IBufferAdapter adapter)
        {
            this.adapter = adapter;
        }
    }
}
