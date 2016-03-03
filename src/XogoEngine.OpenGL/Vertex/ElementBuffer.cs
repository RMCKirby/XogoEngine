using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class ElementBuffer<T> : IResource<int> where T : struct
    {
        private int handle;
        private readonly IBufferAdapter adapter;

        private bool isDisposed = false;

        public ElementBuffer(IBufferAdapter adapter)
        {
            this.adapter = adapter;
            this.handle = adapter.GenBuffer();
        }

        public int Handle { get { return handle; } }
        public BufferTarget Target { get { return BufferTarget.ElementArrayBuffer; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Dispose()
        {

        }
    }
}
