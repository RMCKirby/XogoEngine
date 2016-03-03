using OpenTK.Graphics.OpenGL4;
using System;
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
        public IntPtr Size { get; private set; }
        public bool IsDisposed { get { return isDisposed; } }

        public void Bind()
        {
            this.ThrowIfDisposed();
            adapter.BindBuffer(Target, handle);
        }

        public void Fill(IntPtr size, T[] indiceData, BufferUsageHint usageHint)
        {
            this.ThrowIfDisposed();
            if (size == IntPtr.Zero || size.ToInt32() < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"The size allocated to the buffer must be greater than zero, got : {size}"
                );
            }
            adapter.BufferData(Target, size, indiceData, usageHint);
            Size = size;
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
