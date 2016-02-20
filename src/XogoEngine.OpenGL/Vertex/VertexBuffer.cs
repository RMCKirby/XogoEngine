using OpenTK.Graphics.OpenGL4;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

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
        public BufferTarget Target { get { return BufferTarget.ArrayBuffer; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Bind()
        {
            this.ThrowIfDisposed();
            adapter.BindBuffer(Target, handle);
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteBuffer(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
