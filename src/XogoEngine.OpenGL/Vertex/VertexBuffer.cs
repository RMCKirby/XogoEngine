using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexBuffer<T> : IResource<int> where T : struct
    {
        private int handle;
        private readonly IBufferAdapter adapter;

        private bool isDisposed = false;

        public VertexBuffer(IBufferAdapter adapter)
        {
            this.adapter = adapter;
            this.handle = adapter.GenBuffer();
        }

        public int Handle { get { return handle; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Dispose()
        {
            
        }
    }
}
